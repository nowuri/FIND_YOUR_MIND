package com.example.find_your_mind

import android.app.Service
import android.content.Intent
import android.os.AsyncTask
import android.os.IBinder
import android.util.Log
import com.google.firebase.database.FirebaseDatabase
import org.json.JSONObject
import java.io.InputStreamReader
import java.net.Socket

class FBSocketService : Service() {

    private val TAG = "FBSocketService"

    override fun onCreate() {
        super.onCreate()
        Log.d(TAG, "FBSocketService created")
        // 소켓 작업 시작
        SocketTask().execute("192.168.3.16", 9621) // 서버의 IP와 포트 사용
    }

    override fun onBind(intent: Intent?): IBinder? {
        return null
    }

    inner class SocketTask : AsyncTask<Any, Void, Void>() {

        override fun doInBackground(vararg params: Any?): Void? {
            val host = params[0] as String
            val port = params[1] as Int
            var socket: Socket? = null

            try {
                socket = Socket(host, port)
                val inputStream = socket.getInputStream()
                val reader = InputStreamReader(inputStream, "UTF-8")
                val bufferedReader = reader.buffered()

                while (true) {
                    val result = bufferedReader.readLine()
                    if (result != null) {
                        // 서버에서 받은 데이터를 로그에 출력
                        Log.d(TAG, "Received data: $result")

                        // 서버에서 받은 데이터를 Firebase에 저장
                        saveDataToFirebase(result)

                    } else {
                        break
                    }
                }

            } catch (e: Exception) {
                Log.e(TAG, "Error: ${e.message}", e)
            } finally {
                socket?.close()
            }

            return null
        }

        private fun saveDataToFirebase(jsonString: String) {
            try {
                val jsonObject = JSONObject(jsonString)

                // JSON 데이터에서 정보 추출
                val emotion = jsonObject.getString("first")
                val landmarks = jsonObject.getJSONArray("landmarks")
                val timestamp = jsonObject.getString("timestamp")

                // Firebase에 데이터를 저장할 Map 생성
                val emotionData = HashMap<String, Any>()
                emotionData["emotion"] = emotion
                emotionData["landmarks"] = landmarks.toString() // Firebase에 문자열로 저장
                emotionData["timestamp"] = timestamp

                // 데이터베이스에 새로운 항목을 추가
                val newEntryRef = FirebaseDatabase.getInstance("https://find-your-mind-fbadd-default-rtdb.asia-southeast1.firebasedatabase.app/")
                    .getReference("FBSocketData").push()
                newEntryRef.setValue(emotionData)
                    .addOnSuccessListener {
                        Log.d(TAG, "Data saved successfully")
                    }
                    .addOnFailureListener { e ->
                        Log.e(TAG, "Failed to save data: ${e.message}", e)
                    }

            } catch (e: Exception) {
                Log.e(TAG, "Error saving data: ${e.message}", e)
            }
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        Log.d(TAG, "FBSocketService destroyed")
        // 필요한 경우 리소스 해제 등 추가 작업 수행
    }
}
