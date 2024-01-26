using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Linq;

public class InteractionSystem : MonoBehaviour
{
    public event EventHandler                   OnInteractExit;
    public event EventHandler                   OnIneractRange;
    public event EventHandler<GameObject>       OnInteractEnter;
    public event EventHandler<GameObject>       OnMemoryMiniGameStart;

    [SerializeField] private float              radius;
    [SerializeField] private float              distance;
    [SerializeField] private RaycastHit         lastNpc;
    [SerializeField] private bool               havedInteraction;

    private const string                        NPC_TAG = "npc";
    private const string                            MEMORY_TAG = "memory_minigame";

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

    private void Update()
    {

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5, transform.forward, 1f);
        
        foreach (var hit in hits)
        {
            if (GameInput.Instance.playerInputAction.Player.Interact.IsPressed())
            {
                switch (hit.collider.tag)
                {
                    case NPC_TAG:
                        OnInteractionStart(hit);
                        OnInteractEnter?.Invoke(this, hit.collider.gameObject);
                        break;
                    case MEMORY_TAG:
                        OnInteractionStart(hit);
                        OnMemoryMiniGameStart?.Invoke(this, hit.collider.gameObject);
                        break;
                }
            }

        }

        if (havedInteraction && !hits.Contains(lastNpc))
        {
            hits = null;
            OnInteractExit?.Invoke(this, EventArgs.Empty);
            havedInteraction = false;
        }
    }

    void OnInteractionStart(RaycastHit hit)
    {
        havedInteraction = true;
        lastNpc = hit;
        hit.collider.GetComponent<NpcBehaviour>().transform.LookAt(transform.position);
    }


}
