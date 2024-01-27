using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Fireflys
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private List<BoolVariable> conditions;
        [SerializeField] private List<NpcAction> actionSequence;
        [SerializeField] private List<NpcAction> actionSequenceIfTrue;
        [SerializeField] private State currentState = State.Idle;
        [SerializeField] private Animator npcAnimator;
        [SerializeField] private CinemachineVirtualCamera npcCamera;
        
        enum State { Idle, Interactable, Active}

        private int currentAction = 0;


        private void Awake()
        {
            npcCamera.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                currentState = State.Interactable;
                
                MainUIController.Instance.ShowInteractionUI(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                currentState = State.Idle;
                npcAnimator.SetInteger("State", 0);
                MainUIController.Instance.ShowInteractionUI(false);
                MainUIController.Instance.ShowDialogueUI(false);
                FindObjectOfType(typeof(ThirdPersonCam)).GetComponent<ThirdPersonCam>().EnableThirdPersonCamera(true);
                npcCamera.enabled = false;
                currentAction = 0;
            }
        }

        private void Update()
        {
            if (currentState == State.Interactable && Input.GetKeyDown(KeyCode.E))
            {
                currentState = State.Active;
                MainUIController.Instance.ShowInteractionUI(false);
                FindObjectOfType(typeof(ThirdPersonCam)).GetComponent<ThirdPersonCam>().EnableThirdPersonCamera(false);
                npcCamera.enabled = true;
                PlayCurrentAction();
                npcAnimator.SetInteger("State", actionSequence[currentAction].animatorStateValue);
                npcAnimator.SetTrigger("TriggerTalk");
                return;
            }

            if (currentState == State.Active && Input.GetKeyDown(KeyCode.E))
            {
                currentAction++;
                if (currentAction >= actionSequence.Count)
                {
                    currentState = State.Idle;
                    
                    MainUIController.Instance.ShowInteractionUI(true);
                    MainUIController.Instance.ShowDialogueUI(false);
                    FindObjectOfType(typeof(ThirdPersonCam)).GetComponent<ThirdPersonCam>().EnableThirdPersonCamera(true);
                    npcCamera.enabled = false;
                    return;
                }
                PlayCurrentAction();
                npcAnimator.SetInteger("State", actionSequence[currentAction].animatorStateValue);
                npcAnimator.SetTrigger("TriggerTalk");
            }
            
        }

        private void PlayCurrentAction()
        {
            actionSequence[currentAction].Execute();
        }
    }
    
    
}

