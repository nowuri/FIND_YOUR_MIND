package com.example.find_your_mind.Playlist

import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.Color
import android.graphics.drawable.ColorDrawable
import android.net.Uri
import android.os.Bundle
import android.util.Log
import android.view.Gravity
import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.AdapterView
import android.widget.ImageView
import android.widget.PopupWindow
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.find_your_mind.Connecting
import com.example.find_your_mind.DB.MusicRepository
import com.example.find_your_mind.FBSocketService
import com.example.find_your_mind.R
import com.example.find_your_mind.databinding.ActivityPlaylistBinding
import com.example.find_your_mind.databinding.ActivityPopUpMessageBinding
import com.example.find_your_mind.unityMainActivity2
import com.google.firebase.database.*

class PlaylistActivity : AppCompatActivity() {
    private lateinit var viewBinding: ActivityPlaylistBinding
    private lateinit var databaseReference: DatabaseReference
    private lateinit var feelingDatabaseImageView: ImageView
    private var phoneNumberToCall: String? = null // 전역 변수로 전화번호 저장

    // 감정 문자열을 인덱스로 매핑
    private val emotionToIndex = mapOf(
        "Angry" to 0,
        "Disgust" to 1,
        "Fear" to 2,
        "Happiness" to 3,
        "Neutral" to 4,
        "Sadness" to 5,
        "Surprise" to 6
    )

    // 감정 인덱스에 해당하는 이미지 리소스 맵핑
    private val idxToDrawable = mapOf(
        0 to R.drawable.img_angry,
        1 to R.drawable.img_disgust,
        2 to R.drawable.img_fear,
        3 to R.drawable.img_happy,
        4 to R.drawable.img_neutral,
        5 to R.drawable.img_sad,
        6 to R.drawable.img_surprise
    )

    // DBMusic을 PlaylistMusic으로 변환하는 함수
    fun convertToPlaylistMusicList(dbMusicList: ArrayList<com.example.find_your_mind.DB.Music>): ArrayList<com.example.find_your_mind.Playlist.Music> {
        val playlistMusicList = ArrayList<com.example.find_your_mind.Playlist.Music>()
        for (dbMusic in dbMusicList) {
            val playlistMusic = com.example.find_your_mind.Playlist.Music(dbMusic.title, dbMusic.artist, dbMusic.youtubeLink)
            playlistMusicList.add(playlistMusic)
        }
        return playlistMusicList
    }

    // Firebase에서 최신 감정 데이터를 가져오는 함수
    private fun fetchLatestEmotion() {
        databaseReference.orderByChild("timestamp").limitToLast(1)
            .addListenerForSingleValueEvent(object : ValueEventListener {
                override fun onDataChange(snapshot: DataSnapshot) {
                    for (data in snapshot.children) {
                        val emotionStr = data.child("emotion").getValue(String::class.java)
                        emotionStr?.let {
                            val emotionIndex = emotionToIndex[it] ?: 4 // 기본값으로 Neutral 상태(인덱스 4) 사용
                            Log.e("emotion Index", emotionStr)
                            val drawableRes = idxToDrawable[emotionIndex]  // 인덱스에 해당하는 이미지 리소스를 가져옴
                            if (drawableRes != null) {
                                // 이미지 설정
                                feelingDatabaseImageView.setImageResource(drawableRes)

                                // 감정 인덱스에 따라 플레이리스트 업데이트
                                updatePlaylistForEmotion(emotionIndex)

                                // 감정 인덱스가 0, 2, 5 중 하나라면 팝업 표시
                                if (emotionIndex == 0 || emotionIndex == 2 || emotionIndex == 5) {
                                    showEmotionPopup()
                                    Log.d("emotion Bad", emotionStr)
                                }
                                else{
                                    Log.d("emotion Good", emotionStr)
                                }

                            } else {
                                Log.e("PlaylistActivity", "Invalid emotion index: $emotionIndex")
                            }
                        }
                    }
                }

                override fun onCancelled(error: DatabaseError) {
                    Log.e("PlaylistActivity", "Failed to fetch latest emotion: ${error.message}")
                }
            })
    }

    // 감정 인덱스에 따른 플레이리스트 업데이트 메서드
    private fun updatePlaylistForEmotion(emotionIndex: Int) {
        val repository = MusicRepository(this)
        val dbMusicList = repository.getMusicList_num(emotionIndex)
        val playlistMusicList = convertToPlaylistMusicList(dbMusicList)
        val adapter = PlaylistAdapter(this, playlistMusicList)
        viewBinding.playlist.adapter = adapter
    }

    // 감정이 0, 2, 5일 때 호출되는 팝업 메서드
    private fun showEmotionPopup() {
        val inflater = LayoutInflater.from(this)
        val popupView = inflater.inflate(R.layout.activity_pop_up_message, null)
        val popupBinding = ActivityPopUpMessageBinding.bind(popupView)

        val popupWindow = PopupWindow(
            popupView,
            ViewGroup.LayoutParams.WRAP_CONTENT,
            ViewGroup.LayoutParams.WRAP_CONTENT,
            true
        )

        // PopupWindow의 배경을 반투명하게 설정
        popupWindow.setBackgroundDrawable(ColorDrawable(Color.TRANSPARENT))
        popupWindow.isOutsideTouchable = true
        popupWindow.isFocusable = true

        // 팝업 창 표시
        popupWindow.showAtLocation(viewBinding.root, Gravity.CENTER, 0, 0)

        // 팝업 창 외부를 어둡게 설정
        val window = this.window
        val layoutParams = window.attributes
        layoutParams.alpha = 0.5f // 반투명 효과
        window.attributes = layoutParams

        popupWindow.setOnDismissListener {
            // 원래 상태로 되돌리기
            layoutParams.alpha = 1.0f
            window.attributes = layoutParams
        }

        // number1 클릭 이벤트 처리
        popupBinding.number1.setOnClickListener {
            val telnum = popupBinding.number1.text.toString().split(": ")[1] // 전화번호 추출
            val intent = Intent(Intent.ACTION_DIAL)
            intent.data = Uri.parse("tel:$telnum")
            startActivity(intent)
        }

        // number2 클릭 이벤트 처리
        popupBinding.number2.setOnClickListener {
            val telnum2 = popupBinding.number2.text.toString().split(": ")[1]
            val intent = Intent(Intent.ACTION_DIAL)
            intent.data = Uri.parse("tel:$telnum2")
            startActivity(intent)
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        viewBinding = ActivityPlaylistBinding.inflate(layoutInflater)
        setContentView(viewBinding.root)

        // ImageView 참조
        feelingDatabaseImageView = findViewById(R.id.feeling_database)
        // Firebase 데이터베이스 참조 가져오기
        databaseReference = FirebaseDatabase.getInstance("https://find-your-mind-fbadd-default-rtdb.asia-southeast1.firebasedatabase.app/")
            .getReference("FBSocketData")
        // 가장 최근의 데이터 가져오기
        fetchLatestEmotion()

        val repository = MusicRepository(this)

        // 초기 데이터베이스에 음악 추가
        // 예: 0번 감정(Angry)에 대한 음악 추가
        repository.addMusic("https://www.youtube.com/watch?v=OaNKSviZ9ME", "5sos", "Not in the same way", 0)
        repository.addMusic("https://www.youtube.com/watch?v=medo8dj_-28", "한요한", "Bumper Car", 0)
        repository.addMusic("https://www.youtube.com/watch?v=zUUwTI0cIyM", "에픽하이", "뒷담화", 0)
        repository.addMusic("https://www.youtube.com/watch?v=1CptfMEEC8g", "HUGEL", "WTF", 0)
        repository.addMusic("https://www.youtube.com/watch?v=NaFd8ucHLuo", "Gayle", "abcdefu", 0)

        // 초기화된 플레이리스트 가져오기 (기본적으로 Neutral 상태로 초기화)
        updatePlaylistForEmotion(4)  // 감정 인덱스 4: Neutral

        // 리스트 항목 클릭 시 동작
        viewBinding.playlist.onItemClickListener =
            AdapterView.OnItemClickListener { parent, _, position, _ ->
                val selectItem = parent.getItemAtPosition(position) as com.example.find_your_mind.Playlist.Music
                val intent = Intent(Intent.ACTION_VIEW, Uri.parse(selectItem.url))
                startActivity(intent)
            }

        viewBinding.calling.setOnClickListener {
            val intent = Intent(this, unityMainActivity2::class.java)
            startActivity(intent)

            val fbSocketServiceIntent = Intent(this, FBSocketService::class.java)
            startService(fbSocketServiceIntent)
        }
    }

    private fun callPhone(telnum: String) {
        val intent = Intent(Intent.ACTION_CALL)
        intent.data = Uri.parse("tel:$telnum")
        startActivity(intent)
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<out String>,
        grantResults: IntArray
    ) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)
        if (requestCode == 1 && grantResults.isNotEmpty() && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
            phoneNumberToCall?.let { callPhone(it) } // 전역 변수에서 전화번호 가져오기
        } else {
            Toast.makeText(this, "전화 권한이 필요합니다.", Toast.LENGTH_SHORT).show()
        }
    }
}
