using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public JoystickInput JSI;
    Rigidbody rigid;
    ConfigurableJoint cJoint;
    
    public float moveSpeed = 2;
    public GameObject hoist, haloArm, neck;
    public float headRollSpeed, headRollAmount, leanSpeed, leanAmount;
    float leanX, leanZ;
    bool increasingX, increasingZ;


   

	void Start () {
        rigid = GetComponent<Rigidbody>();
        cJoint = GetComponentInParent<ConfigurableJoint>();

        leanX = leanAmount - 0.1f;
        leanZ = -0.1f;
        increasingX = true;
        increasingZ = true;
	}

    void Move()
    {
        if(JSI.input != null && JSI.input != Vector2.zero)
        {
            Vector3 move = new Vector3(JSI.input.x, 0, JSI.input.y) * moveSpeed;
            rigid.AddForce(move, ForceMode.Acceleration);
        }
    }
	
	void Update () {
        Move();
        HeadLoll();
        Lean();
    
    }

    void Lean()
    {
        if(leanX >= leanAmount)
        {
            increasingX = false;
        }
        if(leanX <= -leanAmount)
        {
            increasingX = true;
        }

        if(increasingX)
        {
            leanX += Time.deltaTime * leanSpeed;
        }
        else
        {
            leanX -= Time.deltaTime * leanSpeed;
        }



        if (leanZ >= leanAmount)
        {
            increasingZ = false;
        }
        if (leanZ <= -leanAmount)
        {
            increasingZ = true;
        }

        if (increasingZ)
        {
            leanZ += Time.deltaTime * leanSpeed;
        }
        else
        {
            leanZ -= Time.deltaTime * leanSpeed;
        }

        


        cJoint.targetAngularVelocity = new Vector3(leanX, 0, leanZ);
    }

    void HeadLoll()
    {
        hoist.transform.Rotate(0,headRollSpeed,0);
        haloArm.transform.localPosition = new Vector3(headRollAmount, 0, 0);
        Vector3 direction = (haloArm.transform.position - neck.transform.position).normalized;
        neck.transform.up = direction;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Wall")
        {
            Vector3 direction = transform.position - other.contacts[0].point;
            rigid.AddForce(direction * 10, ForceMode.Impulse);
        }
    }
}
