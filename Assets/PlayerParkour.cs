using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;
using static PlatformMove;

public class PlayerParkour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            transform.position = ParkourManager.Instance.spawnPoint; 
        }
    }

}
