using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {
    public string loadElevator;

    public AudioClip hasCard;
    public AudioClip noCard;

    public AudioSource CardSource;

    /******************************************************************************************************
    * If the player clicks on elevator and has the required tools then player goes to the next scene
    *******************************************************************************************************/
    public void interact() {
        SceneManager.LoadSceneAsync(loadElevator);
    }

    // Plays audio for working elevator if has needed items
    public void workingElevatorClip() {
        // get audio source
        CardSource.clip = hasCard;
        // play audio source
        CardSource.Play();
    }

    // Plays audio for elevator if missing items
    public void brokenElevatorClip() {
        // get audio source
        CardSource.clip = noCard;
        // play audio source
        CardSource.Play();
    }
}
