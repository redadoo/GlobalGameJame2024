using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


public class TreeTrunk : MonoBehaviour
{
    [SerializeField] private Transform jumpPoint;
    [SerializeField] private TMP_Text keyLabel;

    private float xMax = 0.4f;
    private float xMin = -0.4f;
    
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
            transform.Translate(Vector3.right * (Time.deltaTime * 0.4f));

            if (transform.position.x > xMax)
            {
                increaseX = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * (Time.deltaTime * 0.4f));

            if (transform.position.x < xMin)
            {
                increaseX = true;
            }
        }
    }

    public void Fall()
    {
        Sequence fallSequence = DOTween.Sequence();
        fallSequence.Append(transform.DOPunchPosition(Vector3.left*0.4f, 1.5f,5,1f));
        fallSequence.Join(transform.DOPunchPosition(Vector3.up*0.4f, 1.5f,3,1f));
        fallSequence.Append(transform.DOMove(transform.position + Vector3.down*5f, 1f));
        fallSequence.AppendCallback(() => { transform.gameObject.SetActive(false); });
    }
}
