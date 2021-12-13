using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TarotTower")]
public class ScriptableTower : ScriptableObject
{
    public string towerName;
    public string description;
    public Sprite UI_Sprite;
    public Sprite tower_Sprite;

    public int cost;

    //Stats
    public float  range, fireRate;
    public GameObject projectile;
}
