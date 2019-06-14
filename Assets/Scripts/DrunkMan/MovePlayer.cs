using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public JoystickInput JSI;
    Rigidbody rigid;
    ConfigurableJoint cJoint;
    public Transform arms;
    public float armSwingMax;
    public bool armsRight, resettingLean;
    
    public float moveSpeed = 2;
    public GameObject hoist, haloArm, neck;
    public float headRollSpeed, headRollAmount, leanSpeed, leanAmount, resetLeanRate;
    float leanX, leanZ;
    bool increasingX, increasingZ;
    public float bounceForce;

    
	void Start () {
        rigid = transform.GetChild(0).GetComponent<Rigidbody>();
        cJoint = GetComponent<ConfigurableJoint>();

        leanX = leanAmount - 0.1f;
        leanZ = -0.1f;
        increasingX = true;
        increasingZ = true;
	}

    void Move()
    {
        if(JSI.input != null && JSI.input != Vector2.zero)
        {
            // interupt lean reset
            resettingLean = false;
            // get move vector
            Vector3 move = new Vector3(JSI.input.x, 0, JSI.input.y) * moveSpeed;
            // apply move force
            rigid.AddForce(move, ForceMode.Acceleration);
            // add lean based on moveDirection
            cJoint.targetRotation *= Quaternion.Euler(move.x, 0, move.y);
        }
        else if(cJoint.targetRotation != Quaternion.identity)
        {
            resettingLean = true;
            Quaternion startRotation = cJoint.targetRotation;
            LerpLeanToZero(startRotation);

                //Euler(JSI.input.x, 0, JSI.input.y);
        }
    }

    void LerpLeanToZero(Quaternion startRotation)
    {
        float timer = 0;
        while(cJoint.targetRotation != Quaternion.identity || resettingLean)
        {
            timer += Time.deltaTime/resetLeanRate;
            cJoint.targetRotation = Quaternion.Lerp(startRotation, Quaternion.identity, timer);
        }
    }
	
	void Update () {
        Move();
        HeadLoll();
        Lean();
        ArmWobble();
    }

    void ArmWobble()
    {
        float armSwing = Random.Range(0, armSwingMax);
        if (Mathf.Abs(arms.localRotation.eulerAngles.z) > 20)
        {
            armsRight = !armsRight;
            armSwing *= 0.5f;
        }

        arms.rotation *= armsRight ? Quaternion.Euler(0, 0, armSwingMax * Time.deltaTime) : Quaternion.Euler(0, 0, -armSwingMax * Time.deltaTime);
    }
    void Lean()
    {
        if (leanX >= leanAmount)
        {
            increasingX = false;
        }
        if (leanX <= -leanAmount)
        {
            increasingX = true;
        }

        leanX += increasingX ? Time.deltaTime * leanSpeed : -Time.deltaTime * leanSpeed;

        if (leanZ >= leanAmount)
        {
            increasingZ = false;
        }
        if (leanZ <= -leanAmount)
        {
            increasingZ = true;
        }

        leanZ += increasingZ ? Time.deltaTime * leanSpeed : -Time.deltaTime * leanSpeed;

        cJoint.targetAngularVelocity = new Vector3(leanX, 0, leanZ);
    }

    void HeadLoll()
    {
        // rotate
        hoist.transform.Rotate(0,headRollSpeed,0);
        haloArm.transform.localPosition = new Vector3(headRollAmount, 0, 0);
        Vector3 direction = (haloArm.transform.position - neck.transform.position).normalized;
        neck.transform.up = direction;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Wall")
        {
            Vector3 direction = other.contacts[0].point - transform.position;
            Vector3 barrierNormal = other.contacts[0].normal;

            Vector3 outForce = Vector3.Reflect(new Vector3(direction.x, 0, direction.z), barrierNormal);
            rigid.AddForce(outForce * bounceForce, ForceMode.Impulse);
        }
    }
}
