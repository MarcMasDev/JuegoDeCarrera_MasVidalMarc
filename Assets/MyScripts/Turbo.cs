using UnityEngine;

public class Turbo : MonoBehaviour
{
    [Header("Turbo Settings")]
    public float maxTurbo = 100f;
    [SerializeField] private float turboDrainSpeed = 30f;
    [SerializeField] private float turboRechargeSpeed = 20f;

    [Header("Movement")]
    [SerializeField] private float turboForce = 20f;
    private Rigidbody rb;

    [Header("Particles")]
    [SerializeField] private ParticleSystem turboParticles;

    private float currentTurbo = 0;
    private bool canBoost = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTurbo = maxTurbo;
        SetParticles(false);
    }

    private void Update()
    {
        if (currentTurbo >= maxTurbo) canBoost = true;

        if (Input.GetKey(KeyCode.Space) && canBoost) Boost();
        else RecoverTurbo();

        if (Input.GetKeyUp(KeyCode.Space) || currentTurbo <= 0f) canBoost = false;
    }

    private void Boost()
    {
        currentTurbo -= turboDrainSpeed * Time.deltaTime;
        currentTurbo = Mathf.Clamp(currentTurbo, 0f, maxTurbo);

        rb.AddForce(transform.forward * turboForce, ForceMode.Acceleration);

        SetParticles(true);
    }

    private void RecoverTurbo()
    {
        currentTurbo += turboRechargeSpeed * Time.deltaTime;
        currentTurbo = Mathf.Clamp(currentTurbo, 0f, maxTurbo);

        SetParticles(false);
    }

    public float GetTurbo()
    {
        return currentTurbo;
    }
    public bool CanTurbo()
    {
        return canBoost;
    }
    private void SetParticles(bool activated)
    {
        if (turboParticles == null) return;

        if (activated) turboParticles.Play();
        else turboParticles.Stop();

        turboParticles.gameObject.SetActive(activated);
    }
}
