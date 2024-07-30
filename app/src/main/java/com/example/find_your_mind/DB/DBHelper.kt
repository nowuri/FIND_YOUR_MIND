package com.example.find_your_mind.DB

import android.content.ContentValues
import android.content.Context
import android.database.sqlite.SQLiteDatabase
import android.database.sqlite.SQLiteOpenHelper
import com.example.find_your_mind.DB.Music

class DBHelper(context: Context?, name: String?, factory: SQLiteDatabase.CursorFactory?, version: Int)
    : SQLiteOpenHelper(context, name, factory, version) {
    override fun onCreate(db: SQLiteDatabase?) {
        var createTableSQL = """
            CREATE TABLE IF NOT EXISTS Music(
                _id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT,
                artist TEXT,
                link TEXT,
                emotion INTEGER
            );
        """

        db!!.execSQL(createTableSQL)
    }

    override fun onUpgrade(db: SQLiteDatabase?, oldVersion: Int, newVersion: Int) {
        val dropTableSQL = "DROP TABLE IF EXISTS Music"

        db!!.execSQL(dropTableSQL)
        onCreate(db)
    }

    fun insertMusic(title: String, artist: String, link: String, emotion: Int){
        val db = this.writableDatabase

        val values = ContentValues().apply{
            put("title", title)
            put("artist", artist)
            put("link", link)
            put("emotion", emotion)
        }

        db.insert("Music", null, values)
        db.close()
    }

    fun getAllMusic(): List<Music>{
        val db = this.readableDatabase
        val cursor = db.query("Music", null, null, null, null, null, null)
        val musicList = mutableListOf<Music>()

        with(cursor) {
            while(moveToNext()){
                val id = getInt(getColumnIndexOrThrow("_id"))
                val title = getString(getColumnIndexOrThrow("title"))
                val artist = getString(getColumnIndexOrThrow("artist"))
                val link = getString(getColumnIndexOrThrow("link"))
                val emotion = getInt(getColumnIndexOrThrow("emotion"))
                musicList.add(Music(id, link, artist, title, emotion))
            }
        }

        cursor.close()
        db.close()
        return musicList
    }

    fun getAllMusic_num(emotion: Int): ArrayList<Music> {
        val db = this.readableDatabase
        val musicList = ArrayList<Music>()

        val selection = "emotion = ?"
        val selectionArgs = arrayOf(emotion.toString())

        val cursor = db.query(
            "Music", null, selection, selectionArgs, null, null, null
        )

        with(cursor) {
            while(moveToNext()){
                val id = getInt(getColumnIndexOrThrow("_id"))
                val title = getString(getColumnIndexOrThrow("title"))
                val artist = getString(getColumnIndexOrThrow("artist"))
                val link = getString(getColumnIndexOrThrow("link"))

                val music = Music(id, link, artist, title, emotion)
                musicList.add(music)
            }
            close()
        }

        return musicList
    }
}