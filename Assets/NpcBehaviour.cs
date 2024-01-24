using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NpcBehaviour : MonoBehaviour
{
    [SerializeField] private bool       needToRotate;
    [SerializeField] private Vector3    dir;

    // Start is called before the first frame update
    void Start()
    {
        needToRotate = false;
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RotateTo(Vector2 dir)
    {

    }
}
