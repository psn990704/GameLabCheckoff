using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HUDManager : Singleton<HUDManager>
{

    public GameObject scoreText;
    public GameObject gameOverScoreText;

    public GameObject gameOverUI;
    public GameObject onGameUI;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
        gameOverUI.SetActive(false);
        onGameUI.SetActive(true);
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        gameOverScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        gameOverUI.SetActive(true);
        onGameUI.SetActive(false);
    }
}
