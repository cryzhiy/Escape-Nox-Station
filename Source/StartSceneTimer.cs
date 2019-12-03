using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneTimer : MonoBehaviour {
    public float time;

    // Update is called once per frame
    void Update() {
        if (time > 0) {
            time -= Time.deltaTime;
        }
        else {
            SceneManager.LoadScene("Elevator1");
        }
    }
}
