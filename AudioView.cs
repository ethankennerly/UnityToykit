using UnityEngine/*<GameObject>*/;
using System.Collections.Generic/*<Dictionary>*/;

namespace Finegamedesign.Utils
{
	public sealed class AudioView
	{
		public AudioSource audio;
		public Dictionary<string, AudioClip> sounds;

		//
		// Expects exactly one instantiated game object with this name.
		// Preloads sounds into a static dictionary to playback by base filename.
		// Expects each sound file is in Assets/Resources folder at the path.
		// Warning:  All files in Resources are compiled into the executable.  Only place necessary files to play the game in that folder.
		// 
		public AudioSource Setup(string gameObjectName, 
				List<string> loadBaseFilenames, 
				string audioPath = "Sounds/")
		{
			GameObject gameObject = GameObject.Find(gameObjectName);
			audio = gameObject.GetComponent<AudioSource>();
			if (null == audio) {
				gameObject.AddComponent<AudioSource>();
				audio = gameObject.GetComponent<AudioSource>();
			}
			sounds = new Dictionary<string, AudioClip>();
			for (int index = 0; index < loadBaseFilenames.Count; index++) {
				string filename = loadBaseFilenames[index];
				sounds[filename] = (AudioClip) Resources.Load(audioPath + filename, typeof(AudioClip));
				if (null == sounds[filename]) {
					throw new System.InvalidOperationException("Could not find AudioClip at Resources/" + audioPath + filename);
				}
			}
			return audio;
		}

		public void Play(string name)
		{
			audio.PlayOneShot(sounds[name]);
		}
	}
}
