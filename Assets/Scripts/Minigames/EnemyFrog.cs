using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EnemyFrog : MonoBehaviour
{
    [SerializeField] private AudioSource adSource;
    private void Start()
    {
        MemoryGameManager.Instance.OnEnemyTurn += MemoryGameManager_OnEnemyTurn;
        MemoryGameManager.Instance.OnDeath += OnDeath; ;
    }

    private void OnDeath(object sender, System.EventArgs e)
    {
        StopCoroutine(playAudioSequentially());
        MemoryGameManager.Instance.ChangeMemoryState(MemoryGameManager.MemoryGameState.PlayerTurn);
    }

    private void MemoryGameManager_OnEnemyTurn(object sender, System.EventArgs e)
    {
        StartCoroutine(playAudioSequentially());
    }

    IEnumerator playAudioSequentially()
    {
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < MemoryGameManager.Instance.GetAudioClipMinigames().Count; i++)
        {
            //2.Assign current AudioClip to audiosource
            adSource.clip = MemoryGameManager.Instance.GetAudioClipMinigames()[i];

            //3.Play Audio
            yield return new WaitForSeconds(1);
            adSource.Play();
            UiManager.Instance.AnimBar(true, MemoryGameManager.Instance.GetClipPos(adSource.clip));
            //4.Wait for it to finish playing
            while (adSource.isPlaying)
            {
                yield return null;
            }
            UiManager.Instance.AnimBar(false, MemoryGameManager.Instance.GetClipPos(adSource.clip));

            //5. Go back to #2 and play the next audio in the adClips array
        }
        MemoryGameManager.Instance.isEnemyPlaying = false;
        MemoryGameManager.Instance.ChangeMemoryState(MemoryGameManager.MemoryGameState.PlayerTurn);
    }

}
