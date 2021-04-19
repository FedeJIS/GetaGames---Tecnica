using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

//This class handles everything regarding to the UI of the main menu.
public class MenuManager : MonoBehaviour
{
    SceneLoading mySceneLoader;
    public TextMeshProUGUI txt_gp;
    public TextMeshProUGUI txt_bt;
    public TextMeshProUGUI txt_gl;
    public TextMeshProUGUI txt_gw;
    
    // Start is called before the first frame update
    void Start()
    {
        mySceneLoader = GameObject.FindObjectOfType<SceneLoading>();
        txt_gp = GameObject.FindGameObjectWithTag("Text_GP").GetComponent<TextMeshProUGUI>();
        txt_bt = GameObject.FindGameObjectWithTag("Text_BT").GetComponent<TextMeshProUGUI>();
        txt_gl = GameObject.FindGameObjectWithTag("Text_GL").GetComponent<TextMeshProUGUI>();
        txt_gw = GameObject.FindGameObjectWithTag("Text_GW").GetComponent<TextMeshProUGUI>();
        LoadJSON();
    }

    public void Play()
    {
        mySceneLoader.LoadScene(2);
    }

    public void Edit()
    {
        mySceneLoader.LoadScene(3);
    }

    public void LoadJSON()
    {
        string jsonFilePath = Application.dataPath + "/playerstats.json";
        string jsonFilePath_2 = Application.dataPath + "/edit.json";
        PlayerStats load = new PlayerStats();
        if(File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            load = JsonUtility.FromJson<PlayerStats>(json);
            txt_gp.text += load.gamesPlayed;
            txt_bt.text += Mathf.RoundToInt(load.bestTime);
            txt_gl.text += load.gamesLost;
            txt_gw.text += load.gamesWon;
        }
        else
        {
            txt_gp.text += 0;
            txt_bt.text += 0;
            txt_gl.text += 0;
            txt_gw.text += 0;
        }
        /*EditorHelper load2 = new EditorHelper();
        if(File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            load2 = JsonUtility.FromJson<EditorHelper>(json);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MeshFilter>().sharedMesh = load2.mesh;
        }*/
    }
}
