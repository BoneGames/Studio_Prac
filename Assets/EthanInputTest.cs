using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanInputTest : MonoBehaviour
{
    public Animator anim;
    float speed;
    void Start()
    {
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            speed += 0.5f;
            if(speed > 1)
            {
                speed = 0;
            }
        }
        anim.SetFloat("Speed", speed);

    }
}
