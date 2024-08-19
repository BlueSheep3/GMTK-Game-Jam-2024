using System.IO;
using UnityEngine;

static class Savedata
{
	const int VERSION = 0;
	static readonly string path = Application.persistentDataPath;
	public static Settings settings = Load();

	public class Settings
	{
		public int version = VERSION;
		public int maxHeight = 0;
		public int maxShapes = 0;
		public int volume = 50;
	}


	// only saving when the game closes is mostly fine,
	// but if the game crashes, all progress of that session would be lost,
	// so you should use this function in some places that might seem important to save.
	public static void Save(Settings s) {
		if(s is not null) {
			File.WriteAllText(path + "/settings.json", JsonUtility.ToJson(s, true));
			Debug.Log("(SAVE): Saved Settings");
		} else {
			Debug.LogError("(SAVE): No Settings to save (null)");
		}
	}

	public static Settings Load() {
		// create file if it doesnt exist
		if(!File.Exists(path + "/settings.json")) File.WriteAllText(path + "/settings.json", "");

		Settings s = JsonUtility.FromJson<Settings>(File.ReadAllText(path + "/settings.json")) ?? new Settings();
		while(s.version < VERSION) UpdateSettings(s);
		AudioListener.volume = s.volume / 100f;
		Debug.Log("(SAVE): Loaded Settings");
		return s;
	}

	static void UpdateSettings(Settings s) {
		Debug.Log("(SAVE): Updating Settings...");

		switch(s.version) {
			default:
				Debug.LogError("(SAVE): Don't know how to update");
				break;
		}
		s.version++;

		Debug.Log("(SAVE): Settings updated to version " + s.version);
	}
}
