package com.example.find_your_mind.DB

import android.content.Context

class MusicRepository(context: Context) {
    private val dbHelper = DBHelper(context, "MusicDB", null, 1)

    fun addMusic(youtubeLink: String, artist: String, title:String, emotion: Int){
        dbHelper.insertMusic(title, artist, youtubeLink, emotion)
    }

    fun getMusicList_num(emotion: Int): ArrayList<Music> {
        return dbHelper.getAllMusic_num(emotion)
    }
}