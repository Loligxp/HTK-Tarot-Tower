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
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1 / _myTower.fireRate)
        {
            scanForEnemies();
            if (_enemiesInRange.Count != 0)
            {
                basicShot();
                timer = 0;
            }
        }
    }

    void basicShot()
    {
        var bullet = Instantiate(_myTower.projectile, transform.position, Quaternion.identity);
        var bulletScript = bullet.GetComponent<Projectile>();

        bulletScript._direction = (_enemiesInRange[0].transform.position - transform.position).normalized;
    }

    void scanForEnemies()
    {
        var enemiesFound = Physics2D.OverlapCircleAll(transform.position,_myTower.range, scanMask);

        _enemiesInRange = new List<GameObject>();

        for (int i = 0; i < enemiesFound.Length; i++)
        {
            _enemiesInRange.Add(enemiesFound[i].gameObject);
        }
    }
}
