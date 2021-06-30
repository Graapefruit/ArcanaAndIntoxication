using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIntoxicationUI : MonoBehaviour {
    public PlayerStats stats;
    private Text text;
    void Awake() {
        text = transform.GetComponent<Text>();
        updateDisplay();
    }

    public void updateDisplay() {
        text.text = "Intoxication: " + stats.CurrentIntoxication.ToString("0") + " / " + stats.maxIntoxication.ToString("0");
    }
}
