using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


public class TreeTrunk : MonoBehaviour
{
    [SerializeField] private Transform jumpPoint;
    [SerializeField] private TMP_Text keyLabel;

    private float xMax = 0.5f;
    private float xMin = -0.5f;
    
    public Transform JumpPosition => jumpPoint.transform;
    public KeyCode JumpKey { get; set; }

    private bool increaseX;

    private void Start()
    {
        keyLabel.text = JumpKey.ToString();
        transform.Translate(Vector3.right * Random.Range(-1,1));
        increaseX = Random.Range(0f, 1f) >= 0.5;
    }

    private void Update()
    {
        if (increaseX)
        {
            transform.Translate(Vector3.right * Time.deltaTime);

            if (transform.position.x > xMax)
            {
                increaseX = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime);

            if (transform.position.x < xMin)
            {
                increaseX = true;
            }
        }
    }
}
