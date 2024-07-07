using Godot;
using System;

public partial class menu_button : Button
{
	[Export] public String parentFunctionId;
	[Export] public String hoverFunctionId;
	[Export] public String labelText;
	[Export] public int minimumWidth = 250;
	[Export] public bool stayHovered = false;
	
	private AnimationPlayer animNode;
	private Label label;
	private Node parent;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animNode = (AnimationPlayer)GetNode("animplayer");
		CustomMinimumSize = new Vector2(minimumWidth, 70);
		Size = new Vector2(minimumWidth, 70);
		label = (Label)GetNode("label");
		label.Text = labelText;
		parent = GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_mouse_entered()
	{
		animNode.Play("select");
		parent.Call("hover_function", hoverFunctionId);
		if (stayHovered)
		{
			parent.Call("unhover_others", GetIndex());
		}
	}

	public void _on_mouse_exited()
	{
		if (stayHovered)
		{
			animNode.Play("deselect_stay");
		}
		else
		{
			animNode.Play("deselect");
		}
		
	}

	public void _on_pressed()
	{
		parent.Call("call_function", parentFunctionId);
	}
}
