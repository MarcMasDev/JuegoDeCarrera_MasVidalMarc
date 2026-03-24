using System;
using UnityEngine;
public class StuckDetector : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CanvasGroup restartUI;

    [SerializeField] private float speedThreshold = 1f;
    [SerializeField] private float stuckTime = 3f;

    private float timer = 0f;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

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

    private void Start()
    {
        restartUI.alpha = 0;
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void Update()
    {
        float speed = playerRb.linearVelocity.magnitude;

        if (speed < speedThreshold)
        {
            timer += Time.deltaTime;

            if (timer >= stuckTime) restartUI.alpha = 1;
        }
        else
        {
            timer = 0f;
            restartUI.alpha = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCar();
        }
    }

    private void RestartCar()
    {
        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;

        transform.position = lastPosition;
        transform.rotation = lastRotation;

        timer = 0f;
        restartUI.alpha = 0;
    }

    private void NewLap(int lapNum)
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void HandleCheckpoint(Checkpoint checkpoint)
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
}
