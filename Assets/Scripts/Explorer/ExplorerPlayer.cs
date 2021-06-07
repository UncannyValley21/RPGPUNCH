using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class ExplorerPlayer : MonoBehaviour
{
	[SerializeField] private Sprite[] frameArray;
	[SerializeField] private float minSpeed = 1;
	[SerializeField] private float accelerationSec = 1;
	[SerializeField] private float maxSpeed = 3;

	//Movement
	private Vector2 inputMovement = Vector2.zero;
	private Vector3 movePoint;

	private static float MOVE_DISTANCE = 1f;

	//Animation
	private SpriteRenderer spriteRenderer;
	private int currentFrame;
	private float timer;
	private float movementSpeed;

	public void OnMovement(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			inputMovement = context.ReadValue<Vector2>();
		}
		else if(context.canceled)
		{
			inputMovement = Vector2.zero;
		}		
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		movementSpeed = minSpeed;
	}

	private void Start()
	{
		transform.position = GameManager.instance.transitionMapPosition.GetValueOrDefault(transform.position);
		movePoint = transform.position;
	}

	private void Update()
	{
		if(inputMovement != Vector2.zero)
		{
			movementSpeed += accelerationSec*Time.deltaTime;
		}
		else
		{
			movementSpeed -= accelerationSec*Time.deltaTime;
		}

		movementSpeed = Mathf.Clamp(movementSpeed, minSpeed, maxSpeed);

		//Movement
		if(transform.position == movePoint)
		{
			Vector3 movement = (Vector3)inputMovement * MOVE_DISTANCE;

			if(movement != Vector3.zero && (!Physics2D.OverlapPoint(movePoint + movement) || Physics2D.OverlapPoint(movePoint + movement).isTrigger))
			{
				movePoint += movement;
			}
		}

		else if(Vector3.Distance(transform.position, movePoint) <= 0.05f)
		{
			transform.position = new Vector3(Mathf.Round(transform.position.x*10)/10,
											Mathf.Round(transform.position.y*10)/10,
											transform.position.z);

			movePoint = transform.position;
	
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, movePoint, Time.deltaTime*movementSpeed);
		}

		//Animation
		timer += Time.deltaTime;

		if(timer >= 0.1f)
		{
			timer -= 0.1f;
			currentFrame++;

			if(currentFrame >= frameArray.Length)
				currentFrame = 0;

			spriteRenderer.sprite = frameArray[currentFrame];
		}
	}

	public Vector3 GetMovePoint()
	{
		return movePoint;
	}
}
