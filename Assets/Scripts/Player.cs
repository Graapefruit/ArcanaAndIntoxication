using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const float BASE_SPEED = 6.0f;
    public PlayerStats stats;
    public Inventory inventory;
    public Spellbook spellbook;
    private StateManager stateManager;
    public Vector2 movementInput;
    private Rigidbody2D rigidbody2d;

    void Awake() {
        movementInput = Vector2.zero;
        rigidbody2d = transform.GetComponent<Rigidbody2D>();

        State defaultState = new State();
        defaultState.name = "Default State";
        defaultState.onFixedUpdate = (() => {
            doMovement();
        });

        defaultState.onGetNextState = (() => {
            return defaultState;
        });

        stateManager = new StateManager("Player", defaultState);
    }

    void FixedUpdate() {
        stateManager.doFixedUpdate();
    }

    public void castSpell(int hotkey, Vector3 castTarget) {
        SpellInfo spellInfo = spellbook.getSpellFromHotbar(hotkey);
        if (spellInfo != null) {
            if (spellbook.spellOffCooldown(hotkey) && sufficientMana(spellInfo)) {
                Spell spellPrefab = spellInfo.spellPrefab;
                Spell spellInstance = Instantiate(spellPrefab, transform.position, Quaternion.identity);
                spellInstance.caster = this;
                spellInstance.intoxicationLevel = 0;
                spellInstance.location = castTarget;
                stats.CurrentIntoxication -= spellInfo.cost;
                spellbook.putSpellOnCooldown(hotkey);
            }
        }
    }

    public void dealDamage(float damage) {
        stats.currentHp -= damage;
    }

    private bool sufficientMana(SpellInfo spellInfo) {
        return spellInfo.cost <= stats.CurrentIntoxication;
    }

    private void doMovement() {
        if (movementInput != Vector2.zero) {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            pos.x += movementInput.x * BASE_SPEED * Time.deltaTime;
            pos.y += movementInput.y * BASE_SPEED * Time.deltaTime;
            rigidbody2d.MovePosition(pos);
        }
    }
}
