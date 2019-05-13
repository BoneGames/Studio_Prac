using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snow : MonoBehaviour
{
    Collider col;
    Rigidbody rigid;
    public float bounds;
    void Start()
    {
        col = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        bounds = col.bounds.size.x;
        rigid.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x > 0.5 - bounds)
        {
            rigid.AddForce(new Vector3(Random.Range(0f, -1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode.Impulse);
        }
        else if(transform.localPosition.x < -0.5 + bounds)
        {
            rigid.AddForce(new Vector3(Random.Range(0f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode.Impulse);
        }

        if (transform.localPosition.z > 0.5 - bounds)
        {
            rigid.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(0f, -1f)), ForceMode.Impulse);
        }
        else if (transform.localPosition.z < -0.5 + bounds)
        {
            rigid.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(1f, 0f)), ForceMode.Impulse);
        }

        if (transform.localPosition.y > 1 - bounds)
        {
            rigid.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 0f), Random.Range(-1f, 1f)), ForceMode.Impulse);
        }
        else if (transform.localPosition.y < -1 + bounds)
        {
            rigid.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 0f), Random.Range(-1f, 1f)), ForceMode.Impulse);
        }
    }
}
