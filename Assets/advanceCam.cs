using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advanceCam : MonoBehaviour {

    public Transform playerPos;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerPos.position.z > transform.position.z + 10)
        {
            transform.position += new Vector3(0, 0, Time.deltaTime);
        }
	}
}
