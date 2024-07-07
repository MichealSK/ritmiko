using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FileAccess = Godot.FileAccess;

public partial class charter : Control
{
	[Export] public bool active = false;
	[Export] public int chartingLatency = 8;
	[Export] public bool halfTime = false;
	
	private List<int> holdTimes = new List<int>();
	private bool half = true;

	//private song_chart songChart = new song_chart();
	private Dictionary<int, List<int>> songChart = new Dictionary<int, List<int>>();
	private int songTime = -250;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for(int i=0; i<5; i++) holdTimes.Add(0);
		songTime = -250;
		songChart = new Dictionary<int, List<int>>();
		if (active)
		{
			GD.Print("New Chart Started");
			var player = (playernotes)GetParent().GetChild(1).GetChild(0);
			player.songSheet = "export1";
			player.GetChild(2)._Ready();
		}
		if (halfTime)
		{
			var parent = (note_menu)GetParent();
			var songAudio = (AudioStreamPlayer)parent.GetChild(8).GetChild(0);
			songAudio.PitchScale = 0.5f;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (halfTime)
		{
			if (half)
			{
				songTime++;
				half = false;
			}
			else
			{
				half = true;
			}
		}
		else
		{
			songTime++;
		}
		
		if (active)
		{
			List<int> entry = new List<int>();
			for (int i = 0; i < 5; i++)
			{
				if (Input.IsActionJustPressed(settings.keys[i]))
				{
					entry.Add(1);
				}
				else
				{
					entry.Add(0);
				}
			}
			
			for (int i = 0; i < 5; i++)
			{
				if (Input.IsActionPressed(settings.keys[i]))
				{
					holdTimes[i]++;
					if (holdTimes[i] == 60)
					{
						songChart[songTime - 309 - chartingLatency][i] = 2;
						GD.Print("Hold Note");
					}
				}
				if (Input.IsActionJustReleased(settings.keys[i]))
				{
					if (holdTimes[i] > 60)
					{
						entry[i] = 4;
					}
					holdTimes[i] = 0;
				} 
			}
			
			if(!(entry[0] == 0 && entry[1] == 0 && entry[2] == 0
			     && entry[3] == 0 && entry[4] == 0)) songChart.Add(songTime - 250 - chartingLatency, entry);
			
			if (Input.IsActionJustPressed("key_ENTER"))
			{
				var newSongMap = JsonSerializer.Serialize(songChart);
				var chart = FileAccess.Open("res://songs_charts/export.txt", FileAccess.ModeFlags.Write);
				chart.StoreVar(newSongMap);
			}
		}
	}
}
