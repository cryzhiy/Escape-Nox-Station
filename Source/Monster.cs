using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour {
    Collider player;
    NavMeshAgent agent;

    Vector3 target;

    Camera cameraVision;

    public List<GameObject> patrolGoals;
    public int currentGoal = 0;

    public float baseSpeed = 3.5f;

    public float alertLevel = 0;
    public float stunTimer = 0;
    public int stunCount = 0;

    /********************************************************************************************************
     * Called once when the scene begins
    ********************************************************************************************************/
    void Start() {
        cameraVision = GetComponentInChildren<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolGoals[0].transform.position);
    }

    /********************************************************************************************************
     * Called once every frame
    ********************************************************************************************************/
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            interact();
        }

        alertLevel = Mathf.Max(0.0f, alertLevel - 0.15f * Time.deltaTime);
        checkVision();

        if (stunTimer <= 0) {
            if (stunTimer <= -0.5f && stunCount > 0) {
                stunCount--;
                stunTimer = 0;
            }
            else {
                stunTimer -= Time.deltaTime;
            }
            agent.isStopped = false;
            act();
        }
        else {
            stunTimer -= Time.deltaTime;
            alertLevel = 5;
            agent.isStopped = true;
        }
    }

    /********************************************************************************************************
     * Handles how the monster acts according to its alert level
    ********************************************************************************************************/
    void act() {
        // No Alert
        if (alertLevel == 0) {
            // Patrol
            agent.speed = baseSpeed;
            agent.isStopped = false;
            if (agent.remainingDistance <= agent.stoppingDistance) {
                currentGoal++;
                if (currentGoal >= patrolGoals.Count) {
                    currentGoal = 0;
                }
                agent.SetDestination(patrolGoals[currentGoal].transform.position);
            }
        }
        // Low Alert
        else if (alertLevel <= 1) {
            // Pause
            agent.isStopped = true;
            // Wait
        }
        // Medium Alert
        else if (alertLevel <= 2) {
            // Slowly move toward player
            agent.isStopped = false;
            agent.SetDestination(target);
            agent.speed = baseSpeed;
        }
        // High Alert
        else {
            // Rush toward player
            agent.SetDestination(target);
            agent.speed = baseSpeed * 2;
        }
    }

    /********************************************************************************************************
     * Called to check to see if the player is visible
    ********************************************************************************************************/
    void checkVision() {
        target = transform.position;

        // Sides
        Vector3 pos = player.transform.position;
        pos.x += player.bounds.extents.x;
        if (isPointVisible(pos)) {
            Debug.DrawLine(transform.position, pos, Color.red);
            alertLevel += 0.1f * Time.deltaTime;

            target = player.transform.position;
        }

        pos = player.transform.position;
        pos.x -= player.bounds.extents.x;
        if (isPointVisible(pos)) {
            Debug.DrawLine(transform.position, pos, Color.red);
            alertLevel += 0.1f * Time.deltaTime;

            target = player.transform.position;
        }

        pos = player.transform.position;
        pos.z += player.bounds.extents.z;
        if (isPointVisible(pos)) {
            Debug.DrawLine(transform.position, pos, Color.red);
            alertLevel += 0.1f * Time.deltaTime;

            target = player.transform.position;
        }

        pos = player.transform.position;
        pos.z -= player.bounds.extents.z;
        if (isPointVisible(pos)) {
            Debug.DrawLine(transform.position, pos, Color.red);
            alertLevel += 0.1f * Time.deltaTime;

            target = player.transform.position;
        }

        // Top
        pos = player.transform.position;
        pos.y += player.bounds.extents.y;
        if (isPointVisible(pos)) {
            Debug.DrawLine(transform.position, pos, Color.red);
            alertLevel += 0.1f * Time.deltaTime;

            target = player.transform.position;
        }

        // Bottom
        pos = player.transform.position;
        pos.y -= player.bounds.extents.y;
        if (isPointVisible(pos)) {
            Debug.DrawLine(transform.position, pos, Color.red);
            alertLevel += 0.1f * Time.deltaTime;

            target = player.transform.position;
        }

        // Centre
        if (isPointVisible(player.transform.position)) {
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
            alertLevel += 0.25f * Time.deltaTime;

            target = player.transform.position;
        }
    }

    /********************************************************************************************************
     * Checks whether a point is inside the monster's frustrum and there is nothing between the point and the camera
    ********************************************************************************************************/
    bool isPointVisible(Vector3 point) {
        Vector3 pos = cameraVision.WorldToViewportPoint(point);
        if (pos.x >= 0 && pos.x <= 1) {
            if (pos.y >= 0 && pos.y <= 1) {
                if (pos.z >= 0) {
                    Ray ray = new Ray(transform.position, point - transform.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) {
                        Debug.DrawLine(transform.position, hit.point, Color.blue);
                        Debug.Log(hit.transform.name);
                        if (hit.transform.tag == "Player") {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    /********************************************************************************************************
     * Stuns the monster, stopping it from acting
     * Stunning provides diminishing returns
    ********************************************************************************************************/
    public void interact() {
        stunCount++;
        stunTimer = Mathf.Max(1 / (stunCount * 0.75f), stunTimer + 1 / (stunCount * 0.75f));
    }

    /********************************************************************************************************
     * When the trigger is triggered, if the other is the player rest the scene
    ********************************************************************************************************/
    void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
        if (other.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
