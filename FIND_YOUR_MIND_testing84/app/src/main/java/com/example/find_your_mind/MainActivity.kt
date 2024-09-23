package com.example.find_your_mind

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.find_your_mind.Playlist.PlaylistActivity
import com.example.find_your_mind.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {
    private lateinit var viewBinding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        viewBinding = ActivityMainBinding.inflate(layoutInflater)
        super.onCreate(savedInstanceState)
        setContentView(viewBinding.root)

        viewBinding.startButton.setOnClickListener(){
            var intent = Intent(this, PlaylistActivity::class.java)
            startActivity(intent)
        }
    }
}