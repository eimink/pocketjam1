using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class Item {
	public string id;
	public List<string> triggerWords;
	public Item(string _id, List<string> _triggerWords){
		this.id = _id;
		this.triggerWords = _triggerWords;
	}
	public override string ToString()
	{
		return string.Format("[Item] "+id);
	}
}

public static class ItemSynthesizer {

	private static Dictionary<string,string> itemDB;

	private static bool initialized = false;

	private static Dictionary<string,List<string>> synonymDB;
	private static List<Item> availableItems;

	private static void ReadItemDatabase()
	{
		TextAsset textFile = (TextAsset)Resources.Load("itemdb", typeof(TextAsset));
		var lines = textFile.text.Split('\n');
		foreach ( string line in lines )
		{
			var values = line.Split(',');
			List<string> triggers = new List<string>();
			for (int i = 1; i < values.Length; i++)
			{
				if (!String.IsNullOrEmpty(values[i]) && !triggers.Contains(values[i].ToLowerInvariant()))
				{
					//itemDB.Add(values[i].ToLowerInvariant(),values[0]);
					triggers.Add(values[i].ToLowerInvariant());

				}
			}
			availableItems.Add(new Item(values[0].ToLowerInvariant(),triggers));
		}
	}

	private static Item findItemFromSynonyms(string input)
	{
		Item foundItem = availableItems.Find(item => item.triggerWords.Contains(input));
		if (foundItem != null)
			return foundItem;
		else
			return null;
	}

	public static void Initialize()
	{
		if (availableItems == null)
			availableItems = new List<Item>();
		//itemDB = FileUtil.Read(Config.ItemDataPath);
		ReadItemDatabase();
		if (itemDB == null)
			itemDB = new Dictionary<string, string>();
		initialized = true;
	}

	public static Item Synthesize(string seed)
	{
		if (initialized)
		{
			Item item = findItemFromSynonyms(seed);
			return item;
		}
		else
		{
			Initialize();
			return Synthesize(seed);
		}
	}
}
