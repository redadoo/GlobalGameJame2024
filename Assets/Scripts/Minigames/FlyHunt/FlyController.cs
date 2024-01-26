using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fireflys
{
    public class FlyController : MonoBehaviour
    {
        [SerializeField] private float minX = 0;
        [SerializeField] private float maxX = 0;
        [SerializeField] private float minY = 0;
        [SerializeField] private float maxY = 0;
        [SerializeField] private float minSpeed = 2f;
        [SerializeField] private float maxSpeed = 4f;
        [SerializeField] private GameObject deathEffectPrefab;
        
        private float timeOnScreen;
        private Vector2 direction = Vector2.down;
        private float timeToNextChange = 0f;
        private float minTimeToNextChange = 0.3f;
        private float maxTimeToNextChange = 1.2f;
        private float speed;

        private void Start()
        {
            while (direction == Vector2.zero)
                direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            direction.Normalize();
            
            timeOnScreen = Random.Range(5f, 12f);
            
            speed = Random.Range(minSpeed, maxSpeed);
            
            timeToNextChange = Random.Range(minTimeToNextChange, maxTimeToNextChange);
            Debug.Log(timeToNextChange);
        }

        private void Update()
        {
            if (timeOnScreen < 0)
            {
                if (direction.y < 0.5)
                    direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));
                transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f);

                if (transform.localPosition.y > maxY + 20)
                {
                    Destroy(this.gameObject);
                }
                return;
            }
            
            CheckBoundaries();
            
            timeToNextChange -= Time.deltaTime;
            timeOnScreen -= Time.deltaTime;
            
            if (timeToNextChange <= 0)
            {
                DoChange();
            }
            else
            {
                transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f);
            }
            
        }

        public void Kill()
        {
            Instantiate(deathEffectPrefab, transform.position, quaternion.identity, transform.parent);
            Destroy(this.gameObject);
        }

        private void CheckBoundaries()
        {
            float currX = transform.localPosition.x;
            float currY = transform.localPosition.y;
            
            
            
            if (Mathf.Abs(currX - maxX) < 10 && direction.x > 0)
            {
                direction.x = -direction.x;
            }
            
            if (Mathf.Abs(currX - minX) < 10 && direction.x < 0)
            {
                direction.x = -direction.x;
            }
            
            if (Mathf.Abs(currY - maxY) < 10 && direction.y > 0)
            {
                direction.y = -direction.y;
            }
            
            if (Mathf.Abs(currY - minY) < 10 && direction.y < 0)
            {
                direction.y = -direction.y;
            }
        }

        private void DoChange()
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            while (direction == Vector2.zero)
                direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            direction.Normalize();
            speed = Random.Range(minSpeed, maxSpeed);
            timeToNextChange = Random.Range(minTimeToNextChange, maxTimeToNextChange);
        }
    }
}

