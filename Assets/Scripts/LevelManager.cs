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

	public int envObstavleInterval = 5;

	public float obstacleInterval = 10.0f;
	public float obstacleSpawnDistance = 32.0f;

	private float distanceTravelled = 0.0f;
	private float lastObstacleX = 0.0f;
	private float lastbackgroundCubeX = 0.0f;

	private int i = 0;
	private int ie= 0;
	private GameObject[] obstacles;
	private GameObject[] envObstacles;
	private GameObject[] backgroundObjects;

	public float backgroundCubeInterval = 10.0f;
	public float backgroundCubeDistance = 128.0f;

	public int backgroundlightInterval = 10;
	public int sinceBackgroundLight = 0;

	private float groundLen = 0.0f;
	private int lastGround = 0;
	private bool nextGround1 = false;

	private int sinceEnvObstacle = 0;

	void Start () {
		groundLen = groundPrefab1.transform.localScale.x;

		player = GameObject.FindWithTag("Player");
		obstacles = Resources.LoadAll<GameObject>("Obstacles");
		envObstacles = Resources.LoadAll<GameObject>("EnvironmentObstacles");

		foreach (int i in Enumerable.Range(0, groundFrontCount)) {
			createGround (i);
			lastGround++;
		}
	}

	void createGround(int i) {
		GameObject g = Instantiate<GameObject>(nextGround1?groundPrefab1:groundPrefab2);
		g.transform.position = new Vector3 (groundLen * i, 0.0f, 0.0f);
		nextGround1 = !nextGround1;

	}

	void createbgcube(float refX, Vector3 basePos, Vector3 randLimits) {

	}
		
	void createBackgroundCubes(float refX) {
		
	}

	GameObject createObstacle(float refX) {
		if (sinceEnvObstacle >= envObstavleInterval) {
			ie++;
			if (ie >= obstacles.Count ()) {
				ie = 0;
			}
			int rand = UnityEngine.Random.Range(0, envObstacles.Count()-1);
			GameObject g = Instantiate<GameObject>(envObstacles[i]);
			Debug.Log ("Created env obstacle " + g.name);

			Vector3 pos = g.transform.position;
			pos.x = refX + obstacleSpawnDistance;
			g.transform.position = pos;
			sinceEnvObstacle = 0;
			return g;
		} else {
			i++;
			if (i >= obstacles.Count ()) {
				i = 0;
			}
			int rand = UnityEngine.Random.Range(0, obstacles.Count()-1);
			GameObject g = Instantiate<GameObject>(obstacles[i]);
			Debug.Log ("Created obstacle " + g.name);

			Vector3 pos = g.transform.position;
			pos.x = refX + obstacleSpawnDistance;
			pos.z =  UnityEngine.Random.Range(0.0f, 20.0f) - 10.0f;
			g.transform.position = pos;
			sinceEnvObstacle++;
			return g;
		}

	}

	void Update () {
		distanceTravelled = player.transform.position.x;

		if (distanceTravelled - lastObstacleX >= obstacleInterval) {
			lastObstacleX = distanceTravelled;
			createObstacle (player.transform.position.x);
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
