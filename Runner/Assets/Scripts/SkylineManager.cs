﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylineManager : MonoBehaviour
{
	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	public Vector3 minSize, maxSize;

	void Start()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue.Enqueue((Transform)Instantiate(prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		}
		enabled = false;
	}

	private void GameStart()
	{
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++)
		{
			Recycle();
		}
		enabled = true;
	}

	private void GameOver()
	{
		enabled = false;
	}

	void Update()
	{
		if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			Recycle();
		}
	}

	private void Recycle()
	{
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(o);
	}
}
