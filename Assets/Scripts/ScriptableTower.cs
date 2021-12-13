using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tarot/TarotTower")]
public class ScriptableTower : ScriptableObject
{
    public enum TowerTypes
    {
        shooter,
        buffTower,
        AOETower
    }

    public string towerName;
    public string description;
    public TowerTypes towerType;
    public Sprite UI_Sprite;
    public Sprite tower_Sprite;

    public int cost;

    //Stats
    public float  range, fireRate;
    public GameObject projectile;
}
