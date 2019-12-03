using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    Animator anim;
    public bool doorOpen;
    public bool isLocked;

    private void Start() {
        doorOpen = false;
        anim = GetComponent<Animator>();
    }

    public void interact() {
        if (isLocked) {
            VRInput player = GameObject.FindGameObjectWithTag("Player").GetComponent<VRInput>();

            if (player.hasConsole) {
                isLocked = false;
            }
        }

        // play opening animation
        if (!isLocked) {
            if (doorOpen == false) {
                doorOpen = true;
                anim.SetBool("DoorOpen", true);
            }
            // play closing animation
            else if (doorOpen == true) {
                doorOpen = false;
                anim.SetBool("DoorOpen", false);
            }
        }

    }
}