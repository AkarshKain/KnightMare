using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Camera GeneralCamera;
    public float horizontalFoV = 90.0f;
    public EnemySpawner enemySpawner;

    public Text ScoreText;
    int Score = 0;
    public AudioMixer mixer;
    public Image Black;

    bool starting = true;
    float blackTimeStamp = 0;

    public GameObject DeathScreen;
    public Text DeathScore;
    public GameObject NewHighScoreText;
    public static bool gamePaused = false;
    public GameObject PauseScreen;

    private void Awake()
    {
        gamePaused = false;
        Black.gameObject.SetActive(true);
    }
    void Start()
    {
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);

        float halfHeight = halfWidth * Screen.height / Screen.width;

        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

        GeneralCamera.fieldOfView = verticalFoV;
        mixer.SetFloat("Music", -80 + PlayerPrefs.GetFloat("Music", 1f) * 80);
        mixer.SetFloat("Sounds", -80 + PlayerPrefs.GetFloat("Sounds", 1f) * 80);
    }

    void Update()
    {
        if (starting)
        {
            blackTimeStamp += Time.deltaTime;
            Black.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, blackTimeStamp / 3));
            if (blackTimeStamp >= 3f)
            {
                starting = false;
                enemySpawner.gameStarted = true;
            }

        } else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowHidePauseMenu();
            }
        }

    }

    public void RefreshScore()
    {
        ++Score;
        ScoreText.text = "Score:\n" + Score;
    }

    public void Death()
    {
        DeathScreen.SetActive(true);
        DeathScore.text = "Your Score:\n" + Score;
        if (PlayerPrefs.GetInt("Score", 0) < Score)
        {
            NewHighScoreText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Score", Score);
            PlayerPrefs.Save();
        }
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ShowHidePauseMenu()
    {
        gamePaused = !gamePaused;
        PauseScreen.SetActive(gamePaused);
    }
}
