using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public GameObject backgroundCubePrefab;
	public GameObject backgroundLightPrefab;
	public bool inMenu = true;

	public GameObject groundPrefab1;
	public GameObject groundPrefab2;
	public int groundFrontCount = 10;

	private GameObject player;
	private PlayerBehavior pb;
	public float baseSpeed;

	public int envObstavleInterval = 5;

	public float obstacleInterval = 10.0f;
	public float rupeeInterval = 5.0f;

	public float obstacleSpawnDistance = 32.0f;

	private float distanceTravelled = 0.0f;
	private float lastObstacleX = 0.0f;

	private int i = 0;
	private int ie = 0;
	private int ir = 0;

	private GameObject[] obstacles;
	private GameObject[] envObstacles;
	private GameObject[] rupees;

	private float groundLen = 0.0f;
	private int lastGround = 0;
	private bool nextGround1 = false;

	private int sinceEnvObstacle = 0;
	private int sinceRupee = 0;

//	private float speedFactor = 1.0f;
	private float speedupAmount = 0.01f;
	private float lastSpeedup = 0.0f;
	private float speedupInterval = 100.0f;

	private float lastDistancePoint = 0.0f;
	private float distancePointInterval = 5.0f;
	private float distancePointAmount = 1f;

	private GameObject hud;
	private GameObject menu;
	private GameObject endScreen;

	private bool inEndScreen = false;
    private bool startEndScreen = true;
    private float endScreenTime = 0.0f;

    void Start () {
        GetComponent<Hv_SurfOfLifeSimpleEngine_AudioLib>().SendEvent(Hv_SurfOfLifeSimpleEngine_AudioLib.Event.Start);
        groundLen = groundPrefab1.transform.localScale.x;

		player = GameObject.FindWithTag("Player");
		pb = player.GetComponent<PlayerBehavior> ();

		obstacles = Resources.LoadAll<GameObject>("Obstacles");
		envObstacles = Resources.LoadAll<GameObject>("EnvironmentObstacles");
		rupees = Resources.LoadAll<GameObject>("Jewls");

		hud = GameObject.Find("Hud");
		menu = GameObject.Find("Menu");
		endScreen = GameObject.Find("EndScreen");

		hud.SetActive (false);
		endScreen.SetActive (false);

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
		GameObject g;
		if (sinceEnvObstacle >= envObstavleInterval) {
			ie++;
			if (ie >= envObstacles.Count ()) {
				ie = 0;
			}
			int rand = UnityEngine.Random.Range(0, envObstacles.Count()-1);
			g = Instantiate<GameObject>(envObstacles[ie]);

			Vector3 pos = g.transform.position;
			pos.x = refX + obstacleSpawnDistance;
			g.transform.position = pos;
			sinceEnvObstacle = 0;
		} else if (sinceRupee >= rupeeInterval) {
			ir++;
			if (ir >= rupees.Count ()) {
				ir = 0;
			}
			int rand = UnityEngine.Random.Range(0, rupees.Count()-1);
			g = Instantiate<GameObject>(rupees[ir]);
			Vector3 pos = g.transform.position;
			pos.x = refX + obstacleSpawnDistance;
			pos.z =  UnityEngine.Random.Range(0.0f, 20.0f) - 10.0f;
			g.transform.position = pos;
			sinceRupee = 0;
		} else {
			i++;
			if (i >= obstacles.Count ()) {
				i = 0;
			}
			int rand = UnityEngine.Random.Range(0, obstacles.Count()-1);
			g = Instantiate<GameObject>(obstacles[i]);

			Vector3 pos = g.transform.position;
			pos.x = refX + obstacleSpawnDistance;
			pos.z =  UnityEngine.Random.Range(0.0f, 20.0f) - 10.0f;
			g.transform.position = pos;
		}
		sinceEnvObstacle++;
		sinceRupee++;
		return g;

	}

	void speedup() {
		baseSpeed += speedupAmount;
	}

	void Update () {
		if(inEndScreen) {
            if (startEndScreen) {
                startEndScreen = false;
                GetComponent<Hv_SurfOfLifeSimpleEngine_AudioLib>().SendEvent(Hv_SurfOfLifeSimpleEngine_AudioLib.Event.Gameover);
            }
            endScreenTime += Time.deltaTime;
            if(endScreenTime >= 3.5f) {
                GetComponent<Hv_SurfOfLifeSimpleEngine_AudioLib>().SendEvent(Hv_SurfOfLifeSimpleEngine_AudioLib.Event.Stop);
            }
            if (Input.GetKey ("space")) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		} else if (!inMenu) {
			distanceTravelled = player.transform.position.x;

			if (distanceTravelled - lastObstacleX >= obstacleInterval) {
				lastObstacleX = distanceTravelled;
				createObstacle (player.transform.position.x);
			}

			if ((lastGround-groundFrontCount) * groundLen <= distanceTravelled) {
				createGround (lastGround);
				lastGround++;
			}

			if (distanceTravelled - lastSpeedup >= speedupInterval) {
				speedup ();
				lastSpeedup = distanceTravelled;
			}
			if (distanceTravelled - lastDistancePoint >= distancePointInterval) {
				pb.score += (int) (distancePointAmount*pb.combo);
				lastDistancePoint = distanceTravelled;
			}
			pb.movementSpeed = baseSpeed;

			if (player.GetComponent<PlayerBehavior>().hp <= 0.0f) {
				endScreen.SetActive (true);
				inEndScreen = true;
			}
		} else if (inMenu && Input.GetKey("space")) {
			Debug.Log ("Start game");
			inMenu = false;
			hud.SetActive (true);
			menu.SetActive (false);
		}
			
	}

}
