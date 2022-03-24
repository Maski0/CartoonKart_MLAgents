using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [Tooltip("No Need to Assign, objects are automatically pushed in list")]
    public List<Checkpoint> checkPoints;
    
    private void Awake()
    {
        checkPoints = new List<Checkpoint>(GetComponentsInChildren<Checkpoint>());
    }
}
