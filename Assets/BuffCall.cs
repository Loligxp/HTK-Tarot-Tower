using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCall : MonoBehaviour
{
    public LayerMask hitMask;
    public ScriptableBuffs _buffCaller;
    public float range; /// <summary>
    /// Range will be set from the Firing Tower;
    /// </summary>
    void Start()
    {
        var towersToBuff = Physics2D.OverlapCircleAll(transform.position,range,hitMask);
        Debug.Log("hi");
        foreach (var item in towersToBuff)
        {
            Debug.Log("item");
            item.GetComponent<TowerBase>().AddBuff(_buffCaller);
        }

        Destroy(this.gameObject);
    }
}
