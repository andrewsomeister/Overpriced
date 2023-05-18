using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Cut With ")] public GameObject cutWith; // object that can cut this object
    [Header("Cut Into")] public GameObject cutInto; // Prefab Only,slices, dices etc. 
    [Header("Trash Prefab")] public GameObject trashPrefab; // Prefab Only,slices, dices etc.
    private int cutCount = 0;
    public AudioClip cutAudioClip; 
    public AudioClip destroyAudioCilp;
    public int cutLimit = 4;  // cutting how many times will destroy the object 
    private AudioSource audioSource; // the object that can play the sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int destroyDelayTime = 3; 

    void OnTriggerEnter(Collider other ){
        if (other.gameObject.tag == "knife"){
            Collider myCollider = GetComponent<Collider>();
            if (myCollider != null)
            {
                Physics.IgnoreCollision(myCollider, other);
            }
                cutCount += 1; 
            audioSource.PlayOneShot(cutAudioClip);
            Vector3 point3 = new Vector3(0, 2 * cutCount, 0);
            Instantiate(cutInto, transform.position, transform.rotation);
            Debug.Log("Cutting speed {0} " + other.GetComponent<Rigidbody>().velocity);
            // cutting many times, destroy after 4 cuts
            if (cutCount == cutLimit){
                audioSource.PlayOneShot(destroyAudioCilp);
                // instantiate a trash object 
                Instantiate(trashPrefab, transform.position, transform.rotation);
                Wait(destroyDelayTime); 
                Debug.Log("Destroying Object");
                gameObject.SetActive(false);
                
            }
        }
    }

    IEnumerator Wait(int seconds)
{
    Debug.Log("Inside wait function " + seconds); 
    yield return new WaitForSeconds(seconds);
}


}
