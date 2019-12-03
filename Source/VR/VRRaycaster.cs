using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRaycaster : MonoBehaviour {
    bool validHit = false;
    RaycastHit lastHitObject;
    LineRenderer line;

    void Start() {
        line = GetComponent<LineRenderer>();
    }

    void Update() {
        if (Physics.Raycast(transform.position, transform.forward, out lastHitObject)) {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, lastHitObject.point);
            line.enabled = true;
            validHit = true;
        }
        else {
            line.enabled = false;
            validHit = false;
        }
    }

    public bool getValidHit() {
        return validHit;
    }

    public Vector3 getHitPos() {
        if (validHit == true) {
            return lastHitObject.point;
        }
        return Vector3.zero;
    }

    public GameObject getHitObject() {
        if (validHit == true) {
            return lastHitObject.transform.gameObject;
        }
        return null;
    }
}
