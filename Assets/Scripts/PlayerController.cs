using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    public int speed = 5;
    new public Camera camera;
    public EventSystem eventSystem;
    public GraphicRaycaster leftPageRaycaster;
    public GraphicRaycaster rightPageRaycaster;
    public GameEvent enableSpellbookUI;
    public GameEvent disableSpellbookUI;
    public GameEvent flipSpellbookLeft;
    public GameEvent flipSpellbookRight;
    private const float ONE_OVER_ROOT_TWO = 0.707107f;
    private Player player;
    private StateManager stateManager;
    private Vector3 heading;
    private Pointer pointer;
    void Start() {
        heading = Vector3.zero;
        pointer = transform.Find("Pointer").GetComponent<Pointer>();
        player = transform.GetComponent<Player>();
        State freeState = new State(
            "Free State",
            (() => {}),
            (() => {
                updateHeading();
                doCharacterMovement();
                doCharacterSpellCasts();
            }),
            (() => {})
        );

        State spellbookState = new State(
            "Spellbook State",
            (() => {
                doEnableSpellbookUI();
            }),
            (() => {
                updateHeading();
                doCharacterMovement();
                doSpellbookPageFlipping();
                doHotbarAssignments();
            }),
            (() => {
                doDisableSpellbookUI();
            })
        );

        freeState.setOnGetNextState(() => {
            if (Input.GetKeyDown(KeyCode.Q)) {
                return spellbookState;
            } else {
                return freeState;
            }
        });

        spellbookState.setOnGetNextState(() => {
            if (Input.GetKeyDown(KeyCode.Q)) {
                return freeState;
            } else {
                return spellbookState;
            }
        });

        stateManager = new StateManager(freeState);
    }

    void Update() {
        stateManager.doUpdate();
    }

    private void doCharacterMovement() {
        float xMagnitude = 0.0f;
        float yMagnitude = 0.0f;
        Vector3 location = transform.position;
        if (Input.GetKey(KeyCode.A)) {
            xMagnitude = -1.0f;
        } else if (Input.GetKey(KeyCode.D)) {
            xMagnitude = 1.0f;
        }
        if (Input.GetKey(KeyCode.S)) {
            yMagnitude = -1.0f;
        } else if (Input.GetKey(KeyCode.W)) {
            yMagnitude = 1.0f;
        }

        if (xMagnitude != 0.0f && yMagnitude != 0.0f) {
            xMagnitude *= ONE_OVER_ROOT_TWO;
            yMagnitude *= ONE_OVER_ROOT_TWO;
        }
        location.x += xMagnitude * speed * Time.deltaTime;
        location.y += yMagnitude * speed * Time.deltaTime;
        transform.position = location;
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
