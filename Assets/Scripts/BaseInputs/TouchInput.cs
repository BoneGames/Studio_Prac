using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    public LayerMask touchMask;
    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchOld;
    private RaycastHit hit;


    void Start()
    {

    }

    public void MoveObject()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
        {
            touchOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchOld);
            touchList.Clear();

        

            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, touchMask))
            {
                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);

                /*
                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchDown");
                }
                */
                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("TouchCoroutine", hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchDown");
                }
                /*
                if (Input.GetMouseButton(0))
                {
                    recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchStay");
                    recipient.SendMessage("MoveObject", SendMessageOptions.DontRequireReceiver);
                    //MoveObject(recipient);
                }
                */
                if (Input.GetMouseButtonUp(0))
                {
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchUp");
                }
            }
            foreach (GameObject item in touchOld)
            {
                if (!touchList.Contains(item))
                {
                   // item.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    //Debug.Log("OnTouchExit");
                }
            }
        }
#endif


        
        if(Input.touchCount > 0)
        {
            touchOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
                if(Physics.Raycast(ray, out hit, touchMask))
                {
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);
                    if(touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchDown");
                    }
                    if (touch.phase == TouchPhase.Stationary)
                    {
                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchStay");
                    }
                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchExit");
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchUp");
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchMoving", hit.point, SendMessageOptions.DontRequireReceiver);
                        Debug.Log("OnTouchMoving");
                    }
                }
            }
            foreach(GameObject touch in touchOld)
            {
                if(!touchList.Contains(touch))
                {
                    touch.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("OnTouchExit");
                }
            }
        }


    }
}
