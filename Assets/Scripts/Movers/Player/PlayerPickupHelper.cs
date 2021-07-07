using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupHelper : MonoBehaviour {
    private const float MAX_PICKUP_RANGE = 2.5f;
    public PickupReference pickupCandidate;
    public GameEvent newPickupCandidateEvent;
    private List<Pickup> pickupOptions;
    void Awake() {
        pickupOptions = new List<Pickup>();
    }

    public void addPickupOption(Pickup pickup) {
        pickupOptions.Add(pickup);
        chooseNextCollectableCandidate();
    }

    public void removePickupOption(Pickup pickup) {
        pickupOptions.Remove(pickup);
        chooseNextCollectableCandidate();
    }

    public void chooseNextCollectableCandidate() {
        Pickup candidate = null;
        float candidateDistance = MAX_PICKUP_RANGE;
        foreach(Pickup pickup in pickupOptions) {
            float newDistance = (transform.position - pickup.transform.position).magnitude;
            if(newDistance < candidateDistance) {
                candidate = pickup;
                candidateDistance = newDistance;
            }
        }
        
        if (pickupCandidate.value != candidate) {
            pickupCandidate.value = candidate;
            newPickupCandidateEvent.Raise();
        }
    }
}
