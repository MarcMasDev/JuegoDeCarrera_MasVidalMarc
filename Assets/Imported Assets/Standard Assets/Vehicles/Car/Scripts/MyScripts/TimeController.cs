using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private float lapTime = 0;
    private List<float> lapsTimes = new List<float>();

    [SerializeField] private TMP_Text totalTimeText;
    [SerializeField] private TMP_Text totalLapsText;

    [Header("Pop-up")]
    [SerializeField] private TMP_Text currentTimePopUpText;
    [SerializeField] private TMP_Text allTimePopUpText;
    [SerializeField] private CanvasGroup popUp;

    //Listener
    private void OnEnable()
    {
        Checkpoint.OnCheckpointReached += HandleCheckpoint;
        LapChecker.OnLapCompleted += NewLap;
    }

    private void OnDisable()
    {
        Checkpoint.OnCheckpointReached -= HandleCheckpoint;
        LapChecker.OnLapCompleted -= NewLap;
    }

    private void Update()
    {
        lapTime += Time.deltaTime;
        totalTimeText.text = FormatTime(Time.timeSinceLevelLoad);
        UpdateLapUI();
    }

    void HandleCheckpoint(Checkpoint cp)
    {
        StartCoroutine(SendTimeMessage(cp));
    }

    void NewLap(int lap)
    {
        lapsTimes.Add(lapTime);

        StartCoroutine(SendTimeMessage());

        lapTime = 0;
    }
    private IEnumerator SendTimeMessage(Checkpoint cp = null)
    {
        currentTimePopUpText.text = FormatTime(lapTime);

        bool bestTime = true;

        allTimePopUpText.text = "";

        if (cp != null)
        {
            for (int i = 0; i < cp.lapsTimes.Count; i++)
            {
                if (cp.lapsTimes[i] < lapTime) bestTime = false;

                allTimePopUpText.text += "Lap " + (i + 1) + ": " + FormatTime(cp.lapsTimes[i]) + "\n";
            }

            cp.lapsTimes.Add(lapTime);
        }
        else
        {
            for (int i = 0; i < lapsTimes.Count; i++)
            {
                if (lapsTimes[i] < lapTime) bestTime = false;

                allTimePopUpText.text += "Lap " + (i + 1) + ": " + FormatTime(lapsTimes[i]) + "\n";
            }
        }

        if (bestTime) currentTimePopUpText.color = Color.green;
        else currentTimePopUpText.color = Color.white;

        popUp.alpha = 1;

        yield return new WaitForSeconds(2f);

        popUp.alpha = 0;
    }

    void UpdateLapUI()
    {
        string text = "";

        for (int i = 0; i < lapsTimes.Count; i++)
        {
            text += "Lap " + (i + 1) + ": " + FormatTime(lapsTimes[i]) + "\n";
        }

        text += "Lap " + (lapsTimes.Count + 1) + ": " + FormatTime(lapTime);

        totalLapsText.text = text;
    }


    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        if (minutes <= 0) return seconds.ToString("00.00");
        return minutes.ToString("00") + ":" + seconds.ToString("00.00");
    }
}
