using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    public bool reactivatable = true;
    bool activated = false;

    void OnTriggerEnter(Collider other) {
        if (activated == false || reactivatable == true) {
            if (other.tag == "Player") {
                GetComponent<AudioSource>().Play();
                activated = true;
            }
        }
    }
}
