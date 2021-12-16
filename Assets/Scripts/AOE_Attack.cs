using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Attack : MonoBehaviour
{
    public bool debuffAttack;

    public ScriptableDebuffs _debuffAttack;
    public float AOE_Damage, AOE_Range;
    public LayerMask AOE_Mask;
    public GameObject HitEffect;
    void Start()
    {
        var enemiesInAOE = Physics2D.OverlapCircleAll(transform.position,AOE_Range / 2, AOE_Mask);

        foreach (var enemy in enemiesInAOE)
        {
            if (debuffAttack)
            {
                var ENM = enemy.GetComponent<EnemyBase>();
                StartCoroutine(ENM.AddDebuff(_debuffAttack));
            }
            else
                enemy.GetComponent<EnemyBase>().TakeDamage(AOE_Damage);

        }
        var FX = Instantiate(HitEffect,transform.position,transform.rotation);

        FX.GetComponent<AOE_HitEffect>().maxSize = AOE_Range;
        Destroy(this.gameObject, 3);
    }
}
