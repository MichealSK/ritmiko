using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class main_menu_buttons : Control
{
	private AnimationPlayer animPlayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animPlayer = (AnimationPlayer)GetParent().GetChild(1).GetChild(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void call_function(String id)
	{
		animPlayer.Play("fadein");
		switch (id)
		{
			case "song_select":
				GetTree().ChangeSceneToFile("res://scenes/song_select.tscn");
				break;
			case "Settings":
				GetTree().ChangeSceneToFile("res://scenes/setting_screen.tscn");
				break;
			case "exit":
				GetTree().Quit();
				break;
		}
	}

	public void hover_function(String id) {}
}
