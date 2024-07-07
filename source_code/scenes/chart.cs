using Godot;
using System;
using System.Collections.Generic;

public partial class song_chart : Resource
{
	public Dictionary<int, List<int>> chart = new Dictionary<int, List<int>>();

	public song_chart()
	{
		
	}
	public song_chart(Dictionary<int, List<int>> songChart)
	{
		chart = songChart;
	}
}
