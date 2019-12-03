using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour {
    public void interact() {
        // Destroy keycard when clicked on 
        Destroy(gameObject);
    }
}
