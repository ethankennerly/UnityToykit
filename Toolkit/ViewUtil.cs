using UnityEngine;  // GameObject
using UnityEngine.UI;  // Text
using System.Collections.Generic;  // Dictionary

/**
 * Sharing code between projects:
 * Git repo or submodule.
 * Symlink on mac.
 * Copy code.
 * http://www.rivellomultimediaconsulting.com/symlinks-for-unity-game-development/
 */
public class ViewUtil
{
	/**
	 * Add ButtonView component to buttons and clickable game objects.
	 * @param	address		Expects the name is unique among all prefabs on the stage.
	 */
	public static void SetupButton(Controller controller, string address)
	{
		ButtonView.controller = controller;
		GameObject child = GameObject.Find(address);
		child.AddComponent<ButtonView>();
	}

	public static GameObject GetChild(GameObject parent, string name)
	{
		GameObject child = parent.transform.Find(name).gameObject;
		return child;
	}
	/**
	 * Call animator.Play instead of animator.SetTrigger, in case the animator is in transition.
	 * Test case:  2015-11-15 Enter "SAT".  Type "RAT".  Expect R selected.  Got "R" resets to unselected.
	 * http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
	 *
	 * Do not call until initialized.  Test case:  2015-11-15 Got warning "Animator has not been initialized"
	 * http://answers.unity3d.com/questions/878896/animator-has-not-been-initialized-1.html
	 *
	 * In editor, deleted and recreated animator state transition.  Test case:  2015-11-15 Got error "Transition '' in state 'selcted' uses parameter 'none' which is not compatible with condition type"
	 * http://answers.unity3d.com/questions/1070010/transition-x-in-state-y-uses-parameter-z-which-is.html
	 *
	 * Unity expects not to animate the camera or the root itself.  Instead animate the child of the root.  The root might not move.
	 * Test case:  2016-02-13 Animate camera position.  Play.  Camera does not move.  Generate root motion curves.  Apply root motion curves.  Still camera does not move.  Assign animator to parent of camera.  Animate child.  Then camera moves.
	 */
	public static void SetState(GameObject gameObject, string state, bool isRestart = true)
	{
		Animator animator = gameObject.GetComponent<Animator>();
		if (null != animator && animator.isInitialized)
		{
			// Debug.Log("ViewUtil.SetState: " + gameObject + ": " + state);
			if (isRestart)
			{
				animator.Play(state);
			}
			else
			{
				animator.Play(state, -1, 0f);
			}
		}
		else
		{
			Debug.Log("ViewUtil.SetState: Does animator exist? " + gameObject + ": " + state);
		}
	}

	public static void SetText(Text textComponent, string text)
	{
		textComponent.text = text;
	}

	public static AudioSource audio;
	public static Dictionary<string, AudioClip> sounds;

	/**
	 * Expects exactly one instantiated game object with this name.
	 * Preloads sounds into a static dictionary to playback by base filename.
	 * Expects each sound file is in Assets/Resources folder at the path.
	 * Warning:  All files in Resources are compiled into the executable.  Only place necessary files to play the game in that folder.
	 */
	public static AudioSource SetupAudio(string gameObjectName, string[] loadBaseFilenames, string audioPath = "sounds/")
	{
		GameObject gameObject = GameObject.Find(gameObjectName);
		audio = gameObject.GetComponent<AudioSource>();
		if (null == audio) {
			gameObject.AddComponent<AudioSource>();
			audio = gameObject.GetComponent<AudioSource>();
		}
		sounds = new Dictionary<string, AudioClip>();
		for (int index = 0; index < loadBaseFilenames.Length; index++) {
			string filename = loadBaseFilenames[index];
			sounds[filename] = (AudioClip) Resources.Load(audioPath + filename);
		}
		return audio;
	}

	public static void PlaySound(string name)
	{
		audio.PlayOneShot(sounds[name]);
	}

	public static bool GetKeyDown(string keyName)
	{
		return Input.GetKeyDown(keyName);
	}

	public static string GetInputString()
	{
		return Input.inputString;
	}

}
