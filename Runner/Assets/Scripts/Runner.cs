using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
	private static int boosts;
	public static float distanceTraveled;

	public Rigidbody rb;
	public Renderer rn;
	public float acceleration;

	private bool touchingPlatform;
	public Vector3 boostVelocity, jumpVelocity;
	public float gameOverY;
	private Vector3 startPosition;


	void Start()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		rn.enabled = false;
		rb.isKinematic = true;
		enabled = false;
	}

	private void GameStart()
	{
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		rn.enabled = true;
		rb.isKinematic = false;
		enabled = true;
	}

	private void GameOver()
	{
		rn.enabled = false;
		rb.isKinematic = true;
		enabled = false;
	}

	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (touchingPlatform)
			{
				rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if (boosts > 0)
			{
				rb.AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
		}
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);

		if (transform.localPosition.y < gameOverY)
		{
			GameEventManager.TriggerGameOver();
		}
	}

	void FixedUpdate()
	{
		if (touchingPlatform)
		{
			rb.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	public static void AddBoost()
	{
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}

	void OnCollisionEnter()
	{
		touchingPlatform = true;
	}

	void OnCollisionExit()
	{
		touchingPlatform = false;
	}
}
