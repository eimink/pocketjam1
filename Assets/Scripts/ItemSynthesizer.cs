using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


public static class ItemSynthesizer {

	public static string[] Items = {
		"Nun",
		"Tentacle",
		"Dildo",
		"Cuffs",
		"Gimp",
		"Alien",
		"Pyramid",
		"Chicken",
		"Donald",
		"Pikachu",
		"Drugs",
		"RealityTV",
		"Pizza",
		"Veggies",
		"Aerobics",
		"Charity",
		"Bono",
		"Cross"
	};

	private static Dictionary<string,string> itemDB;

	private static bool initialized = false;

	private static Dictionary<string,List<string>> synonymDB;

	private static void ReadItemDatabase()
	{
		TextAsset textFile = (TextAsset)Resources.Load("itemdb", typeof(TextAsset));
		var lines = textFile.text.Split('\n');
		foreach ( string line in lines )
		{
			var values = line.Split(',');
			for (int i = 1; i < values.Length; i++)
			{
				Debug.Log(values[i]);
				if (!String.IsNullOrEmpty(values[i]) && !itemDB.ContainsKey(values[i].ToLowerInvariant()))
					itemDB.Add(values[i].ToLowerInvariant(),values[0]);
			}
		}
	}

	private static string findRandomItemFromSynonyms(string input)
	{
		string item;
		if (itemDB.TryGetValue(input.ToLowerInvariant(),out item))
			return item;
		else
			return "";
	}

	private static string findRandomItemFromDB()
	{
		int index = (int)UnityEngine.Random.Range(0,Items.Length);
		return Items[index];
	}

	public static void Initialize()
	{
		itemDB = FileUtil.Read(Config.ItemDataPath);
		ReadItemDatabase();
		if (itemDB == null)
			itemDB = new Dictionary<string, string>();
		initialized = true;
	}

	public static string Synthesize(string seed)
	{
		if (initialized)
		{
			if (itemDB.ContainsKey(seed))
				return itemDB[seed];
			else
			{
				string item = findRandomItemFromSynonyms(seed);
				if (!String.IsNullOrEmpty(item))
				{
					itemDB.Add(seed,item);
					FileUtil.Write(itemDB,Config.ItemDataPath);
				}
				return item;
			}
		}
		else
		{
			Initialize();
			return Synthesize(seed);
		}
	}
}
