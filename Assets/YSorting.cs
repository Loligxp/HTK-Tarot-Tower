using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSorting : MonoBehaviour
{
    public float sortOffset = 0;
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt((-transform.position.y * 100) + sortOffset);
    }
}
