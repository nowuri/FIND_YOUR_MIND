package com.example.find_your_mind.Playlist

import android.Manifest
import android.content.Intent
import android.content.pm.PackageManager
import android.net.Uri
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.example.find_your_mind.R
import com.example.find_your_mind.databinding.ActivityPopUpMessageBinding

class PopUpMessage : AppCompatActivity() {
    private lateinit var viewBinding: ActivityPopUpMessageBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        Log.d("On Create","On Create start")
        super.onCreate(savedInstanceState)
        viewBinding = ActivityPopUpMessageBinding.inflate(layoutInflater)
        setContentView(viewBinding.root)

        // TextView가 클릭 이벤트를 처리하도록 설정
        viewBinding.number1.setOnClickListener {
            Log.d("number1","Number1 clicked")
            val telnum = viewBinding.number1.text.toString().split(": ")[1] // 전화번호 추출
            if (ContextCompat.checkSelfPermission(
                    this,
                    Manifest.permission.CALL_PHONE
                ) != PackageManager.PERMISSION_GRANTED) {
                ActivityCompat.requestPermissions(this, arrayOf(Manifest.permission.CALL_PHONE), 1)
            } else {
                callPhone(telnum)
            }
        }

        viewBinding.number2.setOnClickListener {
            Log.d("number2","Number2 clicked")
            val telnum2 = viewBinding.number2.text.toString().split(": ")[1]
            val intent = Intent(Intent.ACTION_DIAL)
            intent.data = Uri.parse("tel:$telnum2")
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
            val telnum = viewBinding.number1.text.toString().split(": ")[1]
            callPhone(telnum)
        } else {
            Toast.makeText(this, "전화 권한이 필요합니다.", Toast.LENGTH_SHORT).show()
        }
    }
}
