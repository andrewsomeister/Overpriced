using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Cut With ")] public GameObject cutWith; // object that can cut this object
    [Header("Cut Into")] public GameObject cutInto; // Prefab Only,slices, dices etc. 
    private int cutCount = 0;
    public AudioClip cutAudioClip; 
    public AudioClip destroyAudioCilp;
    private AudioSource audioSource; // the object that can play the sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other ){
        if (other.gameObject.tag == "knife"){
            cutCount += 1; 
            Debug.LogWarningFormat("knife cutting cuttable ", other.name); 
            audioSource.PlayOneShot(cutAudioClip);
            Instantiate(cutInto, transform.position, transform.rotation);
            Debug.Log("tomato slice supposedly show up ");
            if (cutCount == 4){
                audioSource.PlayOneShot(destroyAudioCilp);
                Destroy(gameObject);
            }
        }
    }


}
