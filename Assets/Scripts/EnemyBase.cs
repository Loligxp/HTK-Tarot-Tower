using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //Stats
    [SerializeField]
    private float _movementSpeed, _health;

    [SerializeField]
    private int _worth;

    [SerializeField]
    private Vector3 _goalPosition = Vector3.up;
    public List<ScriptableDebuffs> _activeDebuffs;
    public float burnStrenght, freezeStrenght;


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,_goalPosition,Time.deltaTime * Mathf.Clamp(_movementSpeed - freezeStrenght,0,99));
        if (burnStrenght > 0)
            TakeDamge(burnStrenght * Time.deltaTime,Projectile.DamageTypes.Normal);

        if(_goalPosition == Vector3.zero)
        {
            Debug.Log("Enemy Reached end -1HP");
            Destroy(this.gameObject);
        }

        if(_health <= 0)
        {
            GameManager.Instance.AddCoins(_worth);
            Destroy(this.gameObject);
        }
    }
    
    public IEnumerator AddDebuff(ScriptableDebuffs newBuff)
    {
        _activeDebuffs.Add(newBuff);
        UpdateBuffs();
        Debug.Log("atach " + newBuff.debuffLenght);
        yield return new WaitForSeconds(newBuff.debuffLenght);
        Debug.Log("deTach");
        _activeDebuffs.Remove(newBuff);
        UpdateBuffs();

    }

    public void UpdateBuffs()
    {
        burnStrenght = 0;
        freezeStrenght = 0;

        for (int i = 0; i < _activeDebuffs.Count; i++)
        {
            if (_activeDebuffs[i].fireDebuff > burnStrenght)
                burnStrenght = _activeDebuffs[i].fireDebuff;

            if (_activeDebuffs[i].freezeDebuff > freezeStrenght)
                freezeStrenght = _activeDebuffs[i].freezeDebuff;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PathNode"))
        {
            _goalPosition = PathManager.Instance.GetNextNodePosition(collision.gameObject);
        }
    }

    public void TakeDamge(float damage, Projectile.DamageTypes _damageType) //_damageType Might be unnecesarry; Remove later 
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
