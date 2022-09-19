using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> ActiveObjects = new List<GameObject>();
    [NonSerialized] public GameObject _player;

    private float timer = 60;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Text pauseText;

    private void Start()
    {
        timer = 60;
        scoreText.text = 0.ToString();
    }

    private void Update()
    {
        if (!ActiveObjects.Contains(_player))
            ReloadScene();
        if (ActiveObjects.Count <= 1)
            ReloadScene();
        
        Timer();
        DisplayTime(timer);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Timer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            ReloadScene();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = seconds.ToString();
    }

    public void IncreaseScore()
    {
        int score =  int.Parse(scoreText.text);
        score += 1;
        scoreText.text = score.ToString();
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            pauseText.text = "Resume";
            Time.timeScale = 0;
        }
        else
        {
            pauseText.text = "Pause";
            Time.timeScale = 1;
        }
    }
}
