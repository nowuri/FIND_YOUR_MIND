package com.example.find_your_mind.Playlist

import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.Color
import android.graphics.drawable.ColorDrawable
import android.net.Uri
import android.os.Bundle
import android.view.Gravity
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.WindowManager
import android.widget.AdapterView
import android.widget.PopupWindow
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.example.find_your_mind.DB.DBHelper
import com.example.find_your_mind.DB.MusicRepository
import com.example.find_your_mind.FriendChoose
import com.example.find_your_mind.R
import com.example.find_your_mind.databinding.ActivityPlaylistBinding
import com.example.find_your_mind.databinding.ActivityPopUpMessageBinding
import com.example.find_your_mind.DB.Music as DBMusic
import com.example.find_your_mind.Playlist.Music as PlaylistMusic
import android.Manifest
import com.example.find_your_mind.Connecting

class PlaylistActivity : AppCompatActivity() {
    private lateinit var viewBinding: ActivityPlaylistBinding
    private var phoneNumberToCall: String? = null // 전역 변수로 전화번호 저장

    fun convertToPlaylistMusicList(dbMusicList: ArrayList<DBMusic>): ArrayList<PlaylistMusic> {
        val PlaylistMusicList = ArrayList<PlaylistMusic>()
        for (dbMusic in dbMusicList) {
            val playlistMusic = PlaylistMusic(dbMusic.title, dbMusic.artist, dbMusic.youtubeLink)
            PlaylistMusicList.add(playlistMusic)
        }
        return PlaylistMusicList
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        viewBinding = ActivityPlaylistBinding.inflate(layoutInflater)
        super.onCreate(savedInstanceState)
        setContentView(viewBinding.root)

        val repository = MusicRepository(this)
        //0. Angry
        repository.addMusic(
            "https://www.youtube.com/watch?v=OaNKSviZ9ME", "5sos",
            "Not in the same way", 0
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=medo8dj_-28", "한요한",
            "Bumper Car", 0
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=zUUwTI0cIyM", "에픽하이",
            "뒷담화", 0
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=1CptfMEEC8g", "HUGEL",
            "WTF", 0
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=NaFd8ucHLuo", "Gayle",
            "abcdefu", 0
        )

        //1. Disgust
        repository.addMusic(
            "https://www.youtube.com/watch?v=XaMVO0h9QP4", "Munn",
            "I can't breath", 1
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=3HJS0b8d-JA", "STANDING EGG",
            "여름밤에 우린", 1
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=d2ytH5mymWY", "산들",
            "취기를 빌려", 1
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=ORtV9IFMRjQ", "Sture Zetterberg",
            "Think About You", 1
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=Qc7_zRjH808", "Fifty Fifty",
            "Cupid", 1
        )

        //2. Fear
        repository.addMusic(
            "https://www.youtube.com/watch?v=CyyZyzbQ45Y", "Sarah Kang",
            "Summer Is for Falling in Love", 2
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=DGXPro9x0yo", "데이먼스이어",
            "Yours", 2
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=w3-AKITQMi0", "혁오",
            "Tomboy", 2
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=WbhK3wMXluE", "백예린",
            "Bye bye my blue", 2
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=JSOBF_WhqEM", "아이유",
            "이름에게", 2
        )

        //3. Happy
        repository.addMusic(
            "https://www.youtube.com/watch?v=E35In9bsWh4", "볼빨간 사춘기",
            "Love Story", 3
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=vRdZVDWs3BI", "EHYPEN",
            "Polaroid Love", 3
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=35lirBqwgTs", "미연(아이들)",
            "Drive", 3
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=ZbZSe6N_BXs", "Pharrell Williams",
            "Happy", 3
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=qDRORgoZxZU", "Meghan Trainor",
            "Me too", 3
        )

        //4. Netural
        repository.addMusic(
            "https://www.youtube.com/watch?v=VQZXXciZb_c", "10cm",
            "그라데이션", 4
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=WQcU5j_HOA0", "LANY",
            "nobody else", 4
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=ic8j13piAhQ", "Taylor Swift",
            "Cruel Summer", 4
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=lNvBbh5jDcA", "JOY(조이)",
            "안녕", 4
        )
        repository.addMusic("https://www.youtube.com/watch?v=tYmBm1qKD3I", "폴킴", "우린 제법 잘 어울려요", 4)

        //5. Sad
        repository.addMusic(
            "https://www.youtube.com/watch?v=0q6DR6EiPPo", "선우정아",
            "도망가자", 5
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=JSOBF_WhqEM", "아이유",
            "이름에게", 5
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=nSDgHBxUbVQ", "Ed Sheeran",
            "Photograph", 5
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=vovxrQdQ_gk", "Yaeow",
            "Behind the clouds", 5
        )
        repository.addMusic(
            "https://youtu.be/qMkQ3lAD418?si=qIMsHBP-KalOR5DD", "권진아",
            "위로", 5
        )

        //6. Surprize
        repository.addMusic(
            "https://www.youtube.com/watch?v=C0puxFMBLHw", "최유리",
            "오랜만이야", 6
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=UnWRKuEtdtI", "적재",
            "지금 이대로", 6
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=HjmBLCbTgDo", "FINNEAS",
            "Break My Heart Again", 6
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=kOCkne-Bku4", "Lavu",
            "Paris in the Rain", 6
        )
        repository.addMusic(
            "https://www.youtube.com/watch?v=4iFP_wd6QU8", "백예린",
            "Square", 6
        )


        var dbMusicList = ArrayList<DBMusic>()
        dbMusicList = repository.getMusicList_num(4)
        val playlistMusicList = convertToPlaylistMusicList(dbMusicList)
        val Adapter = PlaylistAdapter(this, playlistMusicList)
        viewBinding.playlist.adapter = Adapter

        viewBinding.playlist.onItemClickListener =
            AdapterView.OnItemClickListener { parent, view, position,
                                              id ->
                val selectItem = parent.getItemAtPosition(position) as PlaylistMusic
                val intent = Intent(Intent.ACTION_VIEW, Uri.parse(selectItem.url))
                startActivity(intent)
            }
        //Toast.makeText(this, selectItem.name, Toast.LENGTH_SHORT).show()}

        val video_url = Uri.parse("https://youtu.be/RLODP2xmu7s?si=14BnwNQc0XFcgpSf")

        viewBinding.videoName.setOnClickListener {
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
            popupWindow.showAtLocation(it, Gravity.CENTER, 0, 0)

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

        viewBinding.calling.setOnClickListener {
            val intent = Intent(this, FriendChoose::class.java)
            startActivity(intent)
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