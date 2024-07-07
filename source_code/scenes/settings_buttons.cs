using Godot;
using System;

public partial class settings_buttons : Control
{
	public Control labels;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Name == "settings_buttons")
		{
			labels = (Control)GetParent().GetNode("labels");
			labels.GetChild<Label>(0).Text = ""+settings.keys[0][4];
			labels.GetChild<Label>(1).Text = ""+settings.keys[1][4];
			labels.GetChild<Label>(2).Text = ""+settings.keys[3][4];
			labels.GetChild<Label>(3).Text = ""+settings.keys[4][4];
			if (settings.ghostTapping)
				labels.GetChild<Label>(6).Text = "On";
			else
				labels.GetChild<Label>(6).Text = "Off";
			if (settings.invincibility)
				labels.GetChild<Label>(7).Text = "On";
			else
				labels.GetChild<Label>(7).Text = "Off";
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public async void call_function(String id)
	{
		switch (id)
		{
			case "note1":
				GetParent().GetNode<change_controls>("press_key").keyID = 0;
				GetParent().GetNode<change_controls>("press_key").Visible = true;
				break;
			case "note2":
				GetParent().GetNode<change_controls>("press_key").keyID = 1;
				GetParent().GetNode<change_controls>("press_key").Visible = true;
				break;
			case "note4":
				GetParent().GetNode<change_controls>("press_key").keyID = 3;
				GetParent().GetNode<change_controls>("press_key").Visible = true;
				break;
			case "note5":
				GetParent().GetNode<change_controls>("press_key").keyID = 4;
				GetParent().GetNode<change_controls>("press_key").Visible = true;
				break;
			case "mainmenu":
				GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
				break;
			case "toggleGhost":
				settings.ghostTapping = !settings.ghostTapping;
				settings.save_settings();
				break;
			case "toggleInvincibility":
				settings.invincibility = !settings.invincibility;
				settings.save_settings();
				break;
			case "deletesavedata":
				GetChild<menu_button>(7).Position = GetChild<menu_button>(6).Position;
				GetChild<menu_button>(6).Position = new Vector2(1000000f, 1000000f);
				break;
			case "deletesavedataSURE":
				settings.keys = new Godot.Collections.Array<String>{"key_D", "key_F", "key_SPACE", "key_J", "key_K"};
				settings.ghostTapping = false;
				settings.songResults = new Godot.Collections.Dictionary<String, Godot.Collections.Array<float>>();
				settings.save_settings();
				settings.save_file();
				GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
				break;
		}
		_Ready();
	}
	
	public void hover_function(String id) {}
}
