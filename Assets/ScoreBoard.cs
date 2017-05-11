using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
	private GameObject player;
	private PlayerBehavior pb;
	private Text text;
	private string tScore = "Score: ";
	private string tCombo = "Combo: ";

	void Start () {
		player = GameObject.FindWithTag("Player");
		pb = player.GetComponent<PlayerBehavior> ();
		text = GetComponent<Text> ();
	}

	
	// Update is called once per frame
	void Update () {
		text.text = tScore + pb.score + "\n" + tCombo + pb.combo + "x";
	}
}
