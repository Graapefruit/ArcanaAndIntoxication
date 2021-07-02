using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerHp : MonoBehaviour {
    public PlayerStats stats;
    private Text text;
    void Start() {
        text = transform.GetComponent<Text>();
        updateDisplay();
    }
    public void updateDisplay() {
        text.text = "HP: " + stats.CurrentHp.ToString("0") + " / " + stats.maxHp.ToString("0");
    }
}
