using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject box;
	void Start () {
        InvokeRepeating("SpawnBox", 0, 2);
	}
	
	void SpawnBox()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-6f, 6f), 6.5f, 0f);
        Instantiate(box, spawnPoint, Quaternion.identity);
    }
}
