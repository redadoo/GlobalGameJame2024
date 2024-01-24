using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Linq;

public class InteractionSystem : MonoBehaviour
{
    public event EventHandler                   OnInteractExit;
    public event EventHandler<GameObject>       OnInteractEnter;

    [SerializeField] private float              radius;
    [SerializeField] private float              distance;
    [SerializeField] private RaycastHit         lastNpc;
    [SerializeField] private bool               havedInteraction;

    public static InteractionSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        havedInteraction = false;
        lastNpc = new RaycastHit();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.up * distance, radius);
    }

    private void Update()
    {

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5, transform.forward, 1f);
        
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("npc") && GameInput.Instance.playerInputAction.Player.Interact.IsPressed())
            {
                havedInteraction = true;
                lastNpc = hit;
                OnInteractEnter?.Invoke(this, hit.collider.gameObject);
                hit.collider.GetComponent<NpcBehaviour>().transform.LookAt(transform.position);
            }
        }

        if (havedInteraction && !hits.Contains(lastNpc))
        {
            hits = null;
            OnInteractExit?.Invoke(this, EventArgs.Empty);
            havedInteraction = false;
        }
    }

    


}
