using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float timeDelta;

    void Update()
    {
        timeDelta += Time.deltaTime;
        timeDelta /= 2;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, 40, 300, 40), "ms: " + timeDelta.ToString());
        GUI.Label(new Rect(100, 80, 300, 40), "fps:" + (1 / timeDelta).ToString());

    }
}
