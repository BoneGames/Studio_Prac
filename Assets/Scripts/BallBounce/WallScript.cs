using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

    AudioSource aS;
    Renderer rend;
    public float colorDuration;
    Color startCol;
    Color changeCol;

    public enum ColorPick
    {
        Red,
        Blue,
        Green
    }

    public ColorPick colorPicker;
    public ColorPick ColorPicker
    {
        get { return colorPicker; }
        set
        {
            colorPicker = value;
            SetEnum();
        }
    }

    void SetEnum()
    {
        Debug.Log("set enum");
        switch (colorPicker)
        {
            case ColorPick.Blue:
            changeCol = Color.blue;
            break;

            case ColorPick.Red:
            changeCol = Color.red;
            break;

            case ColorPick.Green:
            changeCol = Color.green;
            break;
        }
     
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        aS = GetComponent<AudioSource>();
        startCol = rend.material.color;
    }

    IEnumerator HitWall(GameObject sphere)
    {
        sphere.GetComponent<Collider>().enabled = false;
        //aS.Play(); // Get Pokie machine register noise
        rend.material.color = changeCol;
        yield return new WaitForSeconds(colorDuration);
        rend.material.color = startCol;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sphere")
        {
            Debug.Log(other.name);
            StartCoroutine(HitWall(other.gameObject));
        }
    }
}
