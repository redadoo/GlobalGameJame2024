using System;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class StartPositionController : MonoBehaviour
{
    [SerializeField] private Vector3Variable position;
    [SerializeField] private Vector3Variable rotation;
    [SerializeField] private GameObject player;

    public static StartPositionController Instance;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        player.transform.position = position.Value;
        player.transform.eulerAngles = rotation.Value;
    }


    public void SaveCurrentPosition()
    {
        position.Value = player.transform.position;
        rotation.Value = player.transform.eulerAngles;
    }



}
