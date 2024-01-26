using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Fireflys
{
    public class ZapperController : MonoBehaviour
    {
        [SerializeField] private GameObject aim;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Image flashEffect;
        private void Update()
        {
            HandlePosition();
            HandleShoot();
        }

        private void HandlePosition()
        {
            Vector2 mousePos = Input.mousePosition;
            float xRot = -(mousePos.y - (1080 / 2f)) / 25f - 5f;
            float yRot = (mousePos.x - (1920 / 2f)) / 50f;
            transform.eulerAngles = new Vector3(xRot, yRot, 0f);


            float tvYCoord = (mousePos.y - (1080 / 2f)) / 1080f * 480f*5f;
            float tvXCoord = (mousePos.x - (1920 / 2f)) / 1920f * 640f*5f;

            aim.transform.localPosition = new Vector2(tvXCoord, tvYCoord);
        }

        private void HandleShoot()
        {
            if (Input.GetMouseButtonDown(0))
            {
                aim.GetComponent<AimController>().Shoot();
                audioSource.PlayOneShot(shootSound);
                StartCoroutine(ShowFlash());
            }
        }

        IEnumerator ShowFlash()
        {
            flashEffect.enabled = true;
            yield return null;
            yield return null;
            flashEffect.enabled = false;
        }
        
    }
}

