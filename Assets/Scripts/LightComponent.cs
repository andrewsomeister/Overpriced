using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    Light lightcomp; 
    public bool lightState = true;
    public Color ChangeColor; 
    public void TurnOn() {
        lightState = true;
    }
    public void TurnOff() {
        lightState = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        lightcomp = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightState == true) {
            lightcomp.enabled = true;            
        }
        else {
            lightcomp.enabled = false;
            lightcomp.color = ChangeColor;
        }
    }
}
