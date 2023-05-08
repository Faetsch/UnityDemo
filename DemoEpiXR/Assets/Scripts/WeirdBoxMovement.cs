
using UnityEngine;

public class WeirdBoxMovement : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] float bounds = 5f;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float cooldownTime = 3f;
    [SerializeField] float lerpTime = 3f;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    Vector3 newPosition;
    Rigidbody rb;
    bool cooldown = false;
    float passedTime = 0f;
    Vector3 startPos;
    float passedLerpTime = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        meshRenderer.material.color = endColor;
        CalculateNewPosition();
    }

    void Update()
    {
        HandleTimers();
        LerpToNewColor();
    }

    void FixedUpdate()
    {
        MoveBoxToNewPosition();
    }

    private void HandleTimers()
    {
        passedTime += Time.deltaTime;
        passedLerpTime += Time.deltaTime;

        if (passedTime >= cooldownTime)
        {
            cooldown = false;
            passedTime = 0f;
        }
    }

    private void LerpToNewColor()
    {
        if(cooldown)
        {
            if(passedTime < cooldownTime)
            {
                meshRenderer.material.color = Color.Lerp(startColor, endColor, passedTime / cooldownTime);
            } 
            else
            {
                meshRenderer.material.color = endColor;
            }
        }
    }


    private void MoveBoxToNewPosition()
    {
        if (passedLerpTime < lerpTime)
        {
            rb.MovePosition(Vector3.Lerp(startPos, newPosition, passedLerpTime / lerpTime));
        }
        else
        {
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!cooldown && other.CompareTag("Player"))
        {
            PlayEffects();
            CalculateNewPosition();
            TriggerCooldown();
        }
    }

    private void PlayEffects()
    {
        particle.Play();
        audioSource.Play();
    }

    private void CalculateNewPosition()
    {
        startPos = transform.position;
        newPosition = new Vector3(Random.Range(-bounds, bounds), transform.position.y, Random.Range(-bounds, bounds));
        passedLerpTime = 0f;
    }

    private void TriggerCooldown()
    {
        cooldown = true;
        passedTime = 0f;
    }
}
