using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [SerializeField]
    private ScriptableTower _myTower;

    private float timer;
    [SerializeField]
    private List<GameObject> _enemiesInRange;
    public LayerMask scanMask;
    public List<ScriptableBuffs> _activeBuffs;

    private float _damageBuff, _fireRateBuff;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1 / _myTower.fireRate + _fireRateBuff)
        {
            ScanForEnemies();
            if (_enemiesInRange.Count != 0)
            {
                switch (_myTower.towerType)
                {
                    case ScriptableTower.TowerTypes.shooter:
                        BasicShot();
                        break;
                    case ScriptableTower.TowerTypes.buffTower:
                        break;
                }
                timer = 0;
            }
        }
    }
    void BuffCall()
    {
        var bullet = Instantiate(_myTower.projectile, transform.position, Quaternion.identity);
        var bulletScript = bullet.GetComponent<BuffCall>();

        bulletScript.range += _myTower.range;
    }

    void BasicShot()
    {
        var bullet = Instantiate(_myTower.projectile, transform.position, Quaternion.identity);
        var bulletScript = bullet.GetComponent<Projectile>();

        bulletScript._direction = (_enemiesInRange[0].transform.position - transform.position).normalized;
        bulletScript.damage += _damageBuff;
    }

    void ScanForEnemies()
    {
        var enemiesFound = Physics2D.OverlapCircleAll(transform.position,_myTower.range, scanMask);

        _enemiesInRange = new List<GameObject>();

        for (int i = 0; i < enemiesFound.Length; i++)
        {
            _enemiesInRange.Add(enemiesFound[i].gameObject);
        }
    }

    public IEnumerator AddBuff(ScriptableBuffs newBuff)
    {
        _activeBuffs.Add(newBuff);
        yield return new WaitForSeconds(newBuff.buffLenght);
        _activeBuffs.Remove(newBuff);
    }

    public void UpdateBuffs()
    {
        _damageBuff = 0;
        _fireRateBuff = 0;

        for (int i = 0; i < _activeBuffs.Count; i++)
        {
            if (_activeBuffs[i].damageBuff > _damageBuff)
                _damageBuff = _activeBuffs[i].damageBuff;

            if (_activeBuffs[i].fireRateBuff > _fireRateBuff)
                _fireRateBuff = _activeBuffs[i].fireRateBuff;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.2f);

        Gizmos.DrawSphere(transform.position,_myTower.range);
    }
}

