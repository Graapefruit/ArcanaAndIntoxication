using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Collectable collectable;
    private List<Player> pickupCandiadates;
    void Awake() {
        pickupCandiadates = new List<Player>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        Player player = other.gameObject.transform.GetComponent<Player>();
        if (player != null) {
            player.playerPickupHelper.addPickupOption(this);
            pickupCandiadates.Add(player);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Player player = other.gameObject.transform.GetComponent<Player>();
        if (player != null) {
            player.playerPickupHelper.removePickupOption(this);
            pickupCandiadates.Remove(player);
        }
    }

    // We cannot return and Destroy in the same function, so we must access the player directly
    public void pickup(Player player) {
        if (player == null) {
            Debug.LogWarning("Pickup.pickup invoked without specifying a player!");
            return;
        }
        if (collectable is SpellbookPage) {
            SpellbookPage spellbookPage = collectable as SpellbookPage;
            player.spellbook.addNewSpell(spellbookPage.spellInfo);
        } else if (collectable is Holdable) {
            Holdable holdable = collectable as Holdable;
            player.HeldItem = holdable;
        }
        foreach(Player nowIneligiblePlayer in pickupCandiadates) {
            nowIneligiblePlayer.playerPickupHelper.removePickupOption(this);
        }
        Destroy(gameObject);
    }
}
