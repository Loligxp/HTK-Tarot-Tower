using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tarot/TarotWave")]
public class ScriptableWave : ScriptableObject
{
    [Space]
    public int easyEnemies;
    public float easyInterval;
    [Space]

    public int MediumEnemies;
    public float MediumInterval;
    [Space]

    public int StrongEnemies;
    public float StrongInterval;
    [Space]

    public int CamoEnemies;
    public float CamoInterval;
    [Space]

    public int FrostEnemies;
    public float FrostInterval;
    [Space]

    public int FireEnemies;
    public float FireInterval;
}
