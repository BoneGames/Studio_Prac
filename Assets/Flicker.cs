using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour {

    GameObject streetLight;

    void Start() {
        streetLight = transform.GetChild(0).gameObject;
        StartCoroutine("FlickerLight");
    }
	
	IEnumerator FlickerLight()
    {
        streetLight.SetActive(true);

        float holdTime = Random.Range(10f, 75f);
        yield return new WaitForSeconds(holdTime);

        int length = Random.Range(2, 10);
        for (int i = 0; i < length; i++)
        {
            if (streetLight.activeSelf)
            {
                streetLight.SetActive(false);
            }
            else
            {
                streetLight.SetActive(true);
            }

            float flickr = Random.Range(0, 0.75f);
            yield return new WaitForSeconds(flickr);
        }

        StartCoroutine("FlickerLight");
    }
}
