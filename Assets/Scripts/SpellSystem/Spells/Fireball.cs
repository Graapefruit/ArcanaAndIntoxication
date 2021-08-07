using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell, Damageable {
    public Spell fireballExplosionPrefab;
    private const float FIREBALL_SPEED = 2.0f;
    private const float ROTATION_SPEED = 8.0f;
    private const float BASE_DAMAGE = 8.0f;
    private Vector3 directionVector;
    new private Rigidbody2D rigidbody2D;
    private bool alreadyHitCaster;

    void Awake() {
        alreadyHitCaster = false;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }
    void Start() {
        throwIfNotAllNecessaryDataPresent();
        directionVector = (location - transform.position).normalized;
    }

    void FixedUpdate() {
        rigidbody2D.MovePosition(transform.position + directionVector * FIREBALL_SPEED * Time.deltaTime);
    } 

    void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable != null) {
            if (!alreadyHitCaster && damageable == caster as Damageable) {
                alreadyHitCaster = true;
                return;
            } else {
                damageable.dealDamage(BASE_DAMAGE);
            }
        }
        explode();
    }

    public void dealDamage(float damage) {
        explode();
    }

    private void explode() {
        Spell explosion = Instantiate(fireballExplosionPrefab, transform.position, Quaternion.identity);
        explosion.caster = caster;
        explosion.intoxicationLevel = intoxicationLevel;
        explosion.location = location;
        Destroy(gameObject);
    }
}

// https://answers.unity.com/questions/187506/add-90-degrees-to-transformrotation.html
// transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.rotation.EulerAnge);