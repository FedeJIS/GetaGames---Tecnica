using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class contains all the behaviours and data to represent a Countdown Gamemode.
public class GameModeManager : MonoBehaviour
{
    public delegate void GameOverDelegate();
    public event GameOverDelegate gameOverEvent; 
    public TextMeshProUGUI txt_Timer;
    public GameObject panel;
    [SerializeField]
    private int startTime = 60;
    private float timeRemaining;
    private int gamesPlayed = 0;
    private int gamesWon = 0;
    private int gamesLost = 0;
    private float bestTime = 99999;
    private bool gameOver;

    private AudioManager myAudioManager;


    // Start is called before the first frame update
    void Start()
    {
        myAudioManager = GameObject.FindObjectOfType<AudioManager>();
        timeRemaining = startTime;
        txt_Timer = GameObject.FindGameObjectWithTag("Text_Time").GetComponent<TextMeshProUGUI>();
        panel.SetActive(false);
        LoadJSON();
    }

    // Update is called once per frame
    void Update()
    {
      if(!gameOver) Countdown();
    }

    //This method represents a counter.
    private void Countdown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                SetWinState(false);
        }
       txt_Timer.text = "TIME: "+ Mathf.RoundToInt(timeRemaining);
    }

    //Adds given time to counter
    public void AddTime(float time)
    {
        timeRemaining+=time;
    }

    //Sets the win state. If player has lost: Lose scene is loaded. Else Win scene is loaded.
    public void SetWinState(bool flag)
    {
        panel.SetActive(true);
        gamesPlayed++;
        if(flag){
            panel.GetComponentInChildren<TextMeshProUGUI>().text = "YOU WIN!"; 
            gamesWon++;
            if(startTime - timeRemaining < bestTime) bestTime = startTime - timeRemaining;
            myAudioManager.PlayClip(1);
        }
        else{ 
            panel.GetComponentInChildren<TextMeshProUGUI>().text = "GAME OVER!"; 
            gamesLost++;
            myAudioManager.PlayClip(2);
            }
        gameOver = true;
        gameOverEvent();
        SaveJSON();
    }

    //Handles screen transitions
    public void GoMenu()
    {
        SceneLoading sceneLoader = GameObject.FindObjectOfType<SceneLoading>();
        if(sceneLoader != null)sceneLoader.LoadScene(0);
        else SceneManager.LoadScene(0);
    }

    //Handles JSON saving Player Stats.
    public void SaveJSON()
    {
       PlayerStats save = new PlayerStats();
       save.gamesPlayed = gamesPlayed;
       save.bestTime = bestTime;
       save.gamesLost = gamesLost;
       save.gamesWon = gamesWon;
       string g = JsonUtility.ToJson(save);
       string jsonFilePath = Application.dataPath + "/playerstats.json";
       File.WriteAllText(jsonFilePath, g);
    }

    //Handles JSON loading Player Stats.
    public void LoadJSON()
    {
        string jsonFilePath = Application.dataPath + "/playerstats.json";
        PlayerStats load = new PlayerStats();
        if(File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            load = JsonUtility.FromJson<PlayerStats>(json);
            gamesPlayed = load.gamesPlayed;
            bestTime = load.bestTime;
            gamesLost = load.gamesLost;
            gamesWon = load.gamesWon;
            
        }
    }
}


