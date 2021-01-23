using UnityEngine;
using System.Collections;



public static class M
{
	public static bool setSound = true;
	public static bool isAds = true;
	public static int []STARS = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};
	public static int []UPGEDE = new int[]{0,0,0,0,0};
	public static int []GRANNYCOST = new int[]{0,5999,8999,7999,9999,6999};
	public static string[] NAME = new string[] { "HOMEBODY", "IMPOVERISHED", "LANKY", "MORBIDLY OBESE", "EUROPEAN", "MIDGET" };

	public static int[] SKICOST = new int[] { 0, 20000, 50000, 20000, 50000, 5999 };
	public static string[] SKINAME = new string[]   { "ROSSIGNO"    , "VOLKL RTM"     , "Atomic Vantage","KIT DECO 100%","Nitro Prime", "MIDGET" };
	public static string[] SKISUBNAME = new string[]{ "MENS PURSUIT", "8.0 Green Skis", " 90 CTI Skis"  , "Custom Rider Skis", "Snowboard" , "MIDGET" };
	public static int UNLOCKLEVEL = 1;
	public static int COINS = 89999;
	public static float coinPower = 500;
	public static float starPower = 0;
	public static float MAXPCOIN = 1;
	public static float MAXPSTAR = 1;
	public const float MAXPOWER = 500;
	public static int GCOIN = 0;
	public static int GSOCRE = 0;
	public static float SPD = -.1f;
	public static int GameScreen;
	public const int GAMEMENU = 0;
	public const int GAMEGRANY = 1;
	public const int GAMEBUY = 2;
	public const int GAMESKIUP = 3;
	public const int GAMEACHIV = 4;
	public const int GAMESKI = 5;
	public  const int GAMEPLAY = 6;
	public const int GAMEOVER = 7;
	public const int GAMEPAUSE = 8;
	public static int ParNO = 0;
	public static int MATNO = 0;
	public static int PNO = 0;
	public static int SKINO = 0;
	public static void Save ()
	{
		
		PlayerPrefs.SetInt ("a", setSound ? 1 :0);
		PlayerPrefs.SetInt ("b", isAds ? 1 :0);

		for (int i = 0; i < STARS.GetLength(0); i++) 
			PlayerPrefs.SetInt ("c"+i, STARS[i]);
		for (int i = 0; i < UPGEDE.GetLength(0); i++)
			PlayerPrefs.SetInt ("d"+i, UPGEDE[i]);
		for (int i = 0; i < GRANNYCOST.GetLength(0); i++)
			PlayerPrefs.SetInt ("e"+i, GRANNYCOST[i]);

		for (int i = 0; i < SKICOST.GetLength(0); i++)
			PlayerPrefs.SetInt("0ee" + i, SKICOST[i]);

		PlayerPrefs.SetInt ("f", UNLOCKLEVEL);

		PlayerPrefs.SetInt ("g", COINS);

		Debug.Log ("Save - >"+UNLOCKLEVEL);

	}

	public static void Open ()
	{
		PlayerPrefs.DeleteAll();
		setSound = PlayerPrefs.GetInt ("a", 1) == 1;
		isAds = PlayerPrefs.GetInt ("b", 1) ==1;

		for (int i = 0; i < STARS.GetLength(0); i++) 
			STARS[i] = PlayerPrefs.GetInt ("c"+i, STARS[i]);
		for (int i = 0; i < UPGEDE.GetLength(0); i++)
			UPGEDE[i] = PlayerPrefs.GetInt ("d"+i, UPGEDE[i]);
		for (int i = 0; i < GRANNYCOST.GetLength(0); i++)
			GRANNYCOST[i] = PlayerPrefs.GetInt ("e"+i, GRANNYCOST[i]);
		for (int i = 0; i < SKICOST.GetLength(0); i++)
			SKICOST[i] = PlayerPrefs.GetInt("0ee" + i, SKICOST[i]);
		UNLOCKLEVEL = PlayerPrefs.GetInt ("f", UNLOCKLEVEL);
		COINS = PlayerPrefs.GetInt ("g", COINS);
		Debug.Log ("Open - >"+UNLOCKLEVEL);
	}
	public static bool Rect2RectIntersection(float ax, float ay, float adx, float ady, float bx, float by, float bdx, float bdy)
	{
		ax -= adx / 2;
		ay += ady / 2;
		bx -= bdx / 2;
		by += bdy / 2;
		if (ax + adx > bx && ay - ady < by && bx + bdx > ax && by - bdy < ay)
		{
			return true;
		}
		return false;
	}
}

/*
 * 
 */
