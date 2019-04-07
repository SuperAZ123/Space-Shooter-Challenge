using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardcount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text CreatorText;
    private bool gameover;
    private bool restart;
    private int score;

    void Start()
    {
        score = 0;
        gameover = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        CreatorText.text = "";
        UpdateScore();
        StartCoroutine (SpawnWaves()); 
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene("Main Challenge");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardcount; i++)

            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameover)
            {
                RestartText.text = "Press 'P' for restart";
                restart = true;
                break;
            }
        }

    }

    public void addScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if (score >= 100)
        {
            GameOverText.text = "You Win!";
            CreatorText.text = "Game Created By Akinyele Zahira";
            gameover = true;
        }
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over!";
        gameover = true;
    }
}
