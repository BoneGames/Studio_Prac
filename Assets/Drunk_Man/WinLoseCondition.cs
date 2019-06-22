using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseCondition : MonoBehaviour
{
    public float fallAngle;
    Collider col;
    Renderer rend;
    Rigidbody rigid;
    public ParticleSystem[] sparkles = new ParticleSystem[2];
    public ParticleSystem[] dieSparkles = new ParticleSystem[2];
    public GameObject arms, head;

    public float lifeTime, gravity, dampen, rate, killCollision;
    public ParticleSystem.MinMaxCurve speed, velocityOverLifetime;

    public bool alive;

    AudioSource audioSource;
    public UI ui;

    void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        //sparkles = GetComponentsInChildren<ParticleSystem>();
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        alive = true;
        ui = FindObjectOfType<UI>();
    }

    void KillPlayer()
    {
        Debug.Log("Kill Player");
        col.enabled = false;
        rend.enabled = false;
        rigid.isKinematic = true;

        arms.AddComponent<Rigidbody>();
        arms.GetComponent<MeshCollider>().enabled = true;
        head.AddComponent<Rigidbody>();

        arms.transform.parent = null;
        head.transform.parent = null;

       

        for (int i = 0; i < sparkles.Length; i++)
        {
            sparkles[i].Stop();
            dieSparkles[i].Play();
          
        }
        if(ui)
        {
            ui.DisplayScore();
        }
        else
        {
            FindObjectOfType<UI>().DisplayScore();
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("force: "+collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > killCollision && alive)
        {
            audioSource.Play();
            KillPlayer();
            alive = false;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            ui.DisplayScore();
        }
    }
    void Update()
    {
        if(Vector3.Angle(Vector3.up, transform.up) > fallAngle && alive)
        {
            KillPlayer();
            alive = false;
        }
    }
}
