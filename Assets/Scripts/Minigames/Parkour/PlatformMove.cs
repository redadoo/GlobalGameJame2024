using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [Range(0, 40)]
    [SerializeField] private float      rangeMove;
    [SerializeField] private Vector3    destinationA;
    [SerializeField] private Vector3    destinationB;
    [SerializeField] private float      distanceToDestination;
    [SerializeField] private bool       isGoing;
    [SerializeField] private bool       isFallingDown = false;
    [SerializeField] private float      speedMove;
    [SerializeField] private float      timer = 3f;

    [System.Serializable]
    public enum Dir
    {
        Horizontal,
        Vertical,
        Fall
    }


    [SerializeField] private Dir direction;

    private void Start()
    {
        isGoing = true;

        if (direction == Dir.Horizontal)
        {
            destinationA = new(transform.position.x + rangeMove, transform.position.y, transform.position.z);
            destinationB = new(transform.position.x - rangeMove, transform.position.y, transform.position.z);
        }
        else if(direction == Dir.Vertical)
        {
            destinationA = new(transform.position.x, transform.position.y, transform.position.z + rangeMove);
            destinationB = new(transform.position.x, transform.position.y, transform.position.z - rangeMove);
        }
    }

    private void Update()
    {
        if(direction == Dir.Fall)
        {

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

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Force);

    }
    private void OnCollisionStay(Collision collision)
    {
        print(collision.gameObject.name);
        collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Force);
    }
}
