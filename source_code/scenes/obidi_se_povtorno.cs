using Godot;
using System;

public partial class obidi_se_povtorno : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public async void call_function(String id)
	{
		switch (id)
		{
			case "songmenu":
				GetTree().ChangeSceneToFile("res://scenes/note_menu.tscn");
				break;
		}
	}
	
	public void hover_function(String id) {}
}
