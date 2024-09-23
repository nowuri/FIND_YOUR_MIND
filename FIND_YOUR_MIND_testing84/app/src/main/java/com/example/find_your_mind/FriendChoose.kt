package com.example.find_your_mind

import android.content.Intent
import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.example.find_your_mind.databinding.ActivityFriendChooseBinding

class FriendChoose : AppCompatActivity() {
    private lateinit var viewBinding: ActivityFriendChooseBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        viewBinding = ActivityFriendChooseBinding.inflate(layoutInflater)
        super.onCreate(savedInstanceState)
        setContentView(viewBinding.root)

        viewBinding.friend1.setOnClickListener {
            val intent = Intent(this, Connecting::class.java)
            startActivity(intent)
        }

        viewBinding.friend2.setOnClickListener {
            val intent = Intent(this, Connecting::class.java)
            startActivity(intent)
        }

    }
}