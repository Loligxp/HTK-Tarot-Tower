using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum DamageTypes
    {
        Normal,
        Fire,
        Ice
    }
    public DamageTypes _bulletType;

    [SerializeField]
    private float speed, damage;

    public Vector2 _direction;

    private void Update()
    {
        transform.Translate(_direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().TakeDamge(damage, _bulletType);
            Destroy(this.gameObject);
        }
    }
}
