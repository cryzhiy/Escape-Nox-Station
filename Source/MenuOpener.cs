using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour{

    public GameObject menuUI;
    private bool isMenuOpen = false;

    // Start is called before the first frame update
    void Start(){

        menuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if ((OVRInput.GetDown(OVRInput.Button.Back) || Input.GetKeyDown(KeyCode.Space) ) && !isMenuOpen){

            //Vector3 newPosition = transform.position + Quaternion.AngleAxis(angle, Vector3.up) * transform.right * 5;
             Transform camera = GetComponentInChildren<OVRCameraRig>().leftEyeAnchor.transform;
            Vector3 newPosition = transform.position + transform.right * 5;

            menuUI.transform.position = newPosition;
            menuUI.transform.LookAt(transform.position + transform.right * 6);

            menuUI.SetActive(true);
            isMenuOpen = true;

            Time.timeScale = 0;
        }

        else if ((OVRInput.GetDown(OVRInput.Button.Back) || Input.GetKeyDown(KeyCode.Space)) && isMenuOpen){

            menuUI.SetActive(false);
            isMenuOpen = false;

            Time.timeScale = 1;
        }
    }
}