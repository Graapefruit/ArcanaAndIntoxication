using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebergBlock : Spell, Damageable {
    private const float MAX_HP = 100.0f;
    private float hp;
    void Awake() {
        hp = MAX_HP;
    }
    public void dealDamage(float damage) {
        hp -= damage;
        if (hp <= 0.0f) {
            Destroy(this.gameObject);
        }
    }
}
