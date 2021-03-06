using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool debuffBullet;
    public ScriptableDebuffs debuffEffect;

    [SerializeField]
    public float speed, damage;

    [SerializeField]
    private int piercing;

    public Vector2 _direction;

    public GameObject HitEffect;
    private void Update()
    {
        transform.Translate(_direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && piercing >= 0)
        {
            Instantiate(HitEffect,transform.position,transform.rotation);
            if(!debuffBullet)
                collision.GetComponent<EnemyBase>().TakeDamage(damage);
            else
                collision.GetComponent<EnemyBase>().TakeDamage(damage, debuffEffect);

            piercing--;

            if (piercing < 0)
            {
                transform.position += Vector3.up * 10000;
                Destroy(this.gameObject, 1);
            }
        }
    }
}
