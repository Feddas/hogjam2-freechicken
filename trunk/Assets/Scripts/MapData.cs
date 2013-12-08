using UnityEngine;
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
			"2:level_2:1+"
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
			"2:level_2:*"
		},
	};

	public static readonly Dictionary<string, MapData> RAW_LEVEL_DATA = new Dictionary<string, MapData>() {
		{ "level_1", LEVEL_1 },
		{ "level_2", LEVEL_2 },
	};
}
