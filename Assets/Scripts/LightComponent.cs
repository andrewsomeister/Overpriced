using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    Light light; 
    public bool lightState = true;
    public void TurnOn() {
        lightState = true;
        Debug.LogWarningFormat("Turnon() is called lightState is " , this.lightState);
    }
    public void TurnOff() {
        lightState = false;
        Debug.LogWarningFormat("TurnOff() is called lightState is", this.lightState);

    }

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightState == true) {
            light.enabled = true;
            Debug.LogWarningFormat("_______________Light is On_______________");
            
        }
        else {
            light.enabled = false;
            Debug.LogWarningFormat("|||||||||||||||||Light is Off ||||||||||||||||||");
            Debug.LogWarningFormat("|||||||||||||||||I am ||||||||||||||||||, ", light.name);

        }
        Debug.LogWarningFormat("I am ", light.name, " and my light state is ", light.lightState);
    }
}
