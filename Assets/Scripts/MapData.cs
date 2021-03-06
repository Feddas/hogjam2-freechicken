﻿using UnityEngine;
using System.Collections.Generic;

public class MapData
{

	public MapData()
	{
		this.TargetDoorInfo = new Dictionary<int, string>();
		this.DoorLookup = new Dictionary<int, string>();
	}

	public string[] Tiles { get; set; }

	public Dictionary<int, string> DoorLookup { get; private set; }
	public Dictionary<int, string> TargetDoorInfo { get; private set; }
	public string[] Doors
	{
		get { return null; } // OMGHAX
		set
		{
			foreach (string door in value)
			{
				string[] parts = door.Split(':');
				int num = int.Parse(parts[0]);
				string target = parts[1].Trim();
				this.DoorLookup[num] = target;
				this.TargetDoorInfo[num] = parts[2].Trim();
			}
		}
	}
	/*	Tile ID's:
	 *		x = wall
	 *		w = wall 2
	 *		[space] = empty space
	 *		1 through 9 = doors (define targets below)
	 *		A = Alien
	 *		
	 * Door format:
	 *		door ID, colon, target level name, colon, target door ID, plus or minus to appear on the right or left of the target door
	 *		
	 *		If the door target door ID is just * that's the final exit to trigger victory
	 */

	public static readonly MapData LEVEL_1 = new MapData()
	{
		Tiles = new string[] {
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xx                       xx",
			"xx  x                    xx",
			"xx                  A    xx",
			"xx                wwww   xx",
			"xx          A            2x",
			"xx         www         wwwx",
			"x1         xxx           xx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
		},
		Doors = new string[] { 
			"2:level_2:1+",
		},
	};

	public static readonly MapData LEVEL_2 = new MapData()
	{
		Tiles = new string[] {
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xx                       xx",
			"xx  wwww                 xx",
			"xx                  A    xx",
			"xx                xxxx   xx",
			"xx          A            2x",
			"xx         xxx         xxxx",
			"x1         xxx           xx",
			"xxxxxx  xxxxxxxxxxxxxxxxxxx",
			"xxxxxx  xxxxxxxxxxxxxxxxxxx",
			"xxxxxx  xxxxxxxxxxxxxxxxxxx",
			"xxxxxx  xxxxxxxxxxxxxxxxxxx",
		},
		Doors = new string[] { 
			"1:level_1:2-",
			"2:level_3:1+"
		},
	};

	public static readonly MapData LEVEL_3 = new MapData()
	{
		Tiles = new string[] {
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xx              x        xx",
			"xx  xxxxxxxxx   x        xx",
			"xx          x   x        xx",
			"xxxxxxxxxx  x  xx        3x",
			"xx          x         xxxxx",
			"xx   xxxxxxxxwwwww       xx",
			"xx          x            xx",
			"xxxxxxxxx   x  A         xx",
			"x1      x   xxxxxxxx     2x",
			"xxxx    x          xxxxxxxx",
			"xxxx    xxxxxxxxx  xxxxxxxx",
			"xxxx               xxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
		},
		Doors = new string[] { 
			"1:level_2:2-",
			"2:level_4:1+",
			"3:level_1:1+",
		},
	};

	public static readonly MapData LEVEL_4 = new MapData()
	{
		Tiles = new string[] {
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xx              x        xx",
			"xx  xxxxxxxxx   x        xx",
			"xx          x   x        xx",
			"xxxxxxxxxx  x  xx        3x",
			"xx          x         xxxxx",
			"xx   xxxxxxxxwwwww       xx",
			"xx          x            xx",
			"xxxxxxxxx   x  A         xx",
			"x1      x   xxxxxxxx     2x",
			"xxxx    x          xxxxxxxx",
			"xxxx    xxxxxxxxx  xxxxxxxx",
			"xxxx               xxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
		},
		Doors = new string[] { 
			"1:level_3:2-",
			"2:level_3:1+",
			"3:level_4:*",
		},
	};

	public static readonly MapData LEVEL_5 = new MapData()
	{
		Tiles = new string[] {
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xx                       xx",
			"xx  x                    xx",
			"xx                  A    xx",
			"xx                wwww   xx",
			"xx          A            2x",
			"xx         www         wwwx",
			"x1         xxx           xx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
			"xxxxxxxxxxxxxxxxxxxxxxxxxxx",
		},
		Doors = new string[] { 
			"2:level_2:1+"
		},
	};

	public static readonly Dictionary<string, MapData> RAW_LEVEL_DATA = new Dictionary<string, MapData>() {
		{ "level_1", LEVEL_1 },
		{ "level_2", LEVEL_2 },
		{ "level_3", LEVEL_3 },
		{ "level_4", LEVEL_4 },
		{ "level_5", LEVEL_5 },
	};
}
