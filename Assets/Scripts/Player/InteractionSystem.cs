using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        CapsuleCollider charContr = GetComponent<CapsuleCollider>();
        Vector3 p1 = transform.position + charContr.center + Vector3.up * -charContr.height * 0.5F;
        Vector3 p2 = p1 + Vector3.up * charContr.height;

        // Cast character controller shape 10 meters forward to see if it is about to hit anything.
        if (Physics.CapsuleCast(p1, p2, charContr.radius, transform.forward, out hit, 10))
        {
            print(hit.distance + "  " + hit.collider.gameObject.name);
        }       
    }
}
