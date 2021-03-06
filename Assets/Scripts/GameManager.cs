using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int money = 0;
    public int Life = 200;

    public List<ScriptableTower> scriptableTowerList;
    public List<GameObject> towerObjectList;

    private void Start()
    {
        money = 400;
        Time.timeScale = 1;
    }

    public void AddCoins(int amount)
    {
        money += amount;
    }

    public void RemoveLife(int amount)
    {
        Life -= amount;

        if(Life <= 0)
        {
            Game_UI_Manager.Instance.GameLost = true;
        }
    }
}
