using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerHp : MonoBehaviour {
    public PlayerStats stats;
    private Text text;
    void Start() {
        text = transform.GetComponent<Text>();
    }
    void Update() {
        text.text = "HP: " + stats.currentHp + " / " + stats.maxHp;
    }
}
