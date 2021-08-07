using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceberg : Spell {
    private const float MAX_DISTANCE = 2.5f;
    private const int BLOCKS_NUM = 4;
    private const float BLOCK_SIZE = 2.0f;
    public Spell icebergBlockPrefab;
    void Awake() {
        transform.position = getLocation();
        transform.rotation = getRotation();
        float blockStart = -((BLOCKS_NUM - 1) / 2.0f) * BLOCK_SIZE;
        for (int i = 0; i < BLOCKS_NUM; i++) {
            Vector3 newBlockLocation = new Vector3(blockStart + (i * BLOCK_SIZE), 0.0f, 0.0f);
            Spell newBlock = Instantiate(icebergBlockPrefab, newBlockLocation, Quaternion.identity);
            newBlock.transform.SetParent(transform, false);
        }
    }

    private Vector3 getLocation() {
        float xDistToCast = location.x - caster.transform.position.x; 
        float yDistToCast = location.y - caster.transform.position.y; 
        float distToCast = Mathf.Sqrt(Mathf.Pow(xDistToCast, 2) + Mathf.Pow(yDistToCast, 2));
        float distanceRatio = MAX_DISTANCE / distToCast;
        float xDist = xDistToCast * distanceRatio;
        float yDist = yDistToCast * distanceRatio;
        float x = xDist + caster.transform.position.x; 
        float y = yDist + caster.transform.position.y;
        return new Vector3(x, y, 0.0f);
    }

    private Quaternion getRotation() {
        float rotationRad = Mathf.Atan2(location.y - caster.transform.position.y, location.x - caster.transform.position.x);
        return Quaternion.Euler(0.0f, 0.0f, 90 + (rotationRad * Mathf.Rad2Deg));
    }
}
