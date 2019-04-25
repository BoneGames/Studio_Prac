using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

    CharacterJoint joint;
    GameObject knee1;
    public bool stepping;
    

	// Use this for initialization
	void Start () {
        joint = GetComponent<CharacterJoint>();
        knee1 = GameObject.Find("Knee");    
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(joint.currentForce.magnitude);
        if (joint.currentForce.magnitude > 2 && !stepping)
        {
            
            Vector3 stepForce = joint.currentForce;
            stepForce = new Vector3(stepForce.x, stepForce.y * 3, stepForce.z);
            Vector3.ClampMagnitude(stepForce, 5);
            Debug.Log(stepForce);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            knee1.GetComponent<Rigidbody>().AddForce(stepForce, ForceMode.Impulse);
            stepping = true;
        }
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (stepping)
        {
            if(collision.transform.name == "Floor")
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                stepping = false;
            }
        }
    }
}
