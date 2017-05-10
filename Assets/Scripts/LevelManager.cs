using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public GameObject backgroundCubePrefab;
	public GameObject backgroundLightPrefab;

	public GameObject groundPrefab1;
	public GameObject groundPrefab2;
	public int groundFrontCount = 10;

	public Vector3 backgroundCubeBasePos1;
	public Vector3 backgroundCubeRandPosLimits1;

	public Vector3 backgroundCubeBasePos2;
	public Vector3 backgroundCubeRandPosLimits2;

	private GameObject player;

	public int bigObstacleInterval = 3;

	public float obstacleInterval = 10.0f;
	public float obstacleSpawnDistance = 32.0f;

	private float distanceTravelled = 0.0f;
	private float lastObstacleX = 0.0f;
	private float lastbackgroundCubeX = 0.0f;

	private int i = 0;
	private int ib = 0;
	private GameObject[] obstacles;
	private GameObject[] bigObstacles;

	public float backgroundCubeInterval = 10.0f;
	public float backgroundCubeDistance = 128.0f;

	public int backgroundlightInterval = 10;
	public int sinceBackgroundLight = 0;

	private float groundLen = 0.0f;
	private int lastGround = 0;
	private bool nextGround1 = false;

	private int sinceBigObstacle = 0;

	void Start () {
		groundLen = groundPrefab1.transform.localScale.x;

		player = GameObject.FindWithTag("Player");
		obstacles = Resources.LoadAll<GameObject>("Obstacles");
		bigObstacles = Resources.LoadAll<GameObject>("Big Obstacles");

		foreach (int i in Enumerable.Range(0, (int)(250/backgroundCubeInterval))) {
			createBackgroundCubes(-i * backgroundCubeInterval);
		}

		foreach (int i in Enumerable.Range(0, groundFrontCount)) {
			createGround (i);
			lastGround++;
		}
	}

	void createGround(int i) {
		GameObject g = Instantiate<GameObject>(nextGround1?groundPrefab1:groundPrefab2);
		g.transform.position = new Vector3 (groundLen * i, 0.0f, 0.0f);
		nextGround1 = !nextGround1;
		sinceBigObstacle++;

	}

	void createbgcube(float refX, Vector3 basePos, Vector3 randLimits) {
		float posD = 20f;
		float colD = 0.25f;
		float lposD = 3.0f;

		GameObject g = Instantiate<GameObject>(backgroundCubePrefab);
		Vector3 pos = basePos;
		Quaternion rot = g.transform.localRotation;
		pos.x = refX + backgroundCubeDistance;

		pos.y = pos.y + UnityEngine.Random.Range(-randLimits.x, randLimits.x);
		pos.z = pos.z + UnityEngine.Random.Range(-randLimits.z, randLimits.z);
		pos.x = pos.x + UnityEngine.Random.Range(-randLimits.y, randLimits.y);

		g.transform.rotation = UnityEngine.Random.rotation;
		float s = UnityEngine.Random.Range (5, 15);
		g.transform.localScale = new Vector3(s, s, s);
		g.transform.position = pos;

		if (sinceBackgroundLight >= backgroundlightInterval) {
			GameObject l = Instantiate<GameObject>(backgroundLightPrefab);
			Vector3 lpos = g.transform.position;
			l.transform.position = g.transform.position;
			lpos.y = lpos.y + UnityEngine.Random.Range(-lposD, lposD);
			lpos.z = lpos.z + UnityEngine.Random.Range(-lposD, lposD);
			lpos.x = lpos.x + UnityEngine.Random.Range(-lposD, lposD);
			l.transform.position = lpos;
			Light lightComp = l.GetComponent<Light> ();
			Color col = lightComp.color;
			col.r = col.r + UnityEngine.Random.Range(-colD, colD);
			col.g = col.g + UnityEngine.Random.Range(-colD, colD);
			col.b = col.b + UnityEngine.Random.Range(-colD, colD);
			lightComp.color = col;
		}

	}

	void createBigObstacle() {
		Debug.Log ("Create Big Obstacle");
		ib++;
		if (ib >= bigObstacles.Count ()) {
			ib = 0;
		}
		GameObject g = Instantiate<GameObject>(bigObstacles[ib]);
		Debug.Log (g.name);
		Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
		pos.x = (lastGround+1) * groundLen;
		g.transform.position = pos;
		lastGround = lastGround + 3;
	}

	void createBackgroundCubes(float refX) {
		createbgcube (refX, backgroundCubeBasePos1, backgroundCubeRandPosLimits1);
		createbgcube (refX, backgroundCubeBasePos2, backgroundCubeRandPosLimits2);
		sinceBackgroundLight++;
	}

	GameObject createObstacle(float refX) {
		i++;
		if (i >= obstacles.Count ()) {
			i = 0;
		}
		int rand = UnityEngine.Random.Range(0, obstacles.Count()-1);
		GameObject g = Instantiate<GameObject>(obstacles[i]);
		Vector3 pos = g.transform.position;
		pos.x = refX + obstacleSpawnDistance;
		g.transform.position = pos;
		return g;
	}

	void Update () {
		distanceTravelled = player.transform.position.x;
		if (distanceTravelled - lastObstacleX >= obstacleInterval) {
			if (sinceBigObstacle >= bigObstacleInterval) {
				createBigObstacle();
				sinceBigObstacle = 0;
			} else {
				createObstacle (player.transform.position.x);
				sinceBigObstacle++;
			}
			lastObstacleX = distanceTravelled;
		}
		if (distanceTravelled - lastbackgroundCubeX >= backgroundCubeInterval) {
			createBackgroundCubes (player.transform.position.x);
			lastbackgroundCubeX = distanceTravelled;
		}

		if ((lastGround-groundFrontCount) * groundLen <= distanceTravelled) {
			createGround (lastGround);
			lastGround++;
		}

	}

}
