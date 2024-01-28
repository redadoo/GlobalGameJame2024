using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourManager : MonoBehaviour
{
    public Vector3 spawnPoint;

    public static ParkourManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

}
