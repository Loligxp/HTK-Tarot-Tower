using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_HitEffect : MonoBehaviour
{
    public float maxSize;
    public float speedGrow;


    void Update()
    {
        transform.localScale = transform.localScale + Vector3.one * speedGrow * Time.deltaTime;

        if (transform.localScale.x > maxSize)
            Destroy(this.gameObject);
    }
}
