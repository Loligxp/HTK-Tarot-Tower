using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //Stats
    [SerializeField]
    private float _movementSpeed, _health;

    [SerializeField]
    private Vector3 _goalPosition = Vector3.up;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,_goalPosition,Time.deltaTime * _movementSpeed);

        if(_goalPosition == Vector3.zero)
        {
            Debug.Log("Enemy Reached end -1HP");
            Destroy(this.gameObject);
        }

        if(_health <= 0)
        {
            Debug.Log("Enemy Died +1Mone or somthing");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PathNode"))
        {
            _goalPosition = PathManager.Instance.GetNextNodePosition(collision.gameObject);
        }
    }

    public void TakeDamge(float damage, Projectile.DamageTypes _damageType)
    {
        switch (_damageType)
        {
            case Projectile.DamageTypes.Normal:
                _health -= damage;
                break;
            case Projectile.DamageTypes.Fire:
                break;
            case Projectile.DamageTypes.Ice:
                break;
        }
    }
}
