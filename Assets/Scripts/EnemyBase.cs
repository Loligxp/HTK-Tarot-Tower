using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public enum EnemyTypes
    {
        Normal,
        Camo,
        Regen,
        FreezeRes,
        FireRes
    }

    public EnemyTypes myEnemyType;

    //Stats
    [SerializeField]
    private float _movementSpeed, _health;

    [SerializeField]
    private int _worth, _damage;

    [SerializeField]
    private GameObject _goalPosition = null;
    public List<ScriptableDebuffs> _activeDebuffs;
    public float burnStrenght, freezeStrenght;

    private void Start()
    {
            _goalPosition = PathManager.Instance.GetStartPosition();
    }
    void Update()
    {
        if (_goalPosition == null)
        {
            GameManager.Instance.RemoveLife(_damage);
            Destroy(this.gameObject);
        }

        if (myEnemyType != EnemyTypes.FreezeRes)
            transform.position = Vector2.MoveTowards(transform.position,_goalPosition.transform.position,Time.deltaTime * Mathf.Clamp(_movementSpeed - freezeStrenght,0,99));
        else
            transform.position = Vector2.MoveTowards(transform.position, _goalPosition.transform.position, Time.deltaTime * Mathf.Clamp(_movementSpeed, 0, 99));

        transform.up = transform.position - _goalPosition.transform.position;

        if(myEnemyType != EnemyTypes.FireRes)
            if (burnStrenght > 0)
                TakeDamage(burnStrenght * Time.deltaTime);

       

        if(_health <= 0)
        {
            GameManager.Instance.AddCoins(_worth);
            Destroy(this.gameObject);
        }

        if( Vector2.Distance(transform.position, _goalPosition.transform.position) < 0.1f)
        {
            _goalPosition = PathManager.Instance.GetNextNodePosition(_goalPosition);
        }
    }
    
    public IEnumerator AddDebuff(ScriptableDebuffs newBuff)
    {
        _activeDebuffs.Add(newBuff);
        UpdateBuffs();
        yield return new WaitForSeconds(newBuff.debuffLenght);
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


    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public void TakeDamage(float damage, ScriptableDebuffs newDebuff)
    {
        _health -= damage;

        StartCoroutine(AddDebuff(newDebuff));
    }
}
