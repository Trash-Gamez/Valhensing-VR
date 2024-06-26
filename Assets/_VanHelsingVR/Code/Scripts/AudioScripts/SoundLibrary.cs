using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour
{
	public SoundGroup[] soundGroups;

	Dictionary<string, UnityEngine.AudioClip[]> groupDictionary = new Dictionary<string, UnityEngine.AudioClip[]>();

	void Awake()
	{
		foreach (SoundGroup soundGroup in soundGroups)
		{
			groupDictionary.Add(soundGroup.name, soundGroup.clip);
		}
	}

	public UnityEngine.AudioClip GetClipFromName(string name)
	{
		if (groupDictionary.ContainsKey(name))
		{
            UnityEngine.AudioClip[] sounds = groupDictionary[name];
			return sounds[Random.Range(0, sounds.Length)];
		}
		return null;
	}

	[System.Serializable]
	public class SoundGroup
	{
		public string name;
		public UnityEngine.AudioClip[] clip;
	}
}
