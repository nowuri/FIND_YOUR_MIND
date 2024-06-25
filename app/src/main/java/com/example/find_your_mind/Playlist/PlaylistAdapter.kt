package com.example.find_your_mind.Playlist

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.find_your_mind.databinding.ItemMusicBinding

class PlaylistAdapter(val context: Context, val musicList: ArrayList<com.example.find_your_mind.Playlist.Music>) : BaseAdapter() {

    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val binding: ItemMusicBinding
        val view: View

        if (convertView == null) {
            binding = ItemMusicBinding.inflate(LayoutInflater.from(context), parent, false)
            view = binding.root
            view.tag = binding
        } else {
            binding = convertView.tag as ItemMusicBinding
            view = convertView
        }

        val music = musicList[position]
        binding.name.text = music.name
        binding.singer.text = music.singer

        return view
    }

    override fun getItem(position: Int): Any {
        return musicList[position]
    }

    override fun getItemId(position: Int): Long {
        return position.toLong()
    }

    override fun getCount(): Int {
        return musicList.size
    }
}
