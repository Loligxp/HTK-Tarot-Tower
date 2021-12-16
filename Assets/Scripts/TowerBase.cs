using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBase : MonoBehaviour
{
    [SerializeField]
    private ScriptableTower _myTower;

    private float timer;
    [SerializeField]
    private List<GameObject> _enemiesInRange;
    public LayerMask scanMask;
    public List<ScriptableBuffs> _activeBuffs;
    private SpriteRenderer SPR;
    private float _damageBuff, _fireRateBuff, _rangeBuff;

    private void OnMouseDown()
    {
        Game_UI_Manager.Instance.SelectActiveTower(_myTower,this.gameObject);
    }

    private void OnMouseEnter()
    {
        SPR.color = Color.blue;
    }

    private void OnMouseExit()
    {
        SPR.color = Color.white;
    }

    private void Start()
    {
        if (_myTower.towerType == ScriptableTower.TowerTypes.WheelOfFortune)
            WaveManager.Instance.FortuneStart += WheelOfFortuneSpin;

        SPR = GetComponent<SpriteRenderer>();
    }

    public void Sell()
    {
        if (_myTower.towerType == ScriptableTower.TowerTypes.WheelOfFortune)
            WaveManager.Instance.FortuneStart -= WheelOfFortuneSpin;

        Destroy(this.gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 / (_myTower.fireRate + _fireRateBuff))
        {
            ScanForEnemies();

            switch (_myTower.towerType)
            {
                case ScriptableTower.TowerTypes.Shooter:
                    if (_enemiesInRange.Count != 0)
                    {
                        BasicShot();
                        timer = 0;

                    }

                    break;
                case ScriptableTower.TowerTypes.BuffTower:
                    BuffCall();
                    timer = 0;

                    break;
                case ScriptableTower.TowerTypes.AOETower:
                    AOE_Attack();
                    timer = 0;
                    break;
                case ScriptableTower.TowerTypes.HitScan:
                    if (_enemiesInRange.Count != 0)
                    {
                        HitScan_Attack();
                        timer = 0;
                    }
                    break;
                case ScriptableTower.TowerTypes.Decloack:
                    foreach (var item in _enemiesInRange)
                    {
                        item.layer = 6;
                    }
                    timer = 0;
                    break;
                case ScriptableTower.TowerTypes.Rolando:
                    if (_enemiesInRange.Count != 0)
                    {
                        HitScan_Attack();
                        timer = 0;
                    }
                    break;
            }
        }
    }

    void WheelOfFortuneSpin()
    {
        GameManager.Instance.AddCoins(Random.Range(0,11));
    }

    void HitScan_Attack()
    {
        _enemiesInRange[0].GetComponent<EnemyBase>().TakeDamage(_myTower.damage);
    }

    void AOE_Attack()
    {
        var bullet = Instantiate(_myTower.projectile, transform.position, Quaternion.identity);
        var bulletScript = bullet.GetComponent<AOE_Attack>();

        bulletScript.AOE_Range += _myTower.range;
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
        var enemiesFound = Physics2D.OverlapCircleAll(transform.position,(_myTower.range + _rangeBuff) /2, scanMask);

        _enemiesInRange = new List<GameObject>();

        for (int i = 0; i < enemiesFound.Length; i++)
        {
            _enemiesInRange.Add(enemiesFound[i].gameObject);
        }
    }

    public IEnumerator AddBuff(ScriptableBuffs newBuff)
    {
        _activeBuffs.Add(newBuff);
        UpdateBuffs();
        yield return new WaitForSeconds(newBuff.buffLenght);
        _activeBuffs.Remove(newBuff);
        UpdateBuffs();

    }

    public void UpdateBuffs()
    {
        _damageBuff = 0;
        _fireRateBuff = 0;
        _rangeBuff = 0;

        for (int i = 0; i < _activeBuffs.Count; i++)
        {
            if (_activeBuffs[i].damageBuff > _damageBuff)
                _damageBuff = _activeBuffs[i].damageBuff;

            if (_activeBuffs[i].fireRateBuff > _fireRateBuff)
                _fireRateBuff = _activeBuffs[i].fireRateBuff;

            if (_activeBuffs[i].rangeBuff > _rangeBuff)
                _rangeBuff = _activeBuffs[i].rangeBuff;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.2f);

        Gizmos.DrawSphere(transform.position,_myTower.range / 2);

        Gizmos.color = new Color(0, 0, 1, 0.2f);

        Gizmos.DrawSphere(transform.position,(_myTower.range + _rangeBuff) / 2);
    }

}

