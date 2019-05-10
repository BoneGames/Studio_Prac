using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAbout : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        InvokeRepeating("Bounce", 0, 0.5f);
    }

    void Bounce()
    {
        float x, y, z;
        x = Random.Range(-2f, 2f);
        y = Random.Range(-2f, 2f);
        z = Random.Range(-2f, 2f);
        Vector3 force = new Vector3(x, y, z);
        rigid.AddForce(force, ForceMode.Impulse);
    }
}
