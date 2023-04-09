using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Switch {
 
 // Define new event type (i.e. we want the function to be called to expect one argument with the boolean type)
	public delegate void SwitchEvent ( bool is_turned_on );
    
    // a bool that keeps the state of the switch
    protected SwitchEvent on_toggled_callback_with_arg; 
    
    public void on_toggled(SwitchEvent callback) { 
        Debug.LogWarningFormat("Light Switch Callback Set ");
        on_toggled_callback_with_arg = callback; 
    }

    public bool turnedOn = true;
    public override void self_toggled_by ( MainPlayerController player ) {

        Debug.LogWarningFormat("Light Switch is Called ");

        base.self_toggled_by( player );
        turnedOn = !turnedOn;
        
        // calling the callback function
        if ( on_toggled_callback_with_arg != null ) on_toggled_callback_with_arg( turnedOn );
    }
}

