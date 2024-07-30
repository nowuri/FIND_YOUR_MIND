package com.example.find_your_mind

import android.Manifest
import android.content.Intent
import android.content.pm.PackageManager
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.camera.core.CameraSelector
import androidx.camera.core.Preview
import androidx.camera.lifecycle.ProcessCameraProvider
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.lifecycle.LifecycleOwner
import com.example.find_your_mind.Playlist.PlaylistActivity
import com.example.find_your_mind.databinding.ActivityUnityMain2Binding
import com.google.common.util.concurrent.ListenableFuture
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import java.io.ByteArrayOutputStream
import java.net.HttpURLConnection
import java.net.URL
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale
import android.graphics.Bitmap
import android.util.Base64
import org.json.JSONObject

class unityMainActivity2 : AppCompatActivity() {
    private lateinit var viewBinding: ActivityUnityMain2Binding
    private lateinit var mUnityPlayer: UnityPlayer
    private val cameraPermissionRequestCode = 101
    private val TAG = "unityMainActivity2"
    private val TAG2 = "encodedStr"

    override fun onCreate(savedInstanceState: Bundle?) {
        viewBinding = ActivityUnityMain2Binding.inflate(layoutInflater)
        super.onCreate(savedInstanceState)
        setContentView(viewBinding.root)

        // 유니티 게임 화면 표시
        mUnityPlayer = UnityPlayer(this)
        viewBinding.unityFrameLayout.addView(mUnityPlayer.view)

        // 카메라 화면 표시
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.CAMERA)
            == PackageManager.PERMISSION_GRANTED) {
            startCamera()
        } else {
            ActivityCompat.requestPermissions(this, arrayOf(Manifest.permission.CAMERA),
                cameraPermissionRequestCode)
        }

        // 버튼 클릭 이벤트 처리
        viewBinding.callEnd.setOnClickListener {
            val intent = Intent(this, PlaylistActivity::class.java)
            startActivity(intent)
        }
    }

    private fun startCamera() {
        val cameraProviderFuture: ListenableFuture<ProcessCameraProvider> =
            ProcessCameraProvider.getInstance(this)

        cameraProviderFuture.addListener({
            try {
                val cameraProvider: ProcessCameraProvider = cameraProviderFuture.get()
                val preview = Preview.Builder().build()
                preview.setSurfaceProvider(viewBinding.smallPreviewView.surfaceProvider)

                val cameraSelector = CameraSelector.Builder()
                    .requireLensFacing(CameraSelector.LENS_FACING_FRONT)
                    .build()

                cameraProvider.unbindAll()
                cameraProvider.bindToLifecycle(this as LifecycleOwner, cameraSelector, preview)

                Log.d(TAG, "Camera started successfully")

                // 주기적으로 카메라 프레임을 캡쳐하여 Python 서버로 전송
                val frameCaptureInterval = 1000L // 1초 간격으로 캡처
                val handler = android.os.Handler()
                val frameCaptureRunnable = object : Runnable {
                    override fun run() {
                        captureAndSendFrame()
                        handler.postDelayed(this, frameCaptureInterval)
                    }
                }
                handler.post(frameCaptureRunnable)
            } catch (e: Exception) {
                e.printStackTrace()
                Log.e(TAG, "Failed to start camera: ${e.message}")
            }
        }, ContextCompat.getMainExecutor(this))
    }

    private fun captureAndSendFrame() {
        viewBinding.smallPreviewView.bitmap?.let { bitmap ->
            val encodedString = bitmapToBase64(bitmap)
            // 코루틴을 사용하여 네트워크 작업 수행
            CoroutineScope(Dispatchers.IO).launch {
                try {
                    sendToServer(encodedString)
                } catch (e: Exception) {
                    e.printStackTrace()
                    Log.e(TAG, "Error sending request", e)
                }
            }
        }
    }

    private fun bitmapToBase64(bitmap: Bitmap): String {
        val byteArrayOutputStream = ByteArrayOutputStream()
        bitmap.compress(Bitmap.CompressFormat.JPEG, 80, byteArrayOutputStream) // JPEG 형식으로 압축
        val byteArray = byteArrayOutputStream.toByteArray()
        val encodedString = Base64.encodeToString(byteArray, Base64.NO_WRAP) // 줄바꿈 없이 인코딩

        Log.d(TAG, "Encoded string length: ${encodedString.length}")
        Log.d(TAG, "Encoded string: $encodedString")

        return encodedString
    }

    private suspend fun sendToServer(encodedString: String) {
        withContext(Dispatchers.IO) {
            try {
                val url = URL("http://192.168.60.16:5001/upload")
                val connection = url.openConnection() as HttpURLConnection
                connection.requestMethod = "POST"
                connection.setRequestProperty("Content-Type", "application/json")
                connection.doOutput = true

                val currentTime = SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.getDefault()).format(Date())
                val jsonInputString = "{\"image\":\"$encodedString\", \"timestamp\":\"$currentTime\"}"
                Log.d(TAG2, "Sending JSON at $currentTime: $jsonInputString")

                connection.outputStream.use { os ->
                    val input = jsonInputString.toByteArray(Charsets.UTF_8)
                    os.write(input, 0, input.size)
                }

                if (connection.responseCode != HttpURLConnection.HTTP_OK) {
                    throw RuntimeException("Failed : HTTP error code : ${connection.responseCode}")
                }

                connection.inputStream.use { response ->
                    val responseText = response.bufferedReader().readText()
                    Log.d(TAG, "Server response: $responseText")
                }
            } catch (e: Exception) {
                Log.e(TAG, "Error sending request", e)
                throw e
            }
        }
    }

    override fun onResume() {
        super.onResume()
        mUnityPlayer.resume()
    }

    override fun onPause() {
        super.onPause()
        mUnityPlayer.pause()
    }

    override fun onDestroy() {
        super.onDestroy()
        mUnityPlayer.destroy()
    }

    override fun onLowMemory() {
        super.onLowMemory()
        mUnityPlayer.lowMemory()
    }

    override fun onWindowFocusChanged(hasFocus: Boolean) {
        super.onWindowFocusChanged(hasFocus)
        mUnityPlayer.windowFocusChanged(hasFocus)
    }

    override fun onRequestPermissionsResult(requestCode: Int, permissions: Array<String>, grantResults: IntArray) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)
        if (requestCode == cameraPermissionRequestCode) {
            if ((grantResults.isNotEmpty() && grantResults[0] == PackageManager.PERMISSION_GRANTED)) {
                Log.d(TAG, "Camera permission granted")
                startCamera()
            } else {
                Log.e(TAG, "Camera permission denied")
            }
        }
    }
}
