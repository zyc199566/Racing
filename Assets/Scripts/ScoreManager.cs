using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _Instance;
    public static ScoreManager Instance
    {
        get { return _Instance; }
    }
    private int nextBallScore;
    private float[] playerScore;
    private Text[] scoreText;
    private Text[] timingText;
    //private Slider ballRefreshTiming;
    public int waitTime;
    public int totalBallCount;
    private Text winText;
    void Awake()
    {
        _Instance = this;
    }
    void Start()
    {
        // ballRefreshTiming = GameObject.Find("Canvas/RefreshTiming").GetComponent<Slider>();
        //ballRefreshTiming.gameObject.SetActive(false);
        newInitiate();
        varInitiate();
        refreshScoreText();
        timingText[0].gameObject.SetActive(false);
        timingText[1].gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        StartCoroutine(nextBall());
    }
    private void newInitiate()
    {
        scoreText = new Text[2];
        playerScore = new float[2];
        timingText = new Text[2];
        scoreText[0] = GameObject.Find("Canvas/Score1").GetComponent<Text>();
        scoreText[1] = GameObject.Find("Canvas/Score2").GetComponent<Text>();
        timingText[0] = GameObject.Find("Canvas/Timing1").GetComponent<Text>();
        timingText[1] = GameObject.Find("Canvas/Timing2").GetComponent<Text>();
        winText = GameObject.Find("Canvas/WinText").GetComponent<Text>();
    }
    private void varInitiate()
    {
        nextBallScore = 1;
        playerScore[0] = 0;
        playerScore[1] = 0;
    }
    public void getBall(int playernum)
    {
        getScore(playernum, nextBallScore);
        totalBallCount--;
        if (totalBallCount <= 0)
        {
            if (playerScore[0] > playerScore[1])
            {
                winText.text = "Player1 Win!";
            }
            else if (playerScore[0] < playerScore[1])
            {
                winText.text = "Player2 Win!";
            }
            else if (playerScore[0] == playerScore[1])
            {
                winText.text = "Draw!";
            }
            winText.gameObject.SetActive(true);
        }
        else
        {
            nextBallScore++;
            StartCoroutine(nextBall());
        }
    }
    public void getScore(int playernum,int score)
    {
        playerScore[playernum] +=score;
        refreshScoreText();
    }
    public void refreshScoreText()
    {
        scoreText[0].text = "player1 score:" + playerScore[0];
        scoreText[1].text = "player2 score:" + playerScore[1];
    }
    IEnumerator nextBall()
    {
        timingText[0].gameObject.SetActive(true);
        timingText[1].gameObject.SetActive(true);
        /*
        ballRefreshTiming.gameObject.SetActive(true);
        ballRefreshTiming.value = 0;
        while(ballRefreshTiming.value<1)
        {
            ballRefreshTiming.value += Time.deltaTime * ballRefreshSpeed;
            yield return null;
        }
        ballRefreshTiming.gameObject.SetActive(false);
        */
        for (int i=waitTime;i>0;i--)
        {
            timingText[0].text = "" + i;
            timingText[1].text = "" + i;
            yield return new WaitForSeconds(1);
        }
        timingText[0].gameObject.SetActive(false);
        timingText[1].gameObject.SetActive(false);
        Instantiate(Resources.Load("PointBall"), new Vector3((Random.value-0.5f)*90, 1, (Random.value-0.5f)*90), new Quaternion());
    }
}
