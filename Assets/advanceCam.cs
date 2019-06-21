using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advanceCam : MonoBehaviour
{

    public Transform playerPos;
    public float camMoveSpeed;

    
    void Update()
    {
        camMoveSpeed = Vector3.Distance(new Vector3(0, 0, playerPos.position.z), new Vector3(0, 0, transform.position.z + 10));
        if (playerPos.position.z > transform.position.z + 10)
        {
            transform.position += new Vector3(0, 0, Time.deltaTime * camMoveSpeed);
        }
    }
}
