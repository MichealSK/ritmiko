using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using Array = Godot.Collections.Array;

public partial class song_selection : VBoxContainer
{
	// Called when the node enters the scene tree for the first time.

	private Label titleNode;
	private Label statsNode;
	private Label difficultyNode;
	private Label rankNode;
		
	public override void _Ready()
	{
		titleNode = (Label)GetParent().GetParent().GetNode("song_title");
		statsNode = (Label)titleNode.GetChild(0);
		difficultyNode = (Label)titleNode.GetChild(1);
		rankNode = (Label)titleNode.GetChild(2);

		titleNode.Text = "";
		rankNode.Text = "";
		difficultyNode.Text = "";
		statsNode.Text = "";

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void call_function(String id)
	{
		settings.currentSongID = id;
		GetTree().ChangeSceneToFile("res://scenes/note_menu.tscn");
	}

	public void hover_function(String id)
	{
		titleNode.Text = settings.songData[id].title;
		switch (settings.songData[id].difficulty)
		{
			case 1:
				difficultyNode.Text = "Тежина: ★☆☆☆☆";
				break;
			case 2:
				difficultyNode.Text = "Тежина: ★★☆☆☆";
				break;
			case 3:
				difficultyNode.Text = "Тежина: ★★★☆☆";
				break;
			case 4:
				difficultyNode.Text = "Тежина: ★★★★☆";
				break;
			case 5:
				difficultyNode.Text = "Тежина: ★★★★★";
				break;
		}
		
		if (settings.songResults.ContainsKey(id))
		{
			Array<float> statList = (Array<float>)settings.songResults[id];
			statsNode.Text = "Поени: " + statList[0] + "\nТочност: " + statList[1] + "%\nНајдолго Combo: " + statList[2];
			
			if (statList[1] <= 50)
			{
				rankNode.Text = "Оцена: D";
				rankNode.Modulate = Colors.Gray;
			}
			if (statList[1] > 50)
			{
				rankNode.Text = "Оцена: C";
				rankNode.Modulate = Colors.Lime;
			}
			if (statList[1] > 60)
			{
				rankNode.Text = "Оцена: B";
				rankNode.Modulate = Colors.Blue;
			}
			if (statList[1] > 70)
			{
				rankNode.Text = "Оцена: A";
				rankNode.Modulate = Colors.Red;
			}
			if (statList[1] > 80)
			{
				rankNode.Text = "Оцена: S";
				rankNode.Modulate = Colors.Purple;
			}
			if (statList[1] > 90)
			{
				rankNode.Text = "Оцена: SS";
				rankNode.Modulate = Colors.Gold;
			}
		}
		else
		{
			statsNode.Text = "Поени: 0\nТочност: 0%\nНајдолго Combo: 0";
			rankNode.Text = "Оцена: N/A";
			rankNode.Modulate = Colors.White;
		}
	}

	public void unhover_others(int id)
	{
		foreach (menu_button child in GetChildren())
		{
			if (child.GetIndex() != id)
			{
				var animPlayer = (AnimationPlayer)child.GetChild(1);
				animPlayer.Play("deselect");
			}
		}
	}
}
