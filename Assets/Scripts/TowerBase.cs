using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBase : MonoBehaviour
{
    [SerializeField]
    private ScriptableTower _myTower;
    public ScriptableTower _weirdFix;
    public ScriptableTower _lovers;

    private float timer;
    [SerializeField]
    private List<GameObject> _enemiesInRange;
    public LayerMask scanMask;
    public List<ScriptableBuffs> _activeBuffs;
    private SpriteRenderer SPR;
    private float _damageBuff, _fireRateBuff, _rangeBuff;
    public GameObject HitScanHitFX;

    [Space]
    public GameObject unrealBullet;
    public GameObject UnityBullet;
    public bool RolandoUnreal;

    [Space]
    public Animator anims;
    private void OnMouseDown()
    {
        if(_myTower != _weirdFix)
            Game_UI_Manager.Instance.SelectActiveTower(_myTower,this.gameObject);
        else
            Game_UI_Manager.Instance.SelectActiveTower(_lovers, this.gameObject);
    }

    private void OnMouseEnter()
    {
        SPR.color = (Color.white * 3 + Color.blue) / 4;
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

    public float ReturnRange()
    {
        if (_rangeBuff > 1)
        {
            return _myTower.range * _rangeBuff;
        }
        else
            return _myTower.range;
    }

    public void Sell()
    {
        if (_myTower.towerType == ScriptableTower.TowerTypes.WheelOfFortune)
            WaveManager.Instance.FortuneStart -= WheelOfFortuneSpin;

        GameManager.Instance.AddCoins(_myTower.cost/2);
        Destroy(this.gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;

        var actualFireRate = _myTower.fireRate * _fireRateBuff;
        if (actualFireRate == 0)
            actualFireRate = _myTower.fireRate;

        if (timer > 1 / (actualFireRate))
        {
            ScanForEnemies();

            switch (_myTower.towerType)
            {
                case ScriptableTower.TowerTypes.Shooter:
                    if (_enemiesInRange.Count != 0)
                    {
                        AnimationAttack();
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
                        AnimationAttack();
                    }
                    break;
                case ScriptableTower.TowerTypes.Decloack:
                    foreach (var item in _enemiesInRange)
                    {
                        item.layer = 6;
                        item.tag = "Enemy";
                        item.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    timer = 0;
                    break;
                case ScriptableTower.TowerTypes.Rolando:
                    if (_enemiesInRange.Count != 0)
                    {
                        if (RolandoUnreal)
                        {
                            var bullet = Instantiate(unrealBullet, transform.position, Quaternion.identity);
                            var bulletScript = bullet.GetComponent<Projectile>();

                            bulletScript._direction = (_enemiesInRange[0].transform.position - transform.position).normalized;

                            var buffDamage = _damageBuff;
                            if (buffDamage == 0)
                                buffDamage = 1;
                            bulletScript.damage *= buffDamage;
                            RolandoUnreal = !RolandoUnreal;
                        }
                        else
                        {
                            var bullet = Instantiate(UnityBullet, transform.position, Quaternion.identity);
                            var bulletScript = bullet.GetComponent<Projectile>();

                            bulletScript._direction = (_enemiesInRange[0].transform.position - transform.position).normalized;

                            var buffDamage = _damageBuff;
                            if (buffDamage == 0)
                                buffDamage = 1;
                            bulletScript.damage *= buffDamage;
                            RolandoUnreal = !RolandoUnreal;
                        }
                        timer = 0;
                    }
                    break;
            }
        }
    }

    void AnimationAttack()
    {
        if(anims != null)
            anims.SetTrigger("Attack");
    }

    void WheelOfFortuneSpin()
    {
        GameManager.Instance.AddCoins(Random.Range(0,101));
    }

    void HitScan_Attack()
    {
        var i = Random.Range(0,_enemiesInRange.Count);

        Instantiate(HitScanHitFX, _enemiesInRange[i].transform.position, _enemiesInRange[i].transform.rotation);
        _enemiesInRange[i].GetComponent<EnemyBase>().TakeDamage(_myTower.damage);
        timer = 0;

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

        var buffDamage = _damageBuff;
        if (buffDamage == 0)
            buffDamage = 1;
        bulletScript.damage *= buffDamage;
    }

    void ScanForEnemies()
    {
        Collider2D[] enemiesFound;
        if (_rangeBuff > 1)
           enemiesFound  = Physics2D.OverlapCircleAll(transform.position,(_myTower.range * _rangeBuff) /2, scanMask);
        else
            enemiesFound = Physics2D.OverlapCircleAll(transform.position, (_myTower.range) / 2, scanMask);


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

