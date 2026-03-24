using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    [SerializeField] private GhostLapData bestData;
    [SerializeField] private Transform ghostCar;

    private float ghostTime = 0;

    private int currentSample = 0;

    private Vector3 lastPos;
    private Vector3 nextPos;

    private Quaternion lastRot;
    private Quaternion nextRot;

    private void Start()
    {
        gameObject.SetActive(bestData.carTimes.Count>0);
    }

    private void Update()
    {
        if (ghostTime >= bestData.carTimes[bestData.carTimes.Count - 1]) return;

        ghostTime += Time.deltaTime;

        while (currentSample < bestData.carTimes.Count - 2 && bestData.carTimes[currentSample + 1] < ghostTime) 
            currentSample++;


        Vector3 p1 = bestData.carPositions[currentSample];
        Vector3 p2 = bestData.carPositions[currentSample + 1];

        Quaternion r1 = bestData.carRotations[currentSample];
        Quaternion r2 = bestData.carRotations[currentSample + 1];

        float t1 = bestData.carTimes[currentSample];
        float t2 = bestData.carTimes[currentSample + 1];

        float lerp = Mathf.InverseLerp(t1, t2, ghostTime);

        ghostCar.position = Vector3.Lerp(p1, p2, lerp);
        ghostCar.rotation = Quaternion.Slerp(r1, r2, lerp);
    }


    public void StartGhost()
    {
        if (bestData.carTimes.Count < 2) return;

        ghostTime = 0;
        currentSample = 0;

        ghostCar.position = bestData.carPositions[0];
        ghostCar.rotation = bestData.carRotations[0];
    }
}
