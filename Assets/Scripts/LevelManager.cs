using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelManager : MonoBehaviour {

	private GameObject player;

	public int chuckCount = 2;
	public GameObject groundPrefab;
	public GameObject obstaclePrefab;

	public float obstacleInterval = 10.0f;
	public float obstacleSpawnDistance = 32.0f;

	public float respawnDistance = 10.0f;

	private float gameTime = 0.0f;
	private float sinceLastObstacle = 0.0f;

	private float nextX = 0.0f;
	private float chunkWidth;
	private Queue<GameObject> groundObjects;

	void Start () {
		player = GameObject.FindWithTag("Player");
		groundObjects = new Queue<GameObject>(chuckCount);
		chunkWidth = groundPrefab.transform.localScale.x;

		foreach (int i in Enumerable.Range(0, chuckCount)) {
			groundObjects.Enqueue(getNextChunk());
			Debug.Log ("Create chunk");
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
		if (diff > chunkWidth) {
			GameObject last = groundObjects.Dequeue();
			Destroy (last);
			groundObjects.Enqueue(getNextChunk());
		}

	}

	GameObject getObstacle() {
		GameObject g = Instantiate<GameObject>(obstaclePrefab);
		Vector3 pos = player.transform.position;
		pos.x = pos.x + obstacleSpawnDistance;
		g.transform.position = pos;
		return g;
	}

	void Update () {
		UpdateChunks ();
		float translation = Time.deltaTime;
		sinceLastObstacle += translation;
		if (sinceLastObstacle >= obstacleInterval) {
			GameObject g = getObstacle ();
			sinceLastObstacle = 0.0f;
		}
		gameTime += translation;

	}

}
