using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {
    public List<Item> items;
    
    public bool add(Item item) {
        return true;
    }
}