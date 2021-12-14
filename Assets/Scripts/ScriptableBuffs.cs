using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tarot/TarotBuff")]
public class ScriptableBuffs : ScriptableObject
{
    public string buffName;
    public float buffLenght, damageBuff, fireRateBuff, rangeBuff;
}
