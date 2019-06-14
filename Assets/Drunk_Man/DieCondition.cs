using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCondition : MonoBehaviour
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

    void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        //sparkles = GetComponentsInChildren<ParticleSystem>();
        rigid = GetComponent<Rigidbody>();
    }

    void KillPlayer()
    {
        Debug.Log("Kill Player");
        col.enabled = false;
        rend.enabled = false;
        rigid.isKinematic = true;

        arms.AddComponent<Rigidbody>();
        arms.GetComponent<MeshCollider>().convex = true;
        head.AddComponent<Rigidbody>();

        arms.transform.parent = null;
        head.transform.parent = null;

       

        for (int i = 0; i < sparkles.Length; i++)
        {
            sparkles[i].Stop();
            dieSparkles[i].Play();
            //var emissionModule = sparkles[i].emission;
            //emissionModule.rateOverTime = rate;
            //var triggerModlule = sparkles[i].trigger;
            //triggerModlule.enabled = false;

            //var velocityOverLifetimeModule = sparkles[i].velocityOverLifetime;
            //velocityOverLifetimeModule.enabled = true;
            //velocityOverLifetimeModule.speedModifier = velocityOverLifetime;

            //var collisionModule = sparkles[i].collision;
            //collisionModule.enabled = true;
            //collisionModule.dampen = dampen;

            //var main = sparkles[i].main;
            //// increase particle lifetime
            //main.startLifetime = lifeTime;

            //main.startSpeed = speed;
            //// turn off looping
            //main.loop = false;
            //// set gravity
            //main.gravityModifier = gravity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("force: "+collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > killCollision)
        {

        }
            audioSource.Play();
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
