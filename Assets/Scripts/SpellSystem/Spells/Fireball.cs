using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell {
    private const float fireballSpeed = 8.0f;
    protected override void Update() {
        throwIfNotAllNecessaryDataPresent();
        transform.position = Vector3.MoveTowards(transform.position, location, fireballSpeed * Time.deltaTime);
        if (transform.position == location) {
            Destroy(gameObject);
        }
    }
}
