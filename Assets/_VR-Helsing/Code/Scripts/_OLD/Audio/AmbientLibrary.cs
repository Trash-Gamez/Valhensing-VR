using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmbientLibrary : MonoBehaviour
{
	public AmbientGroup[] ambientGroups;

	Dictionary<string, UnityEngine.AudioClip> groupDictionary = new Dictionary<string, UnityEngine.AudioClip>();

	void Awake()
	{
		foreach (AmbientGroup ambientGroups in ambientGroups)
		{
			groupDictionary.Add(ambientGroups.name, ambientGroups.clip);
		}
	}

	public UnityEngine.AudioClip GetClipFromName(string name)
	{
		if (groupDictionary.ContainsKey(name))
		{
            UnityEngine.AudioClip ambient = groupDictionary[name];
			return ambient;
		}
		return null;
	}

	[System.Serializable]
	public class AmbientGroup
	{
		public string name;
		public UnityEngine.AudioClip clip;
	}
}