using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelManager : MonoBehaviour {

	public GameObject player;

	public GameObject groundPrefab;
	public int activeChunkCount = 5;
	public float respawnDistance = 10.0f;

	private float chunkWidth;

	private Queue<GameObject> groundObjects;

	private float nextX = 0.0f;

	void Start () {
		groundObjects = new Queue<GameObject>(activeChunkCount);

		chunkWidth = groundPrefab.transform.localScale.x;

		nextX = - (activeChunkCount / 2) * chunkWidth;
		foreach (int i in Enumerable.Range(0, activeChunkCount-1)) {
			groundObjects.Enqueue(getNextChunk());
		}

	}

	GameObject getNextChunk() {
		GameObject g = Instantiate<GameObject>(groundPrefab);
		Vector3 pos = g.transform.position;
		pos.x = nextX;
		g.transform.position = pos;
		nextX += chunkWidth;
		return g;
	}

	void UpdateChunks() {
		GameObject lastGround = groundObjects.First();

		float diff = player.transform.position.x - lastGround.transform.position.x;

		if (diff > respawnDistance) {
			GameObject last = groundObjects.Dequeue();
			Destroy (last);
			groundObjects.Enqueue(getNextChunk());
		}

	}

	void Update () {
		UpdateChunks ();
	}

}
