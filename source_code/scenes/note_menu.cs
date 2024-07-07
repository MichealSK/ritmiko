using Godot;
using System;
using System.Collections.Generic;

public partial class note_menu : Control
{
	private ProgressBar health;
	private Label statistics;
	private Label comboLabel;
	private Label scoreLabel;
	private Control prompts;
	private AudioStreamPlayer songPlayer;
	private int baseACCGain = 25;

	private float accuracy = 0;
	private float accuracyMax = 0;
	private int perfects = 0;
	private int hits = 0;
	private int misses = 0;
	private int combo = 0;
	private int longestCombo = 0;
	private float score = 0;
	private int songTime = 0;
	private int songLength = 64000;


	private PackedScene PROMPT_OBJ = ResourceLoader.Load("res://scenes/prompt.tscn") as PackedScene;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = (ProgressBar)GetNode("hp");
		statistics = (Label)GetNode("stats");
		comboLabel = (Label)GetNode("combo");
		scoreLabel = (Label)GetNode("score");
		prompts = (Control)GetNode("prompts");
		songPlayer = (AudioStreamPlayer)GetNode("321GO/song");

		var players = GetNode("players");
		for(int i=0; i<players.GetChildCount(); i++)
		{
			var player = (playernotes)players.GetChild(i);
			player.songSheet = settings.songData[settings.currentSongID].playerCharts[i];
			player.noteSpeed = settings.songData[settings.currentSongID].noteSpeed;
			songLength = settings.songData[settings.currentSongID].songLength;
			var spawner = (notes_spawner)player.GetChild(2);
			spawner._Ready();
		}

		songPlayer.Stream = ResourceLoader.Load("res://sound/" + settings.songData[settings.currentSongID].audiopath + ".mp3") as AudioStream;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		songTime++;
		if (songTime > songLength)
		{
			Godot.Collections.Array<float> list = new Godot.Collections.Array<float>();
			list.Add((float)Math.Floor(score));
			list.Add((float)Math.Floor(accuracy/accuracyMax*100));
			list.Add(longestCombo);
			settings.save_file(settings.currentSongID, list);
			GetTree().ChangeSceneToFile("res://scenes/song_select.tscn");
		}
	}

	public void noteHit(int lifetime = 0, String result = "Perfect")
	{
		if (result != "Miss")
		{
			accuracy += baseACCGain - Math.Abs(lifetime);
			combo++;
			if (combo > longestCombo) longestCombo = combo;
			var animPlayer = (AnimationPlayer)GetNode("combo/animplayer");
			animPlayer.Play("RESET");
			animPlayer.Play("bop");
		}
		else
		{
			accuracy += 0;
			combo = 0;
		}
		
		accuracyMax += baseACCGain;
		
		switch (result)
		{
			case "Perfect":
				perfects++;
				createPrompt("Perfect!", Colors.Gold);
				health.Value += 7;
				score += 75;
				break;
			case "Good":
				hits++;
				createPrompt("Good!", Colors.Green);
				health.Value += 4;
				score += 50;
				break;
			case "Miss":
				createPrompt("Miss!", Colors.Red);
				misses++;
				health.Value -= 10;
				score -= 25;
				break;
		}

		if (health.Value <= 0 && settings.invincibility == false)
		{
			GetTree().ChangeSceneToFile("res://scenes/game_over_screen.tscn");
		}
		updateStatistics();
	}

	public void updateStatistics()
	{
		statistics.Text = "Точност: "+ Math.Floor(accuracy/accuracyMax*100) +"%\n" +
		                  "Perfect: "+ perfects +"\n" +
		                  "Hit: "+ hits +"\n" +
		                  "Miss: "+ misses +"\n" +
		                  "Најдолго Combo: "+ longestCombo +"\n";
		comboLabel.Text = "Combo: " + combo;
		scoreLabel.Text = "Поени: " + Math.Floor(score);
	}

	public void createPrompt(String text, Color color)
	{
		var prompt = (Control)PROMPT_OBJ.Instantiate();
		prompt.Modulate = color;
		var textLabel = prompt.GetChild(0) as Label;
		textLabel.Text = text;
		GetNode("prompts").AddChild(prompt);
	}

	public void holdNote()
	{
		score += 0.1f;
		updateStatistics();
	}
	
}
