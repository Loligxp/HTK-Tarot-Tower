using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int money = 0;


    public void AddCoins(int amount)
    {
        money += amount;
    }
}
