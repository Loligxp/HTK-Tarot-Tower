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
        foreach (var item in towersToBuff)
        {
            var towerBase = item.GetComponent<TowerBase>();

            if (towerBase != null)
                towerBase.StartCoroutine(towerBase.AddBuff(_buffCaller));
        }

        Destroy(this.gameObject);
    }
}
