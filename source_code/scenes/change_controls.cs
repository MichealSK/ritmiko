using Godot;
using System;

public partial class change_controls : ColorRect
{
	public int keyID = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Visible)
		{
			if (Input.IsActionJustPressed("key_A"))
				settings.keys[keyID] = "key_A";
			if (Input.IsActionJustPressed("key_B"))
				settings.keys[keyID] = "key_B";
			if (Input.IsActionJustPressed("key_C"))
				settings.keys[keyID] = "key_C";
			if (Input.IsActionJustPressed("key_D"))
				settings.keys[keyID] = "key_D";
			if (Input.IsActionJustPressed("key_E"))
				settings.keys[keyID] = "key_E";
			if (Input.IsActionJustPressed("key_F"))
				settings.keys[keyID] = "key_F";
			if (Input.IsActionJustPressed("key_G"))
				settings.keys[keyID] = "key_G";
			if (Input.IsActionJustPressed("key_H"))
				settings.keys[keyID] = "key_H";
			if (Input.IsActionJustPressed("key_I"))
				settings.keys[keyID] = "key_I";
			if (Input.IsActionJustPressed("key_J"))
				settings.keys[keyID] = "key_J";
			if (Input.IsActionJustPressed("key_K"))
				settings.keys[keyID] = "key_K";
			if (Input.IsActionJustPressed("key_L"))
				settings.keys[keyID] = "key_L";
			if (Input.IsActionJustPressed("key_M"))
				settings.keys[keyID] = "key_M";
			if (Input.IsActionJustPressed("key_N"))
				settings.keys[keyID] = "key_N";
			if (Input.IsActionJustPressed("key_O"))
				settings.keys[keyID] = "key_O";
			if (Input.IsActionJustPressed("key_P"))
				settings.keys[keyID] = "key_P";
			if (Input.IsActionJustPressed("key_Q"))
				settings.keys[keyID] = "key_Q";
			if (Input.IsActionJustPressed("key_R"))
				settings.keys[keyID] = "key_R";
			if (Input.IsActionJustPressed("key_S"))
				settings.keys[keyID] = "key_S";
			if (Input.IsActionJustPressed("key_T"))
				settings.keys[keyID] = "key_T";
			if (Input.IsActionJustPressed("key_U"))
				settings.keys[keyID] = "key_U";
			if (Input.IsActionJustPressed("key_V"))
				settings.keys[keyID] = "key_V";
			if (Input.IsActionJustPressed("key_W"))
				settings.keys[keyID] = "key_W";
			if (Input.IsActionJustPressed("key_X"))
				settings.keys[keyID] = "key_X";
			if (Input.IsActionJustPressed("key_Y"))
				settings.keys[keyID] = "key_Y";
			if (Input.IsActionJustPressed("key_Z"))
				settings.keys[keyID] = "key_Z";

			if (Input.IsActionPressed(settings.keys[keyID]))
			{
				settings.save_settings();
				Visible = false;
				GetTree().ReloadCurrentScene();
			}
				
		}
	}
}
