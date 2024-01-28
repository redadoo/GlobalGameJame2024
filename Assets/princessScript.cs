using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princessScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem test;
    [SerializeField] private GameObject humanPrincess;
    [SerializeField] private GameObject frogPrincess;


    private void OnCollisionEnter(Collision collision)
    {
        if (humanPrincess.activeSelf)
        {
            test.Play();
            humanPrincess.SetActive(false);
            frogPrincess.SetActive(true);
        }        
    }


}
