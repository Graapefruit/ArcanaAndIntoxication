using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int speed = 5;
    new public Camera camera;
    private const float GROUND_LAYER = 1 << 6;
    private const float ONE_OVER_ROOT_TWO = 0.707107f;
    void Start() {
        
    }

    void Update() {
        doCharacterMovement();
        doCharacterRotation();
    }

    private void doCharacterMovement() {
        float xMagnitude = 0.0f;
        float zMagnitude = 0.0f;
        Vector3 location = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            xMagnitude = -1.0f;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            xMagnitude = 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            zMagnitude = -1.0f;
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            zMagnitude = 1.0f;
        }

        if (xMagnitude != 0.0f && zMagnitude != 0.0f) {
            xMagnitude *= ONE_OVER_ROOT_TWO;
            zMagnitude *= ONE_OVER_ROOT_TWO;
        }
        location.x += xMagnitude * speed * Time.deltaTime;
        location.z += zMagnitude * speed * Time.deltaTime;
        transform.position = location;
    }

    private void doCharacterRotation() {
        if (camera == null) {
            return;
        }
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, GROUND_LAYER)) {
            transform.rotation = Quaternion.Euler(0.0f, getAngle(transform.position, hit.point), 0.0f);
        }
    }

    private float getAngle(Vector3 source, Vector3 dest) {
        float x = dest.x - source.x;
        float z = dest.z - source.z;
        float angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
        Debug.Log(angle);
        return angle; 
    }
}
