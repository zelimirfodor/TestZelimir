using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : MonoBehaviour 
{
	public int Health = 0;

	void Awake ()
	{
		LoadHealth ();
		//SaveHealth ();
	}

	public void SaveHealth ()
	{
		PlayerPrefs.SetInt ("Health", Health);
		Debug.Log ("health saved: " + Health.ToString());
	}

	public void LoadHealth ()
	{
		Health = PlayerPrefs.GetInt ("Health", 10);
		Debug.Log ("health loaded: " + Health.ToString());
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();

		Player player = (Player)target;

		if (GUILayout.Button ("Save Health"))
			player.SaveHealth ();

		if (GUILayout.Button ("Load Health"))
			player.LoadHealth ();
	}
}
#endif