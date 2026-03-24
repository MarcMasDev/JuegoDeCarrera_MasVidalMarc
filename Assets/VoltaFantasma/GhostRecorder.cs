using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public GhostLapData ghostData;
    public GhostLapData bestGhostData;
    public Transform car;
    private Vector3 lastPos;
    private float time = 0;

    public float distanceBetweenSamples = 5f;

    private bool recording = false;
    private void Start()
    {
        StartRecording();
    }
    private void OnEnable()
    {
        Checkpoint.OnCheckpointReached += RecordCheckpoint;
        LapChecker.OnLapCompleted += RecordLap;
    }

    private void OnDisable()
    {
        Checkpoint.OnCheckpointReached -= RecordCheckpoint;
        LapChecker.OnLapCompleted -= RecordLap;
    }
    void Update()
    {
        if (!recording) return;

        time += Time.deltaTime;


        if (Vector3.Distance(car.transform.position, lastPos) >= distanceBetweenSamples)
        {
            ghostData.AddNewData(car, time);
            lastPos = car.position;
        }
    }

    private void StartRecording()
    {
        ghostData.ResetData();
        recording = true;

        ghostData.AddNewData(car, time);
        lastPos = car.transform.position;
        time = 0;
    }

    public void StopRecording()
    {
        recording = false;
    }

    public void SaveGhost()
    {
        if (ghostData.carTimes.Count > 0)
        {
            bestGhostData.SetData(ghostData);
        }
    }

    private void RecordCheckpoint(Checkpoint checkpoint)
    {
        ghostData.checkpointTimes.Add(time);
    }
    private void RecordLap(int lap)
    {
        ghostData.checkpointTimes.Add(time);
    }
}
