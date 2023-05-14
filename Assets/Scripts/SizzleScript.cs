using UnityEngine;

public class SteakSizzle : MonoBehaviour
{
    public GameObject sizzlingSound; // Assign the SizzlingSound object in the Inspector

    private AudioSource audioSource;

    void Start()
    {
        audioSource = sizzlingSound.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("pan"))
        {
            audioSource.Play();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the steak exited the collision with the pan
        if (collision.gameObject.CompareTag("pan"))
        {
            audioSource.Stop();
        }
    }
}