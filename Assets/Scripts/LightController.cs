using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : Controller
{
    [Header("Contolled Items")]
    public LightComponent light;
    public LightSwitch lightSwitch;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarningFormat("Light Controller setting the callback function");
        lightSwitch.on_toggled( // passing the callback function 
            ( switch_state ) => { 
            if ( switch_state ) 
                light.TurnOn(); 
            else 
                light.TurnOff();             
            } );
    }
  
}
