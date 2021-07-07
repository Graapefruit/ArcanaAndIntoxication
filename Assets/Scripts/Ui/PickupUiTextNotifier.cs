using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupUiTextNotifier : MonoBehaviour
{
    // Start is called before the first frame update
    public PickupReference collectable;
    public Text text;
    void Awake() {
        updateDisplayText();
    }

    public void updateDisplayText() {
        if (collectable.value == null) {
            text.text = "";
        } else {
            text.text = "Press [F] to pickup: " + collectable.value.collectable.name;
        }
    }
}
