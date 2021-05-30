using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        Player player = other.gameObject.transform.GetComponent<Player>();
        if (player.inventory.add(item)) {
            Destroy(gameObject);
        }
    }
}
