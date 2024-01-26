using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;


namespace Fireflys
{
    public class AimController : MonoBehaviour
    {
        // Inspector
        [SerializeField] private DoubleVariable score;
        [SerializeField] private SpriteRenderer aimSprite;
        
        // Fields
        private bool isOnAFly = false;
        private bool isOnButton;
        private GameObject currentCollision = null;
        private double pointsPerFly = 100;
        
        
        
        //Methods
        public void Shoot()
        {
            aimSprite.DOColor(Color.yellow, 0.3f).From();
            
            if (isOnAFly)
            {
                currentCollision.GetComponent<FlyController>().Kill();
                score.Value += pointsPerFly;
            }

            if (isOnButton)
            {
                currentCollision.GetComponent<Button>()?.onClick.Invoke();
            }
        }
        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Fly")
            {
                isOnAFly = true;
                currentCollision = col.gameObject;
            }

            if (col.tag == "Button")
            {
                isOnButton = true;
                currentCollision = col.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Fly")
            {
                isOnAFly = false;
                currentCollision = null;
            }

            if (other.tag == "Button")
            {
                isOnButton = false;
                currentCollision = null;
            }
        }


    }
}

