using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Attack : MonoBehaviour
{
    public bool debuffAttack;

    public ScriptableDebuffs _debuffAttack;
    public float AOE_Damage, AOE_Range;
    public LayerMask AOE_Mask;
    void Start()
    {
        var enemiesInAOE = Physics2D.OverlapCircleAll(transform.position,AOE_Range, AOE_Mask);

        foreach (var enemy in enemiesInAOE)
        {
            if (debuffAttack)
            {
                var ENM = enemy.GetComponent<EnemyBase>();
                StartCoroutine(ENM.AddDebuff(_debuffAttack));
            }
            else
                enemy.GetComponent<EnemyBase>().TakeDamge(AOE_Damage, Projectile.DamageTypes.Normal);

        }
        Destroy(this.gameObject, 3);
    }
}
