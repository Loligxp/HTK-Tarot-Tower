using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tarot/TarotWave")]
public class ScriptableWave : ScriptableObject
{
    public GameObject enemy;
    public int spawnCount;
    public float spawnInterval;
}
