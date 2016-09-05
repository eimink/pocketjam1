using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public static class FileUtil {

	public static void Write(Dictionary<string, string> dictionary, string file)
	{
		using (FileStream fs = File.OpenWrite(Config.ItemDataPath))
		using (BinaryWriter writer = new BinaryWriter(fs))
		{
			writer.Write(dictionary.Count);
			foreach (var pair in dictionary)
			{
				writer.Write(pair.Key);
				writer.Write(pair.Value);
			}
		}
	}

	public static Dictionary<string, string> Read(string file)
	{
		var result = new Dictionary<string, string>();
		if (File.Exists(Config.ItemDataPath))
		{
			using (FileStream fs = File.OpenRead(Config.ItemDataPath))
			using (BinaryReader reader = new BinaryReader(fs))
			{
				int count = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					string key = reader.ReadString();
					string value = reader.ReadString();
					result[key] = value;
				}
			}
		}
		return result;
	}
		
}
