using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	public float StunDuration = 1.0f;

	[SerializeField]
	private bool _isStunned = false;

	private Animator _animator;
	private ObjectMover _objectMover;

	private int _enemyLayer = 0;
	private int _stunnedLayer = 0;

	void Awake ()
	{
		_animator = GetComponent<Animator> ();
		_objectMover = GetComponentInParent<ObjectMover> ();

		_enemyLayer = LayerMask.NameToLayer ("Enemy");
		_stunnedLayer = LayerMask.NameToLayer ("Stunned");
	}

	private void ChangeLayer (int newLayer)
	{
		gameObject.layer = newLayer;

		for (int i = 0; i < transform.childCount; i++) 
		{
			transform.GetChild (i).gameObject.layer = newLayer;
		}
	}

	public void ToggleStun (bool IsStunned)
	{
		if (_isStunned && IsStunned)
			return;

		_isStunned = IsStunned;
		_animator.SetBool ("IsStunned", _isStunned);
		_objectMover.ShouldMove = !_isStunned;

		if (_isStunned) 
		{
			ChangeLayer (_stunnedLayer);
			Invoke ("DisableStun", StunDuration);
		} 
		else 
		{
			ChangeLayer (_enemyLayer);
		}
	}

	private void DisableStun ()
	{
		ToggleStun (false);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log ("gameover");
		}
	}
}