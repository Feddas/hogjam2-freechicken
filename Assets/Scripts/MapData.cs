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
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xx                       xx",
			"xx  x                    xx",
			"xx                  A    xx",
			"xx                xxxx   xx",
			"xx          A            2x",
			"xx         xxx         xxxx",
			"x1         xxx           xx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
		}
	};

	public static readonly Dictionary<string, MapData> RAW_LEVEL_DATA = new Dictionary<string, MapData>() {
		{ "level_1", LEVEL_1 },
	};
}
