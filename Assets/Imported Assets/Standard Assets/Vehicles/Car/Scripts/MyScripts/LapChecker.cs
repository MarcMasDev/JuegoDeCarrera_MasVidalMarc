using System;
using TMPro;
using UnityEngine;

public class LapChecker : MonoBehaviour
{
    private int laps = 1;
    [SerializeField] private int totalLaps = 0;
    private Checkpoint[] checkpoints;


    [SerializeField] private TMP_Text lapInfo;
    //Events
    public static event Action<int> OnLapCompleted;
    private void Awake()
    {
        checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None); 
        UpdateUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && AllCheckpointsValidated())
        {
            if (totalLaps >= laps)
            {
                print("END");
            }

            laps++;
            OnLapCompleted?.Invoke(laps);
            UpdateUI();

            for (int i = 0; i < checkpoints.Length; i++)
            {
                checkpoints[i].validated = false;
            }


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
}
