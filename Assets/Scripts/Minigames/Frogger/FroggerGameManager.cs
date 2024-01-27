using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
        [SerializeField] private CanvasGroup youDiedPanel;
        [SerializeField] private CanvasGroup victoryPanel;
        [SerializeField] private BoolVariable isFroggerGameCompleted;
        
        private List<KeyCode> keys = new List<KeyCode>() { KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V };
        private KeyCode lastKey;
        private List<TreeTrunk> trunks = new List<TreeTrunk>();

        private Vector3 currentPos;
        private int nextIndex = 0;
        private bool isJumping = false;
        private bool isFalling = false;

        private bool isTimerStarted = false;
        private float timer = 0;
        private float timeBetweenFallingTrunk = 2.8f;
        private int fallIndex = 0;
        
        private void Awake()
        {
            currentPos = startPoint.transform.position;
            character.transform.position = currentPos;
            character.GetComponent<FroggerFrogController>().Idle();
            lastKey = keys[Random.Range(0, keys.Count)];
            lastkeyLabel.text = lastKey.ToString();
            isTimerStarted = false;
            
            for (int i = 0; i < 19; i++)
            {
                TreeTrunk newTrunk = Instantiate(trunkPrefab, trunksRoot.transform.position + Vector3.forward * i * 5f, Quaternion.identity, trunksRoot.transform);
                trunks.Add(newTrunk);
                newTrunk.JumpKey = keys[Random.Range(0, keys.Count)];
            }
        }

        private void Update()
        {
            if (isTimerStarted)
            {
                timer += Time.deltaTime;

                if (timer >= timeBetweenFallingTrunk)
                {
                    timer = 0f;
                    trunks[fallIndex].Fall();

                    if (nextIndex - 1 == fallIndex)
                    {
                        DeathSequence();
                        
                        // isFalling = true;
                        // isTimerStarted = false;
                        // character.transform.parent = null;
                        // character.GetComponent<FroggerFrogController>().Fall();
                        //
                        // Sequence showDeathPanel = DOTween.Sequence();
                        // showDeathPanel.AppendInterval(4f);
                        // showDeathPanel.AppendCallback(() =>
                        // {
                        //     youDiedPanel.gameObject.SetActive(true);
                        // });
                        // showDeathPanel.Append(youDiedPanel.DOFade(1f, 1f));
                        // showDeathPanel.AppendInterval(4f);
                        // showDeathPanel.AppendCallback(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name);});
                    }

                    fallIndex++;
                    timeBetweenFallingTrunk *= 0.9f;
                }
            }

            
            if (nextIndex == trunks.Count && Input.GetKey(lastKey) && !isJumping && !isFalling)
            {
                StartCoroutine(MoveToEndPos(currentPos, endPoint.transform));

                currentPos = endPoint.transform.position;
                isTimerStarted = false;

                Sequence showVictoryPanel = DOTween.Sequence();
                showVictoryPanel.AppendInterval(2f);
                showVictoryPanel.Append(victoryPanel.DOFade(1f, 1f));
                showVictoryPanel.AppendInterval(4f);
                showVictoryPanel.AppendCallback(() => { isFroggerGameCompleted.Value = true; });
            }
            
            if (nextIndex < trunks.Count && Input.GetKey(trunks[nextIndex].JumpKey) && !isJumping && !isFalling)
            {
                StartCoroutine(MoveToEndPos(currentPos, trunks[nextIndex].JumpPosition));
                
                currentPos = trunks[nextIndex].JumpPosition.position;
                nextIndex++;
                isTimerStarted = true;
            }
            
            
            // Check for wrong keys
            if (nextIndex == trunks.Count && AnyKeyExcept(lastKey) && !isJumping && !isFalling)
            {
                trunks[nextIndex-1].Fall();
                DeathSequence();
            }
            
            if (nextIndex < trunks.Count && AnyKeyExcept(trunks[nextIndex].JumpKey) && !isJumping && !isFalling)
            {
                trunks[nextIndex-1].Fall();
                DeathSequence();
            }
            
        }

        private void DeathSequence()
        {
            isFalling = true;
            isTimerStarted = false;
            character.transform.parent = null;
            character.GetComponent<FroggerFrogController>().Fall();

            Sequence showDeathPanel = DOTween.Sequence();
            showDeathPanel.AppendInterval(4f);
            showDeathPanel.AppendCallback(() =>
            {
                youDiedPanel.gameObject.SetActive(true);
            });
            showDeathPanel.Append(youDiedPanel.DOFade(1f, 1f));
            showDeathPanel.AppendInterval(4f);
            showDeathPanel.AppendCallback(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name);});
        }

        private bool AnyKeyExcept(KeyCode keyCode)
        {
            foreach (var k in keys)
            {
                if (k == keyCode)
                    continue;
                
                if (Input.GetKey(k))
                    return true;
            }

            return false;
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

