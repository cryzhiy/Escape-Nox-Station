using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMonsterTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GetComponentInChildren<Monster>(true).gameObject.SetActive(true);
            foreach (AudioSource audio in GetComponentsInChildren<AudioSource>(true)) {
                audio.gameObject.SetActive(true);
            }
        }
    }
}
