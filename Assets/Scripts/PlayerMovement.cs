using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour 
{
	[Range(1.0f, 10.0f)]
	public float MovementSpeed = 3.0f;
	public float JumpForce = 500.0f;

	public Transform GroundCheck;
	public LayerMask GroundLayerMask;

	private float _movementHorizontal;
	private float _movementVertical;

	private Transform _transform;
	private Rigidbody2D _rigidbody2D;
	private Animator _animator;

	public bool isFacingRight = true;

	[Header("Read-only")]
	[SerializeField]
	private bool _isWalking = false;
	[SerializeField]
	private bool _isGrounded = true;

	private int _playerLayer;
	private int _platformLayer;

	void Awake ()
	{
		_transform = transform;
		_animator = GetComponent<Animator> ();
		_rigidbody2D = GetComponent<Rigidbody2D> ();

		_animator.SetBool ("IsGrounded", true);
	}

	void Update ()
	{
		_movementHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");

		if (_movementHorizontal != 0) 
		{
			_isWalking = true;
		}
		else 
		{
			_isWalking = false;
		}

		_animator.SetBool ("IsWalking", _isWalking);

		_movementVertical = _rigidbody2D.velocity.y;

		_isGrounded = Physics2D.Linecast (_transform.position, GroundCheck.position, GroundLayerMask);

		if (_isGrounded && CrossPlatformInputManager.GetButtonDown ("Jump")) 
		{
			_movementVertical = 0.0f;
			_rigidbody2D.AddForce (Vector2.up * JumpForce);
		}

		if (CrossPlatformInputManager.GetButtonUp ("Jump") && (_movementVertical > 0.0f))
		{
			_movementVertical = 0.0f;
		}

		_animator.SetBool ("IsGrounded", _isGrounded);

		Vector2 movement = new Vector2 (_movementHorizontal * MovementSpeed, _movementVertical);
		_rigidbody2D.velocity = movement;
	}

	void LateUpdate ()
	{
		Flip ();
	}

	private void Flip ()
	{
		Vector3 localScale = _transform.localScale;

		if (_movementHorizontal > 0.0f) 
		{
			isFacingRight = true;
		} 
		else if (_movementHorizontal < 0.0f)
		{
			isFacingRight = false;
		}

		if ((isFacingRight && (localScale.x < 0.0f)) || (!isFacingRight && (localScale.x > 0.0f)))
			localScale.x *= -1.0f;

		_transform.localScale = localScale;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == _platformLayer)
			_transform.SetParent (other.transform);
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.layer == _platformLayer)
			_transform.parent = null;
	}

	void OnDrawGizmos ()
	{
		Gizmos.DrawLine (transform.position, GroundCheck.position);
	}
}