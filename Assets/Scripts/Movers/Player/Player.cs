using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover {
    private const float BASE_SPEED = 6.0f;
    public Rigidbody2D rigidbody2d;
    public SpriteRenderer avatar;
    public PlayerStats stats;
    public Spellbook spellbook;
    public Vector2Reference moveDirection;
    public Vector2Reference lookTarget;
    public Vector2Reference playerLocation;
    public PlayerPickupHelper playerPickupHelper;
    public BooleanReference useItemCommand;
    public GameEvent newPlayerHeldItemEvent;
    public Holdable HeldItem {
        get { return _holdableReference.value; }
        set {
            _holdableReference.value = value;
            newPlayerHeldItemEvent.Raise();
        }
    }
    public HoldableReference _holdableReference;
    private bool isConsuming;
    private float timeConsumeStarted;
    private float timeNeededToConsume;
    private StateManager stateManager;
    private bool alive;

    void Awake() {
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

        State consumingState = new State();
        consumingState.name = "Consuming State";
        consumingState.onEnter = (() => {
            isConsuming = true;
            timeConsumeStarted = Time.time;
            timeNeededToConsume = (_holdableReference.value as Consumable).consumptionTime;
            Debug.Log(timeConsumeStarted);
            Debug.Log(timeNeededToConsume);
        });
        consumingState.onUpdate = (() => {
            tickConsuming();
        });

        defaultState.onGetNextState = (() => {
            if (shouldStartConsuming()) {
                return consumingState;
            } else if (alive) {
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

        consumingState.onGetNextState = (() => {
            if (!alive) {
                return deadState;
            } else if (isConsuming) {
                return consumingState;
            } else {
                return defaultState;
            }
        });

        stateManager = new StateManager("Player", defaultState);
    }

    void Update() {
        stateManager.doUpdate();
        playerLocation.value.x = transform.position.x;
        playerLocation.value.y = transform.position.y;
    }

    void FixedUpdate() {
        stateManager.doFixedUpdate();
    }

    public bool shouldStartConsuming() {
        return (!isConsuming && useItemCommand.value && _holdableReference.value != null && _holdableReference.value is Consumable);
    }

    private bool sufficientMana(SpellInfo spellInfo) {
        return spellInfo.cost <= stats.CurrentIntoxication;
    }

    private void rotateAvatar() {
        avatar.transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        avatar.color = Color.red;
    }

    private void doMovement() {
        if (moveDirection.value != Vector2.zero) {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            pos.x += moveDirection.value.x * BASE_SPEED * Time.deltaTime;
            pos.y += moveDirection.value.y * BASE_SPEED * Time.deltaTime;
            rigidbody2d.MovePosition(pos);
            playerPickupHelper.chooseNextCollectableCandidate();
        }
    }

    public void castSpell(int hotkey) {
        SpellInfo spellInfo = spellbook.getSpellFromHotbar(hotkey);
        if (spellInfo != null) {
            if (spellbook.spellOffCooldown(hotkey) && sufficientMana(spellInfo)) {
                Spell spellPrefab = spellInfo.spellPrefab;
                Spell spellInstance = Instantiate(spellPrefab, transform.position, Quaternion.identity);
                spellInstance.caster = this;
                spellInstance.intoxicationLevel = 0;
                spellInstance.location = lookTarget.value;
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

    private void tickConsuming() {
        if (isDoneConsuming()) {
            isConsuming = false;
            stats.CurrentIntoxication += (_holdableReference.value as Consumable).intoxicationAmount;
        }
    }

    private bool isDoneConsuming() {
        return isConsuming && Time.time - timeConsumeStarted >= timeNeededToConsume;
    }
}
