using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryObject : MonoBehaviour 
{
	private Animator _animator;

	void Awake ()
	{
		_animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			TriggerVictory ();
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			_animator.SetBool ("IsVictory", false);
		}
	}

	private void TriggerVictory ()
	{
		_animator.SetBool ("IsVictory", true);
	}
}