using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    public Camera GeneralCamera;
    public float horizontalFoV = 90.0f;

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject ExitMenu;

    public Text Highscore;


    public AudioMixer mixer;
    public Image Black;

    bool gameStarted = false;
    float blackTimeStamp = 0;
        
    void Start ()
    {
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);

        float halfHeight = halfWidth * Screen.height / Screen.width;

        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

        GeneralCamera.fieldOfView = verticalFoV;
        mixer.SetFloat("Music", -80 + PlayerPrefs.GetFloat("Music", 1f) * 80);
        mixer.SetFloat("Sounds", -80 + PlayerPrefs.GetFloat("Sounds", 1f) * 80);
        int score = PlayerPrefs.GetInt("Score", 0);
        if (score > 0)
        {
            Highscore.text = "Highest Score:\n"+PlayerPrefs.GetInt("Score", 0);
        } else
        {
            Highscore.gameObject.SetActive(false);
        }
    }
	
	void Update ()
    {
		if (gameStarted)
        {
            blackTimeStamp += Time.deltaTime;
            Black.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, blackTimeStamp / 1));
            if (blackTimeStamp >= 1f)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
	}

    public void StartGame()
    {
        gameStarted = true;
        Black.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
