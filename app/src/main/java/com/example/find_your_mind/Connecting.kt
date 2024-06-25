package com.example.find_your_mind

import android.content.Intent
import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.example.find_your_mind.Playlist.PlaylistActivity
import com.example.find_your_mind.databinding.ActivityConnectingBinding

class Connecting : AppCompatActivity() {
    private lateinit var viewBinding: ActivityConnectingBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        viewBinding = ActivityConnectingBinding.inflate(layoutInflater)
        super.onCreate(savedInstanceState)
        setContentView(viewBinding.root)

        viewBinding.callEnd.setOnClickListener{
            val intent = Intent(this, PlaylistActivity::class.java)
            startActivity(intent)
        }
    }
}