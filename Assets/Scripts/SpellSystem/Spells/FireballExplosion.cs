using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : Spell
{
    private const float BASE_SIZE = 0.15f;
    private const float EXPANSION_RATE = 10.0f;
    private const float EXPLOSION_DURATION = 0.35f;
    private const float BASE_DAMAGE = 40.0f;
    private const float DAMAGE_DECAY_RATE = 90.0f;
    private const float BASE_FORCE = 1.0f;
    private float timeElapsed;
    // Start is called before the first frame update
    void Awake() {
        timeElapsed = 0.0f;
        transform.localScale = new Vector3(BASE_SIZE, BASE_SIZE, 0.0f);
    }

    // Update is called once per frame
    void Update() {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > EXPLOSION_DURATION) {
            Destroy(gameObject);
        }
        float size = BASE_SIZE + (BASE_SIZE * timeElapsed * EXPANSION_RATE);
        transform.localScale = new Vector3(size, size, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable != null) {
            damageable.dealDamage(BASE_DAMAGE - (timeElapsed * DAMAGE_DECAY_RATE));
        }
    }
}
