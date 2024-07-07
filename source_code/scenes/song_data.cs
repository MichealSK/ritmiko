using Godot;
using System;
using System.Collections.Generic;

namespace Ritmiko
{
	public partial class song_data : Resource
	{
		public String title = "Song Title";
		public int difficulty = 1;
		public float noteSpeed = 2.88f;
		public List<String> playerCharts = new List<string>();
		public String audiopath;
		public int songLength;

		public song_data(String songTitle, int difficulty, float noteSpeed, String[] playerCharts, String audio, int length)
		{
			title = songTitle;
			this.difficulty = difficulty;
			this.noteSpeed = noteSpeed;
			foreach (var chart in playerCharts)
			{
				this.playerCharts.Add(chart);
			}
			this.audiopath = audio;
			this.songLength = length;
		}
	}
	
}

