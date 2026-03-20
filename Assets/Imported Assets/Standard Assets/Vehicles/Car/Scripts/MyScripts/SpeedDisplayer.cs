using TMPro;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class SpeedDisplayer : MonoBehaviour
{
    [SerializeField] private CarController carController;
    private TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        text.text = carController.CurrentSpeed.ToString("0");
    }
}
