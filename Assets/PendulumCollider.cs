using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hands"))
        {
            GameObject.Find("LevelManager").GetComponent<Hv_SurfOfLifeSimpleEngine_AudioLib>().SendEvent(Hv_SurfOfLifeSimpleEngine_AudioLib.Event.Action);
        }
    }
}
