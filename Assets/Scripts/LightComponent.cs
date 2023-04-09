using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightComponent : MonoBehaviour
{
    Light lightcomp; 
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
        lightcomp = GetComponent<Light>();
        Debug.LogWarningFormat("Light is ", lightcomp.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (lightState == true) {
            lightcomp.enabled = true;
            Debug.LogWarningFormat("_______________Light is On_______________");
            
        }
        else {
            lightcomp.enabled = false;
            lightcomp.color = Color.blue;
            Debug.LogWarningFormat("|||||||||||||||||Light is Off ||||||||||||||||||");
            Debug.LogWarningFormat("|||||||||||||||||I am ||||||||||||||||||, ", lightcomp.name);

        }
    }
}
