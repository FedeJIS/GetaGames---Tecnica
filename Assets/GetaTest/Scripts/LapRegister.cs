using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//This class handles the Lap counting and registering. If you want to modify the amount of laps to race try setting lapsToRace var.
public class LapRegister : MonoBehaviour
{
    public int lapsToRace = 1;
    private int currentLap = 0;
    public int checkpointsReached = 0;
    public ArrayList lapCheckpoints = new ArrayList();
    protected GameModeManager myManager;

    protected TextMeshProUGUI txt_laps;
    private void Awake() {
         myManager = GameObject.FindObjectOfType<GameModeManager>();
         txt_laps = GameObject.FindGameObjectWithTag("Text_Laps").GetComponent<TextMeshProUGUI>();
         txt_laps.text = "LAPS: "+currentLap+"/"+lapsToRace;
         foreach(LapCheckpoint lap in GetComponentsInChildren<LapCheckpoint>())
            lapCheckpoints.Add(lap);
    }

    public void AddCheckpoint()
    {
        checkpointsReached++;
    }
    private void EnableCheckpoints()
    {
        foreach(LapCheckpoint lap in GetComponentsInChildren<LapCheckpoint>())
           ((LapCheckpoint)lap).Restore();
    }

    public void RegisterLap()
    {
        if(checkpointsReached == lapCheckpoints.Capacity-1){
            currentLap++;
            checkpointsReached = 0;
            EnableCheckpoints();
            txt_laps.text = "LAPS: "+currentLap+"/"+lapsToRace;
        }
        if(currentLap == lapsToRace) myManager.SetWinState(true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
            RegisterLap();
    }
}
