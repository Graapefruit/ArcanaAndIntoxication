using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover {
    private const float BASE_SPEED = 6.0f;
    public Rigidbody2D rigidbody2d;
    public SpriteRenderer avatar;
    public PlayerStats stats;
    public Spellbook spellbook;
    public Vector2 movementInput;
    public PlayerPickupHelper playerPickupHelper;
    public GameEvent newPlayerHeldItemEvent;
    public Holdable HeldItem {
        get { return _holdableReference.value; }
        set { 
            _holdableReference.value = value;
            newPlayerHeldItemEvent.Raise();
        }
    }
    public HoldableReference _holdableReference;
    private StateManager stateManager;
    private bool alive;

    void Awake() {
        movementInput = Vector2.zero;
        alive = true;

        State defaultState = new State();
        defaultState.name = "Default State";
        defaultState.onFixedUpdate = (() => {
            doMovement();
        });

        State deadState = new State();
        deadState.name = "Dead State";
        deadState.onEnter = (() => {
            rotateAvatar();
        });
        deadState.onFixedUpdate = (() => {});

        State consumingState = new State();
        consumingState.name = "Consuming State";

        defaultState.onGetNextState = (() => {
            if (alive) {
                return defaultState;
            } else {
                return deadState;
            }
        });

        deadState.onGetNextState = (() => {
            if (alive) {
                return defaultState;
            } else {
                return deadState;
            }
        });

        stateManager = new StateManager("Player", defaultState);
    }

    void FixedUpdate() {
        stateManager.doFixedUpdate();
    }

    private bool sufficientMana(SpellInfo spellInfo) {
        return spellInfo.cost <= stats.CurrentIntoxication;
    }

    private void rotateAvatar() {
        avatar.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        avatar.color = Color.red;
    }

    private void doMovement() {
        if (movementInput != Vector2.zero) {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            pos.x += movementInput.x * BASE_SPEED * Time.deltaTime;
            pos.y += movementInput.y * BASE_SPEED * Time.deltaTime;
            rigidbody2d.MovePosition(pos);
            playerPickupHelper.chooseNextCollectableCandidate();
        }
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

    public void pickupThePickupCandidate() {
        Pickup pickup = playerPickupHelper.pickupCandidate.value;
        if (pickup != null) {
            pickup.pickup(this);
        }
    }

    public void killPlayer() {
        alive = false;
    }

    public override void dealDamage(float damage) {
        stats.CurrentHp -= damage;
    }
}
