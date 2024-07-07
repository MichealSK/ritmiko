using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FileAccess = Godot.FileAccess;

public partial class notes_spawner : Control
{
	public float noteSpeed;
	public int songTime = -250;
	private song_chart SONG_CHART;
	private Dictionary<int, List<int>> SONG = new Dictionary<int, List<int>>();

	private List<bool> continuousSpawn = new List<bool>();
	
	private PackedScene NOTE_OBJ = ResourceLoader.Load("res://scenes/note.tscn") as PackedScene;
	private playernotes parent;

	public static void helloworld()
	{
		GD.Print("Hello world");
	}

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = (playernotes)GetParent();
		var chartFile = FileAccess.GetFileAsString("res://songs_charts/"+parent.songSheet+".txt"); 
		SONG = JsonSerializer.Deserialize<Dictionary<int, List<int>>>(chartFile);
		for(int i=0; i<5; i++) continuousSpawn.Add(false);
		noteSpeed = parent.noteSpeed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		songTime++;

		if (songTime % Math.Floor(34 / parent.noteSpeed * 1.15) == 0) 
			for (int i = 0; i < 5; i++) if(continuousSpawn[i]) 
				createNote(i, 3);
		
		if (SONG.ContainsKey(songTime))
		{
			List<int> notes = SONG[songTime];
			for (int i = 0; i < 5; i++)
			{
				if(notes[i] != 0) createNote(i, notes[i]);
			}
		}
	}
	
	public void createNote(int noteRow, int noteType = 1)
	{
		var noteObj = (note)NOTE_OBJ.Instantiate();
		noteObj.row = noteRow;
		noteObj.speed = noteSpeed;
			
		var child1 = (TextureRect)noteObj.GetChild(0);
		var child2 = (TextureRect)noteObj.GetChild(1);
		var child3 = (TextureRect)noteObj.GetChild(2);
		
		if (noteRow == 2)
		{
			child1.Visible = false;
			child2.Visible = true;
		}

		switch (noteType)
		{
			case 2:
				noteObj.noteType = "Hold_START";
				continuousSpawn[noteRow] = true;
				break;
			case 3:
				noteObj.noteType = "Hold_MID";
				noteObj.ZIndex = -2;
				child1.Visible = false;
				child2.Visible = false;
				child3.Visible = true;
				break;
			case 4:
				noteObj.noteType = "Hold_END";
				continuousSpawn[noteRow] = false;
				child1.Visible = false;
				child2.Visible = false;
				break;
		}

		noteObj.Modulate = settings.keyColors[noteRow];
		noteObj.Position = new Vector2(noteObj.Position.X, noteSpeed * 250);
		GetChild(noteRow).AddChild(noteObj);
	}
}
