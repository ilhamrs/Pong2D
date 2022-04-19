using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Setting")]
    public int player1Score;
    public int player2Score;
    public float timer;
    public bool isOver;
    public bool goldenGoal;
    public bool isSpawnPowerUp;
    public GameObject ballSpawned;

    [Header("Prefab")]
    public GameObject[] BallPrefab;
    public GameObject[] powerUp;

    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("InGame UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public GameObject goldenGoalUI;

    [Header("Game Over UI")]
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    public GameObject youWin;
    public GameObject youLose;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);

        player1WinUI.SetActive(false);
        player2WinUI.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);

        timer = GameData.instance.gameTimer;
        isOver = false;
        goldenGoal = false;
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (seconds % 20 == 0 && !isSpawnPowerUp)
            {
                StartCoroutine("SpawnPowerUp");
            }
        }
        if (timer <= 0f && !isOver)
        {
            timerText.text = "00:00";
            if (player1Score == player2Score)
            {
                //baru
                Ball[] ball = FindObjectsOfType<Ball>();
                if (ball.Length > 1)
                {
                    for (int i = 0; i < ball.Length - 1; i++)
                    {
                        Destroy(ball[i].gameObject);
                    }
                }
                //baru
                if (!goldenGoal)
                {
                    goldenGoal = true;
                    //Ball[] ball = FindObjectsOfType<Ball>();
                    //for (int i = 0; i < ball.Length; i++)
                    //{
                    //    Destroy(ball[i].gameObject);
                    //}
                    goldenGoalUI.SetActive(true);
                    SpawnBall();
                }
            }
            else
            {
                GameOver();
            }
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public IEnumerator SpawnPowerUp()
    {
        isSpawnPowerUp = true;
        Debug.Log("Power Up");
        int rand = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[rand], new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-2.35f, 2.25f), 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        isSpawnPowerUp = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.UIClickSfx();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        SoundManager.instance.UIClickSfx();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void SpawnBall()
    {
        Debug.Log("Muncul Bola");
        StartCoroutine("DelaySpawn");
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        switch (GameData.instance.ball)
        {
            case "Ball":
                if (ballSpawned == null)
                {
                    Instantiate(BallPrefab[0], Vector3.zero, Quaternion.identity);
                }
                break;
            case "Ball 1":
                if (ballSpawned == null)
                {
                    Instantiate(BallPrefab[1], Vector3.zero, Quaternion.identity);
                }
                break;
            case "Ball 2":
                if (ballSpawned == null)
                {
                    Instantiate(BallPrefab[2], Vector3.zero, Quaternion.identity);
                }
                break;
        }
        //if (ballSpawned == null)
        //{
        //    Instantiate(BallPrefab, Vector3.zero, Quaternion.identity);
        //}   
    }

    public void GameOver()
    {
        SoundManager.instance.GameOverSfx();
        isOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0;
        
        GameOverPanel.SetActive(true);

        if (!GameData.instance.isSinglePlayer)
        {
            if (player1Score > player2Score)
            {
                player1WinUI.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                player2WinUI.SetActive(true);
            }
        }
        else
        {
            if (player1Score > player2Score)
            {
                youWin.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                youLose.SetActive(true);
            }
        }
    }
}
