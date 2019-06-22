using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Transform player;
    public Text timer, score, winDie;
    public float startTime;
    public int respawnDelay;
    float currentTime;
    public float highScore;
    Vector3 startPos;
    public float bonusScore;
    WinLoseCondition wlC;
    // Start is called before the first frame update
    void Start()
    {
        StartStuff();
    }

    void StartStuff()
    {
        wlC = FindObjectOfType<WinLoseCondition>();
        player = FindObjectOfType<MovePlayer>().transform;
        currentTime = startTime;
        startPos = player.position;
        if(!FindObjectOfType<UI>())
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
    }

    private void OnEnable()
    {
        Debug.Log("onEnable");
        StartStuff();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Timer
        currentTime -= Time.deltaTime;
        timer.text = "Time Remaining: " + System.Math.Round((double)currentTime, 2);


    }

    public void DisplayScore()
    {
        float _score = Vector3.Distance(startPos, player.position) + bonusScore;
        if(_score > highScore)
        {
            score.text = "High Score: " + _score;
            highScore = _score;
        }
        if(this)
        StopAllCoroutines();
        StartCoroutine(WinDieText(_score));
    }

    IEnumerator WinDieText(float score)
    {
        winDie.enabled = true;
        for (int i = respawnDelay; i > 0; i--)
        {
            winDie.text = "Your Score: " + score;
            yield return new WaitForSeconds(1);
        }
        winDie.enabled = false;
        Respawn();
    }

    void Respawn()
    {
        SceneManager.LoadScene(0);
    }
}
