using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayer : MonoBehaviour {
    public float moveSpeed = 1;
    public float horiLookSpeed = 1;
    public float vertLookSpeed = 1;

    LineRenderer line;
    Camera cam;

    float lineCool = 0;

    public string sceneToLoad;

    public bool hasMultitool;
    public bool hasKeycard;

    void Start() {
        line = GetComponent<LineRenderer>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update() {
        // Movement
        float y = transform.position.y;
        Vector3 delta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed;
        transform.Translate(delta, transform);

        delta = new Vector3(0, Input.GetAxis("Mouse X"), 0) * horiLookSpeed;
        Vector3 rot = transform.rotation.eulerAngles;
        rot += delta;
        transform.rotation = Quaternion.Euler(rot);

        delta = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * vertLookSpeed;
        rot = cam.transform.rotation.eulerAngles;
        rot += delta;
        cam.transform.rotation = Quaternion.Euler(rot);

        // Resets the colour of the controller line
        if (lineCool > 0) {
            lineCool -= Time.deltaTime;
        }

        Ray ray = new Ray(transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);

            if (lineCool <= 0) {
                if (hit.transform.tag == "Floor") {
                    line.material.color = Color.green;
                }
                else if (hit.transform.tag == "Locker" && hit.distance <= 3) {
                    line.material.color = Color.cyan;
                }
                else if (hit.transform.tag == "Door" || hit.transform.tag == "Locker") {
                    line.material.color = Color.blue;
                }
                else {
                    line.material.color = Color.red;
                }
            }

            if (Input.GetMouseButtonDown(0)) {
                if (hit.transform.tag == "Floor") {
                    line.material.color = Color.yellow;
                    lineCool = 0.1f;
                }
                else if (hit.transform.tag == "Console") {
                    line.material.color = Color.yellow;
                    lineCool = 0.1f;

                    hasKeycard = true;
                    hit.transform.parent.GetComponent<Keycard>().interact();
                }
                else if (hit.transform.tag == "MultiTool") {
                    line.material.color = Color.yellow;
                    lineCool = 0.1f;

                    hasMultitool = true;
                    hit.transform.parent.GetComponent<PickupMultiTool>().interact();
                }
                else if (hit.transform.tag == "Elevator") {
                    line.material.color = Color.yellow;
                    lineCool = 0.1f;

                    if (hasMultitool) {
                        if (hasKeycard) {
                            SceneManager.LoadScene(sceneToLoad);
                            //hit.transform.parent.GetComponent<Elevator>().interact();
                        }
                    }
                }
                else if (hit.transform.tag == "Door") {
                    line.material.color = Color.yellow;
                    lineCool = 0.1f;

                    hit.transform.parent.GetComponent<Door>().interact();
                }
                else if (hit.transform.tag == "Locker") {
                    line.material.color = Color.yellow;
                    lineCool = 0.1f;

                    if (hit.distance <= 3) {
                        hit.transform.parent.GetComponent<HidingLockerScript>().interact();
                    }
                }
            }
        }
        else {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    }
}
