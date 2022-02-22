using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Space(20)] 
    public Transform spawner;
    
    [Space(0)]
    public Transform spawnParent;

 
    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }else{
            Instance = this;
        }
    }

    private void SpawnItem(ItemData data)
    {
        Instantiate(data.itemPrefab,spawner.position,quaternion.identity,spawnParent);
    }
}
