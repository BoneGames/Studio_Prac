using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {

    Renderer rend;
    Rigidbody rigid;
    public Vector3 startForce;
    float x;
    float y;
    public float moveSpeed;
    bool isGrabbed = false;
    float dragSpeed;
    public GameObject explosion;
    public int reflectMulti;

	void Start () {
        rigid = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        

        x = Random.Range(-1f, 1f);
        y = Random.Range(0f, -1f);
        startForce = new Vector3(x, y, 0);
        rigid.AddForce(startForce, ForceMode.Impulse);

    }

    //IEnumerator HitWall(GameObject sphere)
    //{
    //    sphere.GetComponent<Collider>().enabled = false;

    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.name == "LeftWall")
    //    {
    //        Debug.Log(other.name);
    //        Vector3 reflectForce = Vector3.Reflect(other.transform.position, Vector3.left);
    //        rigid.AddForce(reflectForce * reflectMulti);
    //        //translation.x *= -1f;
    //        //translation = new Vector3(x, y, 0);
    //    }
    //    if(other.name == "RightWall")
    //    {
    //        Debug.Log(other.name);
    //        Vector3 reflectForce = Vector3.Reflect(other.transform.position, Vector3.right);
    //        rigid.AddForce(reflectForce *reflectMulti);
    //    }
    //}

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Sphere" && !isGrabbed)
        {
            Vector3 impact = transform.position - collision.transform.position;
            collision.transform.GetComponent<Rigidbody>().AddForce(impact * ((1 + dragSpeed) * 20), ForceMode.Impulse);
            //Debug.Log("Impact: " + impact);
        }
    }
    


    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            StopMoveRoutine();
            if(isGrabbed)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        if (isGrabbed)
        {
            Vector2 MouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            dragSpeed = MouseMovement.magnitude;
            //Debug.Log("DragSpeed: " + dragSpeed);
        }
    }

    

    public void MoveObject()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float _x = position.x;
        float _y = position.y;
        //Debug.Log(position);

        transform.position = new Vector3(_x, _y, 0);
    }

    public IEnumerator TouchCoroutine()
    {
        //Debug.Log("COROUTINE");
        rend.material.color = Color.red;
        isGrabbed = true;
        rigid.useGravity = false;
        while(true)
        {
            MoveObject();
            yield return null;
        }
    }

    void StopMoveRoutine()
    {
        rigid.useGravity = true;
        StopAllCoroutines();
    }

    public void OnTouchDown()
    {
        rend.material.color = Color.blue;
    }
    public void OnTouchUp()
    {
        rend.material.color = Color.white;
    }
    public void OnTouchStay()
    {
        rend.material.color = Color.green;
    }
    public void OnTouchExit()
    {
        rend.material.color = Color.yellow;
    }
}
