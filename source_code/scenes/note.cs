using Godot;
using System;

public partial class note : Control
{
	public int lifetime = 250;
	public float speed = 2.88f;
	public String noteType = "Normal";
	public int row = 0;

	public playernotes PlayerNotes;
	public note_menu NoteMenu;
	
	/* NOTE TYPES:
	 * 1: Normal
	 * 2: Hold_START
	 * 3: Hold_MID
	 * 4: Hold_END
	 */
	
	public override void _Ready()
	{
		PlayerNotes = (playernotes)GetParent().GetParent().GetParent();
		NoteMenu = (note_menu)PlayerNotes.GetParent().GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = new Vector2(Position.X, Position.Y - speed);
		lifetime -= 1;
		if (noteType == "Hold_MID" || noteType == "Hold_END")
		{
			if (lifetime == 0)
			{
				if (noteType == "Hold_END")
				{
					if (PlayerNotes.holdDownReq[row])
					{
						PlayerNotes.holdDownReq[row] = false;
						NoteMenu.noteHit();
					}
				}
				QueueFree();
			}
		}
		else
		{
			if (lifetime == -13)
			{
				var menu = GetParent().GetParent().GetParent().GetParent().GetParent() as note_menu;
				switch (noteType)
				{
					
					default:
						menu.noteHit(0, "Miss");
						break;
				}
				QueueFree();
			}
		}
	}
}
