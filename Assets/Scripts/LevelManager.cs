using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public GameObject backgroundCubePrefab;
	public GameObject backgroundLightPrefab;

	private GameObject player;

	public float obstacleInterval = 10.0f;
	public float obstacleSpawnDistance = 32.0f;

	private float distanceTravelled = 0.0f;
	private float lastObstacleX = 0.0f;
	private float lastbackgroundCubeX = 0.0f;

	private int i = 0;
	private GameObject[] obstacles;

	public float backgroundCubeInterval = 10.0f;
	public float backgroundCubeDistance = 128.0f;

	public int backgroundlightInterval = 10;
	public int sinceBackgroundLight = 0;

	void Start () {
		player = GameObject.FindWithTag("Player");
		obstacles = Resources.LoadAll<GameObject>("Obstacles");
		Debug.Log("Loaded");

		foreach (int i in Enumerable.Range(0, 10)) {
			
		}
//		GameObject g = getObstacle();
	}

	GameObject createBackgroundCube() {
		float posD = 10f;
		float colD = 0.25f;
		float lposD = 3.0f;
		GameObject g = Instantiate<GameObject>(backgroundCubePrefab);
		Vector3 pos = g.transform.position;
		Quaternion rot = g.transform.localRotation;
		pos.x = player.transform.position.x + backgroundCubeDistance;

		pos.y = pos.y + UnityEngine.Random.Range(-posD, posD);
		pos.z = pos.z + UnityEngine.Random.Range(-posD, posD);
		pos.x = pos.x + UnityEngine.Random.Range(-posD, posD);

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
		sinceBackgroundLight++;

		return g;
	}

	GameObject createObstacle() {
		i++;
		if (i >= obstacles.Count ()) {
			i = 0;
		}
		int rand = UnityEngine.Random.Range(0, obstacles.Count()-1);
		GameObject g = Instantiate<GameObject>(obstacles[i]);
		Vector3 pos = g.transform.position;
		pos.x = player.transform.position.x + obstacleSpawnDistance;
		g.transform.position = pos;
		return g;
	}

	void Update () {
		distanceTravelled = player.transform.position.x;
		if (distanceTravelled - lastObstacleX >= obstacleInterval) {
			GameObject g = createObstacle ();
			lastObstacleX = distanceTravelled;
		}
		if (distanceTravelled - lastbackgroundCubeX >= backgroundCubeInterval) {
			GameObject g = createBackgroundCube ();
			lastbackgroundCubeX = distanceTravelled;
		}
	}

}
