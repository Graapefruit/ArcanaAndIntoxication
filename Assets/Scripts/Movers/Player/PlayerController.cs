using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    private const float ONE_OVER_ROOT_TWO = 0.707107f;
    public Player player;
    public Pointer pointer;
    new public Camera camera;
    public EventSystem eventSystem;
    public GraphicRaycaster leftPageRaycaster;
    public GraphicRaycaster rightPageRaycaster;
    public GameEvent enableSpellbookUI;
    public GameEvent disableSpellbookUI;
    public GameEvent flipSpellbookLeft;
    public GameEvent flipSpellbookRight;
    private Vector3 heading;
    private StateManager stateManager;

    void Awake() {
        heading = Vector3.zero;

        // ===== STATES =====

        State freeState = new State();
        freeState.name = "Free State";
        freeState.onUpdate = (() => {
            updateHeading();
            updateCharacterMovement();
            doCharacterSpellCasts();
            doPickup();
        });

        State spellbookState = new State();
        spellbookState.name = "Spellbook State";
        spellbookState.onEnter = (() => {
            doEnableSpellbookUI();
        });
        spellbookState.onUpdate = (() => {
            updateHeading();
            updateCharacterMovement();
            doSpellbookPageFlipping();
            doHotbarAssignments();
        });
        spellbookState.onExit = (() => {
            doDisableSpellbookUI();
        });

        // ===== STATE TRANSITIONS =====

        freeState.onGetNextState = (() => {
            if (Input.GetKeyDown(KeyCode.Q)) {
                return spellbookState;
            } else {
                return freeState;
            }
        });

        spellbookState.onGetNextState = (() => {
            if (Input.GetKeyDown(KeyCode.Q)) {
                return freeState;
            } else {
                return spellbookState;
            }
        });

        stateManager = new StateManager("Player Controller", freeState);
    }

    void Update() {
        stateManager.doUpdate();
    }

    private void updateCharacterMovement() {
        float x = 0.0f;
        float y = 0.0f;
        if (Input.GetKey(KeyCode.A)) {
            x = -1.0f;
        } else if (Input.GetKey(KeyCode.D)) {
            x = 1.0f;
        }
        if (Input.GetKey(KeyCode.S)) {
            y = -1.0f;
        } else if (Input.GetKey(KeyCode.W)) {
            y = 1.0f;
        }

        if (x != 0.0f && y != 0.0f) {
            x *= ONE_OVER_ROOT_TWO;
            y *= ONE_OVER_ROOT_TWO;
        }
        player.movementInput.x = x;
        player.movementInput.y = y;
    }

    private void doPickup() {
        if (Input.GetKeyDown(KeyCode.F)) {
            player.pickupThePickupCandidate();
        }
    }

    private void doCharacterSpellCasts() {
        if (Input.GetKey(KeyCode.Alpha1)) {
            player.castSpell(1, heading);
        } else if (Input.GetKey(KeyCode.Alpha2)) {
            player.castSpell(2, heading);
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            player.castSpell(3, heading);
        } else if (Input.GetKey(KeyCode.Alpha4)) {
            player.castSpell(4, heading);
        }
    }

    private void doSpellbookPageFlipping() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            flipSpellbookLeft.Raise();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            flipSpellbookRight.Raise();
        }
    }

    private void doHotbarAssignments() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            doHotbarAssignment(1, leftPageRaycaster);
            doHotbarAssignment(1, rightPageRaycaster);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            doHotbarAssignment(2, leftPageRaycaster);
            doHotbarAssignment(2, rightPageRaycaster);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            doHotbarAssignment(3, leftPageRaycaster);
            doHotbarAssignment(3, rightPageRaycaster);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            doHotbarAssignment(4, leftPageRaycaster);
            doHotbarAssignment(4, rightPageRaycaster);
        }
    }

    private void doHotbarAssignment(int hotbarKey, GraphicRaycaster graphicRaycaster) {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);
        foreach (RaycastResult result in results) {
            GameObject spellbookPage = result.gameObject;
            if (spellbookPage.tag == "SpellbookPageUI") {
                SpellbookPageUI page = spellbookPage.GetComponent<SpellbookPageUI>();
                page.spellbook.setNewHotbarMapping(hotbarKey, page.SpellIndex);
            }
        }
    }

    private void doEnableSpellbookUI() {
        enableSpellbookUI.Raise();
    }

    private void doDisableSpellbookUI() {
        disableSpellbookUI.Raise();
    }

    private void updateHeading() {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        heading = ray.origin;
        heading.z = 0.0f;
        pointer.doUpdate(transform.position, heading);
    }
}
