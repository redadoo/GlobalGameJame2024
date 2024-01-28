using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [Range(0, 40)]
    [SerializeField] private float rangeMove;
    [SerializeField] private Vector3 destinationA;
    [SerializeField] private Vector3 destinationB;
    [SerializeField] private float distanceToDestination;
    [SerializeField] private bool   isGoing;
    [SerializeField] private bool   isFallingDown = false;
    [SerializeField] private float speedMove = .5f;
    [SerializeField] private float timer = 3f;

    [System.Serializable]
    public enum Dir
    {
        Horizontal,
        Vertical,
        Fall,
        None
    }


    [SerializeField] private Dir direction;

    private void Start()
    {
        if (direction == Dir.None) return ;
        isGoing = true;

        if (direction == Dir.Horizontal)
        {
            destinationA = new(transform.position.x + rangeMove, transform.position.y, transform.position.z);
            destinationB = new(transform.position.x - rangeMove, transform.position.y, transform.position.z);
        }
        else if (direction == Dir.Vertical)
        {
            destinationA = new(transform.position.x, transform.position.y, transform.position.z + rangeMove);
            destinationB = new(transform.position.x, transform.position.y, transform.position.z - rangeMove);
        }
    }

    private void Update()
    {
        if (direction == Dir.Fall)
        {
            if (timer == 0)
            {
                
            }
            timer -= Time.deltaTime;
        }

        if (isGoing && direction != Dir.Fall)
        {
            if (Vector3.Distance(transform.position, destinationA) > distanceToDestination)
                transform.position = Vector3.Lerp(transform.position, destinationA, Time.deltaTime * speedMove);
            else
                isGoing = !isGoing;
        }
        if (!isGoing && direction != Dir.Fall)
        {
            if (Vector3.Distance(transform.position, destinationB) > distanceToDestination)
                transform.position = Vector3.Lerp(transform.position, destinationB, Time.deltaTime * speedMove);
            else
                isGoing = !isGoing;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(direction != Dir.None)
            speedMove = 0.2f;
    }

    private void OnTriggerStay(Collider other)
    {

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (direction != Dir.None)
            speedMove = 0.5f;
    }
}
