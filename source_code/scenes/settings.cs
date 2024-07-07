using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Ritmiko;
using FileAccess = Godot.FileAccess;

public partial class settings : Node
{
	public static Godot.Collections.Array<String> keys = new Godot.Collections.Array<String>{"key_D", "key_F", "key_SPACE", "key_J", "key_K"};

	public static Godot.Collections.Dictionary<String, Godot.Collections.Array<float>> songResults =
		new Godot.Collections.Dictionary<String, Godot.Collections.Array<float>>();
	public static bool ghostTapping = false;
	public static bool invincibility = false;

	public static String currentSongID;
	public static Dictionary<String, song_data> songData = new Dictionary<String, song_data>();
	public static Color[] keyColors = new[] {Colors.LightYellow, Colors.Orange, Colors.Red, Colors.Aqua, Colors.Magenta};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Adding Song Data
		songData.Add("makedonsko_devojche", new song_data("Македонско Девојче", 3, 2.88f, new []{"makedonsko_devojche"}, "makedonsko_devojche", 32000));
		songData.Add("stani_mome", new song_data("Стани Моме да Заиграш", 5, 4.4f, new []{"stani_mome"}, "nashe_oro", 26000));
		songData.Add("nalej_nalej", new song_data("Налеј, Налеј", 3, 3.8f, new []{"nalej_nalej"}, "nalej_nalej", 29000));
		
		load_file();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void save_settings()
	{
		var keyControls = Json.Stringify(keys);
		var ghostTappingSave = Json.Stringify(ghostTapping);
		var invincibilitySave = Json.Stringify(invincibility);
		using var controlfile = FileAccess.Open("user://controlsSetup.save", FileAccess.ModeFlags.Write);
		controlfile.StoreLine(keyControls);
		controlfile.StoreLine(ghostTappingSave);
		controlfile.StoreLine(invincibilitySave);
		controlfile.Close();
	}
	
	public static void save_file(String song = "", Godot.Collections.Array<float> data = null)
	{
		if (song != "")
		{
			if (songResults.ContainsKey(song))
			{
				if (data[1] > songResults[song][1])
					songResults[song] = data;
			}
			else
			{
				songResults.Add(song, data);
			}
		}

		var songResultsSave = Json.Stringify(songResults);
		using var savefile = FileAccess.Open("user://songResults.save", FileAccess.ModeFlags.Write);
		savefile.StoreLine(songResultsSave);
		savefile.Close();
	}

	public static void load_file()
	{
		if (FileAccess.FileExists("user://songResults.save"))
		{
			var saveFile = FileAccess.Open("user://songResults.save", FileAccess.ModeFlags.Read);
			var jsonString = saveFile.GetLine();
			var parseResult = Json.ParseString(jsonString);
			songResults = (Godot.Collections.Dictionary<String, Godot.Collections.Array<float>>)parseResult;
			saveFile.Close();
		}

		if (FileAccess.FileExists("user://controlsSetup.save"))
		{
			var controlFile = FileAccess.Open("user://controlsSetup.save", FileAccess.ModeFlags.Read);
			var jsonString = controlFile.GetLine();
			var parseResult = Json.ParseString(jsonString);
			keys = (Godot.Collections.Array<String>)parseResult;
			jsonString = controlFile.GetLine();
			parseResult = Json.ParseString(jsonString);
			ghostTapping = (bool)parseResult;
			jsonString = controlFile.GetLine();
			parseResult = Json.ParseString(jsonString);
			invincibility = (bool)parseResult;
			controlFile.Close();
		}
	}

	public static void delete_data()
	{
		if (FileAccess.FileExists("user://controlsSetup.save"))
		{
			FileAccess file1 = FileAccess.Open("user://controlsSetup.save", FileAccess.ModeFlags.ReadWrite);
			string filePath = file1.GetPath();
			Directory.Delete(filePath);
			file1.Close();
		}

		if (FileAccess.FileExists("user://controlsSetup.save"))
		{
			FileAccess file2 = FileAccess.Open("user://songResults.save", FileAccess.ModeFlags.ReadWrite);
			string filePath = file2.GetPath();
			Directory.Delete(filePath);
			file2.Close();
		}
	}
}
