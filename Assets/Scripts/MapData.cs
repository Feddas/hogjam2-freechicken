using UnityEngine;
using System.Collections.Generic;

public class MapData
{

	public MapData()
	{

	}

	public string[] Tiles { get; set; }

	public static readonly MapData LEVEL_1 = new MapData()
	{
		Tiles = new string[] {
			"xxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxx",
			"x                       x",
			"x  x                    x",
			"x                       x",
			"x                       x",
			"x                       x",
			"x         xxx           x",
			"x         xxx           x",
			"xxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxx",
		}
	};

	public static readonly Dictionary<string, MapData> RAW_LEVEL_DATA = new Dictionary<string, MapData>() {
		{ "level_1", LEVEL_1 },
	};
}
