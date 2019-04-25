using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public JoystickInput JSI;
    float distanceToTarget;
    Vector3 translation;
    Rigidbody rigid;
    public float moveSpeed;
    ConfigurableJoint cJoint;
    
    public float pushStrength = 2;
    public GameObject hoist, haloArm, pivot;
    public float rotateSpeed, leanAmount;

	void Start () {
        rigid = GetComponent<Rigidbody>();
        cJoint = GetComponentInParent<ConfigurableJoint>();
	}

    void Move()
    {
        if(JSI.input != null && JSI.input != Vector2.zero)
        {
            Vector3 pushForce = new Vector3(JSI.input.x, 0, JSI.input.y) * pushStrength;
            rigid.AddForce(pushForce, ForceMode.Acceleration);
        }
    }
	
	void Update () {
        Move();
        //Lean();
       //cJoint.targetRotation
        
    }
    void Lean()
    {
        hoist.transform.Rotate(0,rotateSpeed,0);
        haloArm.transform.position = new Vector3(leanAmount, 0, 0);
        pivot.transform.LookAt(haloArm.transform.position);
        Vector3 leanDirection = new Vector3(haloArm.transform.position.x, transform.position.y, haloArm.transform.position.z) - transform.position;
        rigid.AddForce(leanDirection.normalized, ForceMode.Impulse);
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
