using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tarot/TarotTower")]
public class ScriptableTower : ScriptableObject
{
    public enum TowerTypes
    {
        Shooter,
        BuffTower,
        AOETower,
        HitScan,
        Decloack,
        WheelOfFortune,
        Rolando
    }

    public string towerName;
    public string description;
    public TowerTypes towerType;
    public Sprite UI_Sprite;
    public Sprite tower_Sprite;

    public int cost;

    //Stats
    public float damage, range, fireRate;
    public GameObject projectile;
}
