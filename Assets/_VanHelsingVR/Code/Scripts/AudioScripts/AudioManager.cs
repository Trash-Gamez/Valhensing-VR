using UnityEngine;
using System.Collections;
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public enum AudioChannel { Master, Music, Ambient, fx };

	[Range(0, 1)]
	public float masterVolume = 1; // Overall volume
	[Range(0, 1)]
	public float musicVolume = 1f; // Music volume
	[Range(0, 1)]
	public float ambientVolume = 1; // Ambient volume
	[Range(0, 1)]
	public float fxVolume = 1; // SFX volume

	public bool MusicIsLooping = true;
	public bool AmbientIsLooping = true;
	
	
	AudioSource track01;
	AudioSource track02;
	AudioSource ambientSource;
	AudioSource fxSource;

	private bool _isPlayingTrack01;
	[SerializeField] private float timeToFade = 1.25f;

	SoundLibrary soundLibrary;
	MusicLibrary musicLibrary;
	AmbientLibrary ambientLibrary;

	
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != this) Destroy(gameObject);

	   DontDestroyOnLoad(gameObject);
		
		soundLibrary = GetComponent<SoundLibrary>();
		musicLibrary = GetComponent<MusicLibrary>();
		ambientLibrary = GetComponent<AmbientLibrary>();

		
		// Create audio sources
		
		GameObject newfxSource = new GameObject("2D fx source");
		fxSource = newfxSource.AddComponent<AudioSource>();
		newfxSource.transform.parent = transform;
		fxSource.playOnAwake = false;

		GameObject newMusicSource = new GameObject("Music source");
		track01 = newMusicSource.AddComponent<AudioSource>();
		track02 = newMusicSource.AddComponent<AudioSource>();
		newMusicSource.transform.parent = transform;
		track01.loop = MusicIsLooping; // Music is looping
		track01.playOnAwake = false;
		track02.loop = MusicIsLooping; // Music is looping
		track02.playOnAwake = false;



		GameObject newAmbientsource = new GameObject("Ambient source");
		ambientSource = newAmbientsource.AddComponent<AudioSource>();
		newAmbientsource.transform.parent = transform;
		ambientSource.loop = AmbientIsLooping; // Ambient sound is looping
		ambientSource.playOnAwake = false;
		_isPlayingTrack01 = true;
		
		// Set volume on all the channels
		
		SetVolume();
		
	}

	
	// Set volume on all the channels
	
	public void SetVolume()
	{
		
		fxSource.volume = fxVolume * masterVolume;
		track01.volume = musicVolume * masterVolume;
		track02.volume = musicVolume * masterVolume;
		ambientSource.volume = ambientVolume * masterVolume;
	}

	
	// Play music with delay. 0 = No delay

	public void SwampMusic(string Clip)
    {
		StopAllCoroutines();
		StartCoroutine(FadeTrack(Clip));
		_isPlayingTrack01 = !_isPlayingTrack01;
    }

	private IEnumerator FadeTrack(string Clip)
    {
		float timeElapsed = 0;
		if (_isPlayingTrack01)
		{
			track02.clip = musicLibrary.GetClipFromName(Clip);
			track02.Play();
            while (timeElapsed < timeToFade)
            {
				track02.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade) * musicVolume * masterVolume;
				track01.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade) * musicVolume * masterVolume;
				timeElapsed += Time.deltaTime;
				yield return null;
			}
			track01.Stop();
		}
		else
		{
			track01.clip = musicLibrary.GetClipFromName(Clip);
			track01.Play();
			while (timeElapsed < timeToFade)
			{
				track01.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade)*musicVolume*masterVolume;
				track02.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade) * musicVolume * masterVolume;
				timeElapsed += Time.deltaTime;
				yield return null;
			}
			track02.Stop();
		}
	}
	 
	
	// Play ambient sound with delay 0 = No delay
	
	public void PlayAmbient(string ambientName, float delay)
	{
		ambientSource.clip = ambientLibrary.GetClipFromName(ambientName);
		ambientSource.PlayDelayed(delay);
	}

	
	// Stop ambient sound
	
	public void StopAmbient()
	{
		ambientSource.Stop();
	}

	
	// FX Audio
	
	public void PlaySound2D(string soundName)
	{
		fxSource.PlayOneShot(soundLibrary.GetClipFromName(soundName), fxVolume * masterVolume);
	}

	public void PlaySound3D(string soundName, Vector3 soundPosition)
	{
		AudioSource.PlayClipAtPoint(soundLibrary.GetClipFromName(soundName), soundPosition, fxVolume * masterVolume);
	}

	public bool IsAmbientPlaying(string clipId)
    {
        UnityEngine.AudioClip clip = ambientLibrary.GetClipFromName(clipId);
		if (clip == ambientSource.clip)
		{
			return ambientSource.isPlaying;
        }
        else
        {
			return false;
        }
    }
}
