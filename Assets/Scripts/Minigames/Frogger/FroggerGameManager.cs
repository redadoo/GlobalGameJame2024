using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Fireflys
{
    public class FroggerGameManager : MonoBehaviour
    {
        [SerializeField] private TreeTrunk trunkPrefab;
        [SerializeField] private GameObject trunksRoot;
        [SerializeField] private GameObject startPoint;
        [SerializeField] private GameObject endPoint;
        [SerializeField] private GameObject character;
        [SerializeField] private TMP_Text lastkeyLabel;
        
        private List<KeyCode> keys = new List<KeyCode>() { KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V };
        private KeyCode lastKey;
        private List<TreeTrunk> trunks = new List<TreeTrunk>();

        private Vector3 currentPos;
        private int nextIndex = 0;
        private bool isJumping = false;
        
        
        private void Awake()
        {
            currentPos = startPoint.transform.position;
            character.transform.position = currentPos;
            character.GetComponent<FroggerFrogController>().Idle();
            lastKey = keys[Random.Range(0, keys.Count)];
            lastkeyLabel.text = lastKey.ToString();
            
            for (int i = 0; i < 10; i++)
            {
                TreeTrunk newTrunk = Instantiate(trunkPrefab, trunksRoot.transform.position + Vector3.forward * i * 9f, Quaternion.identity, trunksRoot.transform);
                trunks.Add(newTrunk);
                newTrunk.JumpKey = keys[Random.Range(0, keys.Count)];
            }
        }

        private void Update()
        {
            if (nextIndex == trunks.Count && Input.GetKey(lastKey) && !isJumping)
            {
                StartCoroutine(MoveToEndPos(currentPos, endPoint.transform));

                currentPos = endPoint.transform.position;
            }
            
            if (nextIndex < trunks.Count && Input.GetKey(trunks[nextIndex].JumpKey) && !isJumping)
            {
                StartCoroutine(MoveToEndPos(currentPos, trunks[nextIndex].JumpPosition));
                
                currentPos = trunks[nextIndex].JumpPosition.position;
                nextIndex++;
            }
        }
        

        IEnumerator MoveToEndPos(Vector3 startPos, Transform endPos)
        {
            isJumping = true;
            character.transform.parent = null;
            float animationTime = 0;
            while (animationTime < 1f)
            {
                if (startPos.z < (endPos.position.z - startPos.z)/2f)
                {
                    character.GetComponent<FroggerFrogController>().JumpUp();
                }
                else
                {
                    character.GetComponent<FroggerFrogController>().JumpDown();
                }
                animationTime += Time.deltaTime*2f;
                character.transform.position = MathParabola.Parabola(startPos, endPos.position, 2, animationTime);
                yield return null;
            }
            
            character.GetComponent<FroggerFrogController>().Idle();
            character.transform.parent = endPos;
            isJumping = false;
        }
        
        
    }
}

