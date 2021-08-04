using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public float distance = 1.0f;
    public Vector2Reference playerLocation;
    public Vector2Reference lookLocation;
    void Update() {
        float angle = Mathf.Atan2(lookLocation.value.y - playerLocation.value.y, lookLocation.value.x - playerLocation.value.x);
        transform.localPosition = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, (angle * Mathf.Rad2Deg) - 90.0f);
    }
    public void doUpdate(Vector3 position, Vector3 heading) {
    }
}
