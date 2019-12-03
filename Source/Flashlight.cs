using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
    public bool lightOn;

    private void Start() {
        lightOn = false;
        GetComponent<Light>().enabled = false;
    }
    // Toggle the light on and off
    public void toggleLight() {
        // Turn on light
        if (lightOn == false) {
            GetComponent<Light>().enabled = true;
            lightOn = true;
        }
        // Turn off light
        else if (lightOn == true) {
            GetComponent<Light>().enabled = false;
            lightOn = false;
        }
    }
}
