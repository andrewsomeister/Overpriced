using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSwitch : MonoBehaviour
{
    public AudioClip clip; // the sound to play when hit 
    private AudioSource source;
    public string targetTag; // the thing needed to switch this on 

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        // if (other.CompareTag(targetTag)) {
            Debug.Log("PlayAudioSwitch: targetTag ", other.gameObject);
            source.PlayOneShot(clip);
        // }
    }
   
}
