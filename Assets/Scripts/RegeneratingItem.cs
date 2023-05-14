using UnityEngine;
using System.Collections;

public class RegeneratingItem : MonoBehaviour
{
    public float regenerationTime = 5f;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isRegenerating = false;
    private GameObject originalParent;

    private Grabbable grabbableScript;

    void Start()
    {
        // Store the original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalParent = transform.parent.gameObject;

        grabbableScript = GetComponent<Grabbable>();
    }

    void Update()
    {
        // Check if the item is being held and if it's not already regenerating
        if (grabbableScript != null && grabbableScript.HasBeenGrabbed)
        {
            // Start the regeneration process
            StartCoroutine(Regenerate());
            Instantiate(gameObject, originalPosition, originalRotation, originalParent.transform);
        }
    }

    IEnumerator Regenerate()
    {


        // Wait for the specified time
        yield return new WaitForSeconds(regenerationTime);

        // Create a new instance of the object at the original position and rotation
        GameObject newItem = Instantiate(gameObject, originalPosition, originalRotation, originalParent.transform);

        // Disable the RegeneratingItem script on the original object (this)
        this.enabled = false;


    }

}
