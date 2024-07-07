using Godot;
using System;
using System.Collections.Generic;

public partial class playernotes : Control
{
	private Container pressIndicators;
	private notes_spawner songNotes;
	private note_menu noteMenu;
	
	private int perfectWindow = 6;
	private int goodWindow = 12;
	[Export] public float noteSpeed = 2.88f;
	public List<bool> holdDownReq = new List<bool>();
	
	private PackedScene PROMPT_INDICATOR = ResourceLoader.Load("res://scenes/hit_indicator.tscn") as PackedScene;

	[Export] public String songSheet;
	[Export] public bool affectGlobalStats = true;
	[Export] public int localPlayerID = 1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for(int i=0; i<5; i++) holdDownReq.Add(false);
		var keyColor = Color.FromHsv((float)0, 0, 1);
		
		for(int i=1; i<GetNode("container").GetChildCount(); i++)
		{
			var note = (TextureRect)GetNode("container").GetChild(i);
			var notePressed = (TextureRect)GetNode("container_pressed").GetChild(i);
			note.Modulate = keyColor;
			if (i != 3)
			{
				var noteLabel = note.GetChild<Label>(0);
				noteLabel.Text = "" + settings.keys[i - 1][4];
				noteLabel = notePressed.GetChild<Label>(0);
				noteLabel.Text = "" + settings.keys[i - 1][4];
			}
			notePressed.Modulate = keyColor;
		}

		pressIndicators = (Container)GetNode("container_pressed");
		songNotes = (notes_spawner)GetNode("notes");
		noteMenu = (note_menu)GetParent().GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		for(int i=0; i<pressIndicators.GetChildCount()-1; i++)
		{
			var indicator = (TextureRect)pressIndicators.GetChild(i+1);
			
			if (Input.IsActionPressed(settings.keys[i]))
			{
				// VISUAL
				indicator.SelfModulate = Color.FromHsv(0,0,1,(float)0.8);
				// FUNCTIONAL
				if (holdDownReq[i])
					noteMenu.holdNote();
			}
			else
			{
				indicator.SelfModulate = Color.FromHsv(0,0,1,0);
				// FUNCTIONAL
				if (holdDownReq[i])
				{
					noteMenu.noteHit(0, "Miss");
					holdDownReq[i] = false;
					GD.Print("Let go too early");
				}
			}
			// FUNCTIONAL
			if (Input.IsActionJustPressed(settings.keys[i]))
			{
				note noteObj = null;
				if (songNotes.GetChild(i).GetChildCount() != 0)
				{
					noteObj = (note)songNotes.GetChild(i).GetChild(0);
					
					// NOTE HIT: GENERAL
					if (-goodWindow <= noteObj.lifetime && noteObj.lifetime <= goodWindow)
					{
						if (noteObj.noteType == "Hold_START")
						{
							holdDownReq[noteObj.row] = true;
						}
					}
						
					// NOTE HIT: SPECIFIC TIMING
					if (-perfectWindow <= noteObj.lifetime && noteObj.lifetime <= perfectWindow && noteObj.noteType != "Hold_END")
					{
						noteMenu.noteHit(noteObj.lifetime, "Perfect");
						if (i == 2)
						{
							createIndicator(indicator.Position, true);
						}
						else
						{
							createIndicator(indicator.Position);
						}
						noteObj.QueueFree();
					}
					else
					{
						if (-goodWindow <= noteObj.lifetime && noteObj.lifetime <= goodWindow)
						{
							noteMenu.noteHit(noteObj.lifetime, "Good");
							noteObj.QueueFree();
						}
						else
						{
							if (settings.ghostTapping) noteMenu.noteHit(0, "Miss");
						}
					}
				}
				else
				{
					if (settings.ghostTapping) noteMenu.noteHit(0, "Miss");
				}
			}
			
		}
		if (Input.IsActionJustPressed("key_SHIFT"))
		{
			GD.Print("Current Song Time: "+(songNotes.songTime-250));
		}
		if (Input.IsActionJustPressed("key_ESC"))
		{
			//GetTree().ReloadCurrentScene();
			GetTree().ChangeSceneToFile("res://scenes/song_select.tscn");
		}
	}

	public void createIndicator(Vector2 position, bool isSpaceNote = false)
	{
		var prompt = (Control)PROMPT_INDICATOR.Instantiate();
		if (isSpaceNote)
		{
			prompt.Position = new Vector2(position.X+26, position.Y-prompt.Size.Y/2);
			var child1 = (TextureRect)prompt.GetChild(0);
			var child2 = (TextureRect)prompt.GetChild(1);
			child1.Visible = false;
			child2.Visible = true;
		}
		else
		{
			prompt.Position = new Vector2(position.X, position.Y-prompt.Size.Y/2);
		}
		
		GetNode("indicators").AddChild(prompt);
	}
}
