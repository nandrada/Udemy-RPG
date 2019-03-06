using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	public float moveSpeed;

	public Animator myAnim;

	public static PlayerController instance; // reference to this script that is attached to the player

	public string areaTransitionName;
	private Vector3 bottomLeftLimit;
	private Vector3 topRightLimit;

	public bool canMove = true;

	void Start()
	{
		if(instance == null) // checks for instance existence, prevents duplicate players
		{
			instance = this;
		}
		else
		{
			if(instance != this)
			{
				Destroy(gameObject);
			}
		}
		DontDestroyOnLoad(gameObject); // Makes sure player doesnt get destroyed when loading into other scenes
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (canMove)
		{
			rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
		} else
		{
			rb.velocity = Vector2.zero;
		}

		myAnim.SetFloat("moveX", rb.velocity.x);
		myAnim.SetFloat("moveY", rb.velocity.y);

		if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
		{
			if (canMove)
			{
				myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
				myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
			}
		}

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
	}

	public void SetBounds(Vector3 botLeft, Vector3 topRight)
	{
		bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
		topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
	}
}
