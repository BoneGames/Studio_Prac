using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

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

    public float leanMultiX, leanMultiY;


    void Start()
    {
        rigid = transform.GetChild(0).GetComponent<Rigidbody>();
        cJoint = GetComponent<ConfigurableJoint>();

        leanX = leanAmount - 0.1f;
        leanZ = -0.1f;
        increasingX = true;
        increasingZ = true;
    }


    void Move()
    {
        Vector3 keyMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));



        if (JSI.input != Vector2.zero || keyMove != Vector3.zero)
        {

            // interupt lean reset
            resettingLean = false;

            Vector3 move;
            if (keyMove == Vector3.zero)
            {
                // get move vector
                move = new Vector3(JSI.input.x, 0, JSI.input.y) * moveSpeed;
            }
            else
            {
                move = keyMove * moveSpeed;
            }

            // apply move force
            rigid.AddForce(move, ForceMode.Acceleration);
            // add lean based on moveDirection

            cJoint.targetRotation *= Quaternion.Euler(-move.x, 0, move.y);

            leanMultiX += move.z * Time.deltaTime/3;
            leanMultiY += move.x * Time.deltaTime/3;

            //if (cJoint.angularXDrive.positionSpring > 0)
            //{
            //    JointDrive drive = new JointDrive();
            //    drive.positionSpring = cJoint.angularXDrive.positionSpring - leanRecoverRate * Time.deltaTime;
            //    drive.positionDamper = 2;
            //    drive.maximumForce = 3;

            //    cJoint.angularXDrive = cJoint.angularYZDrive = drive;
            //}
        }
        else if (cJoint.targetRotation != Quaternion.Euler(0, 0, 0) || leanMultiX != 1 || leanMultiY != 1)// && !resettingLean)
        {
            leanMultiX = Mathf.Lerp(leanMultiX, 0, Time.deltaTime);
            leanMultiY = Mathf.Lerp(leanMultiY, 0, Time.deltaTime);
            //if (cJoint.angularXDrive.positionSpring < 10)
            //{
            //    JointDrive drive = new JointDrive();
            //    drive.positionSpring = cJoint.angularXDrive.positionSpring + leanRecoverRate * Time.deltaTime;
            //    drive.positionDamper = 2;
            //    drive.maximumForce = 3;

            //    cJoint.angularXDrive = drive;
            //    cJoint.angularYZDrive = drive;
            //}
            if(cJoint.targetRotation.eulerAngles.magnitude > 0.1)
            {
                cJoint.targetRotation = Quaternion.Slerp(cJoint.targetRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 2);
            }
        }
    }

    void Update()
    {
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

        cJoint.targetAngularVelocity = new Vector3(leanX, 0, leanZ) + new Vector3(-leanMultiX, 0, leanMultiY);
    }

    void HeadLoll()
    {
        // rotate
        hoist.transform.Rotate(0, headRollSpeed, 0);
        haloArm.transform.localPosition = new Vector3(headRollAmount, 0, 0);
        Vector3 direction = (haloArm.transform.position - neck.transform.position).normalized;
        neck.transform.up = direction;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Wall")
        {
            Vector3 direction = other.contacts[0].point - transform.position;
            Vector3 barrierNormal = other.contacts[0].normal;

            Vector3 outForce = Vector3.Reflect(new Vector3(direction.x, 0, direction.z), barrierNormal);
            rigid.AddForce(outForce * bounceForce, ForceMode.Impulse);
        }
    }
}
