using System;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class LapChecker : MonoBehaviour
{
    private int laps = 1;
    [SerializeField] private int totalLaps = 0;
    [SerializeField] private Checkpoint[] checkpoints;


    [SerializeField] private TMP_Text lapInfo;

    //Referencias
    [SerializeField] private TimeController timer;
    [SerializeField] private GhostRecorder recorder;
    [SerializeField] private GhostPlayer ghostPlayer;
    [SerializeField] private AutoCam cam;


    //Events
    public static event Action<int> OnLapCompleted;
    private void Awake()
    {
        ResetCheckpoints();
        UpdateUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && AllCheckpointsValidated())
        {
            AddLap();

            if (laps > totalLaps) EndGame();
            ResetCheckpoints();
        }
    }

    private bool AllCheckpointsValidated()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (!checkpoints[i].validated) return false;
        }
        return true;
    }

    private void UpdateUI()
    {
        lapInfo.text = "Lap: " + laps + " / " + totalLaps;
    }

    private void AddLap()
    {
        laps++;
        OnLapCompleted?.Invoke(laps);
        UpdateUI();
    }

    private void EndGame()
    {
        recorder.StopRecording();
        cam.enabled = false;

        if (timer.IsBestRace())
        {
            timer.SaveBestRace();
            recorder.SaveGhost();
            ghostPlayer.StartGhost();
        }
        else
        {
            recorder.ghostData.ResetData();
        }
    }

    private void ResetCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].validated = false;
        }
    }
}
