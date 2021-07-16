using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldHotbarImage : MonoBehaviour {
    public HoldableReference heldItem;
    public Image image;
    public void updateHeldItem() {
        image.sprite = heldItem.value.displaySprite;
    }
}
