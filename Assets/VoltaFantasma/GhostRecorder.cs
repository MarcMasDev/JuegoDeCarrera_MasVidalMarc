using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public GhostLapData ghostData;
    public GhostLapData bestGhostData;
    [SerializeField] private bool recordBest = false;
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


        if (Vector3.Distance(transform.transform.position, lastPos) >= distanceBetweenSamples)
        {
            ghostData.AddNewData(transform, time);
            lastPos = transform.position;
        }
    }

    private void StartRecording()
    {
        ghostData.ResetData();
        recording = true;

        ghostData.AddNewData(transform, time);
        lastPos = transform.transform.position;
        time = 0;
    }

    public void StopRecording()
    {
        recording = false;
    }

    public void SaveGhost()
    {
        if (ghostData.carTimes.Count > 0 && recordBest)
        {
            bestGhostData.SetData(ghostData);
        }
    }

    private void RecordCheckpoint(Checkpoint checkpoint)
    {
        if (recordBest) ghostData.checkpointTimes.Add(time);
    }
    private void RecordLap(int lap)
    {
        if (recordBest) ghostData.checkpointTimes.Add(time);
    }
}
