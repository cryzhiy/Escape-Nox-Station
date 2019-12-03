using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour { // used to run the menu as well as controle the opening of the menu
    bool gamePaused = false; // holds weather the game has been paused or not

    GameObject oculusControler;

    /******************************************************************************************************************
     *                               start fuction sets the local sace to 0                                           *
     ******************************************************************************************************************/
    void Start() {
        transform.localScale = Vector3.zero;

        oculusControler = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<VRInput>().gameObject;
    }

    /******************************************************************************************************************
     *         update function detects pressing of the back button as wellas pausing and unpauseing the game          *
     ******************************************************************************************************************/
    void Update() {
        if ( (OVRInput.GetDown(OVRInput.Button.Back) || Input.GetKeyDown(KeyCode.Space)) && gamePaused == false) {
            pause();
        }
        else if ( (OVRInput.GetDown(OVRInput.Button.Back) || Input.GetKeyDown(KeyCode.Space)) && gamePaused == true) {
            unpause();
        }
    }

    /******************************************************************************************************************
     *                                 pause pauses the game and places the menu                                      *
     ******************************************************************************************************************/
    void pause() {
        transform.localScale = Vector3.one;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
     
        transform.position = oculusControler.transform.position + oculusControler.transform.forward * 3.5f;
        transform.LookAt(oculusControler.transform.position + oculusControler.transform.forward * 4);

        Time.timeScale = 0;

        gamePaused = true;
    }

    /******************************************************************************************************************
     *                         unpause sets the local scale to 0 as well as unpauses the game                         *
     ******************************************************************************************************************/
    void unpause() {
        transform.localScale = Vector3.zero;

        Time.timeScale = 1;

        gamePaused = false;
    }

    /******************************************************************************************************************
     *                                      resume runs unpause() if game is paused                                   *
     ******************************************************************************************************************/
    public void resume() {
        if (gamePaused == true) {
            unpause();
        }
    }

    /******************************************************************************************************************
     *                          restartLevel runs unpause() as well as loads the level again                          *
     ******************************************************************************************************************/
    public void restartLevel() {
        if (gamePaused == true) {
            unpause();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /******************************************************************************************************************
     *                    restartGame runs unpause() as well as loading the starting scene again                      *
     ******************************************************************************************************************/
    public void restartGame() {
        if (gamePaused == true) {
            unpause();
            SceneManager.LoadScene("SpaceStation");
        }
    }    
}
