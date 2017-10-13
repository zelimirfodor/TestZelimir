using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour 
{
	public Transform Player;
	public Vector3 PositionOffset;

	private Transform _transform;

	void Awake ()
	{
		_transform = transform;
	}

	void LateUpdate ()
	{
		Vector3 newPosition = new Vector3 (Player.position.x, Player.position.y, _transform.position.z) + PositionOffset;
		_transform.position = newPosition;
	}
}