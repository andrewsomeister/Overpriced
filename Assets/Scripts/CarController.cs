using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public Transform respawnPoint;
    public AudioClip carPassingSound;
    public float startDelay = 3f; // Add this line for the delay

    private AudioSource audioSource;
    private float timer;
    public float respawnTime = 10f;
    private bool isMoving = false; // Add this line to control the movement

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("StartMoving", startDelay); // Add this line to start moving after the delay
    }

    private void StartMoving()
    {
        isMoving = true; // This will start the movement
    }

    private void Update()
    {
        if (isMoving) // Only move if isMoving is true
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer >= respawnTime)
            {
                timer = 0f;
                transform.position = respawnPoint.position;
                //audioSource.PlayOneShot(carPassingSound);

                isMoving = false; // Stop moving
                Invoke("StartMoving", startDelay); // And wait for the delay before moving again
            }
        }
    }
}
