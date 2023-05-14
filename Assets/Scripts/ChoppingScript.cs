using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingScript : MonoBehaviour
{
    // Assign the sliced prefabs in the Unity Inspector
    public GameObject choppedLettuce;

    // Set a threshold force for the knife to chop the vegetables
    public float choppingForceThreshold = 0.1f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the knife collided with the lettuce or tomato
        if (collision.relativeVelocity.magnitude > choppingForceThreshold)
        {
            if (collision.gameObject.CompareTag("Lettuce"))
            {
                // Instantiate the sliced lettuce prefab at the original lettuce position
                GameObject choppedLettucee = Instantiate(choppedLettuce, collision.gameObject.transform.position, Quaternion.identity);

                // Destroy the original lettuce GameObject
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Tomato"))
            {
                // Instantiate the sliced tomato prefab at the original tomato position
                //GameObject slicedTomato = Instantiate(slicedTomatoPrefab, collision.gameObject.transform.position, Quaternion.identity);

                // Destroy the original tomato GameObject
                Destroy(collision.gameObject);
            }
        }
    }
}