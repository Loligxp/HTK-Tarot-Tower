using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tarot/TarotDebuff")]
public class ScriptableDebuffs : ScriptableObject
{
    public string debuffName;
    public float debuffLenght, fireDebuff, freezeDebuff;
}
