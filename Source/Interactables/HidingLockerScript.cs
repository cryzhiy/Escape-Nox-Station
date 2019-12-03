using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HidingLockerScript : MonoBehaviour {
    
    private GameObject player; // holds a refrence to the player character
    private bool isHiding = false; // holds weather the player is hiding in the locker or not
    public GameObject blackout; // holds refrence to the blackout object attached to the player character ( public needs external insertion of blackout)

    private Quaternion playerRotation; // holds the rotation of the player prior to being placed in the locker
    private NavMeshAgent playerNav; // holds refrence to the player charicters nave mesh agent, used to allow the player to be taken off the navmesh and into the locker
    private Vector3 playerPos; // holds th eplayers position prior to being put in the locker

    bool triggerPress = false; // used when determining getting out of locker
    bool triggerDown = false; // used when determining getting out of locker

    float cool = 0; // cool down for getting out of locker
    float blackCool = 0; // cooldown for the black screen going away
    public float blackOutTime = 2.0f; // time for the the cooldown


    /******************************************************************************************************************
     *                              start fuction gets the player charicter and naveMeshAgent                         *
     ******************************************************************************************************************/
    public void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerNav = player.GetComponent<NavMeshAgent>();
    }


    /******************************************************************************************************************
     *                         the update fuction handles the cooldowns and trigger pulls                             *
     ******************************************************************************************************************/
    public void Update() {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f) {
            triggerDown = false;
            if (triggerPress == false) {
                triggerDown = true;
            }
            triggerPress = true;
        }
        else {
            triggerDown = false;
            triggerPress = false;
        }

        if (cool > 0) {
            cool -= Time.deltaTime;
        }

        if (triggerDown && isHiding && cool <= 0) {
            interact();
        }

        if (blackCool > 0){
            blackCool -= Time.deltaTime;
        }

        if (blackCool < 0){
            blackout.SetActive(false);
        }
    }

    /******************************************************************************************************************
     *             the interact function decideds what happens when the player interacts with the locker              *
     ******************************************************************************************************************/
    public void interact() {
        if (!isHiding) {
            blackout.SetActive(true);
            blackCool = 1.0f;

            Transform newTransform = this.transform;
            playerPos = player.transform.position;
            playerRotation = player.transform.rotation;

            player.transform.position = newTransform.position;
            player.transform.rotation = newTransform.rotation;

            playerNav.enabled = false;

            isHiding = true;

            cool = 1;
        }
        else if (isHiding) {
            blackout.SetActive(true);
            blackCool = 1.0f;

            player.transform.position = playerPos;
            player.transform.rotation = playerRotation;

            playerNav.enabled = true;

            isHiding = false;
        }
    }
}
