using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public float distance = 1.0f;
    public void doUpdate(Vector3 position, Vector3 heading) {
        float angle = Mathf.Atan2(heading.y - position.y, heading.x - position.x);
        transform.localPosition = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, (angle * Mathf.Rad2Deg) - 90.0f);
    }
}
