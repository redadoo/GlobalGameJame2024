using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Linq;

public class InteractionSystem : MonoBehaviour
{
    public event EventHandler                   OnInteractExit;
    public event EventHandler                   OnInteractRange;
    public event EventHandler<GameObject>       OnInteractEnter;
    public event EventHandler<GameObject>       OnMemoryMiniGameStart;
    public event EventHandler<GameObject>       OnFroggerMiniGameStart;
    public event EventHandler<GameObject>       OnFlyHuntMiniGameStart;

    [SerializeField] private float              radius;
    [SerializeField] private float              distance;
    [SerializeField] private RaycastHit         lastNpc;
    [SerializeField] private bool               havedInteraction;

    private const string                        NPC_TAG = "npc_interactable";
    private const string                        MEMORY_TAG = "memory_minigame_interactable";
    private const string                        FROGGER_TAG = "frogger_minigame_interactable";
    private const string                        FLYHUNT_TAG = "flyhunt_minigame_interactable";

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
        
        foreach (RaycastHit hit in hits)
        {
            if (IsInteractableTag(hit.collider.tag))
            {
                OnInteractionStart(hit);
                OnInteractRange?.Invoke(this, EventArgs.Empty);
            }
            if (GameInput.Instance.playerInputAction.Player.Interact.WasPressedThisFrame())
            {
                switch (hit.collider.tag)
                {
                    case NPC_TAG:
                        OnInteractEnter?.Invoke(this, hit.collider.gameObject);
                        break;
                    case MEMORY_TAG:
                        OnMemoryMiniGameStart?.Invoke(this, hit.collider.gameObject);
                        break;
                    case FROGGER_TAG:
                        OnFroggerMiniGameStart?.Invoke(this, hit.collider.gameObject);
                        break;
                    case FLYHUNT_TAG:
                        OnFlyHuntMiniGameStart?.Invoke(this, hit.collider.gameObject);
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
        lastNpc = hit;
        havedInteraction = true;
        hit.collider.GetComponent<NpcBehaviour>().transform.LookAt(transform.position);
    }

    bool IsInteractableTag(string tag)
    {
        List<string> tags = new List<string>();
        tags = UnityEditorInternal.InternalEditorUtility.tags.ToList();
        tags = Enumerable.Reverse(tags).Take(4).Reverse().ToList();


        return tags.Contains(tag);
    }

}
