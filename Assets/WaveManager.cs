using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [SerializeField]
    Transform _startPoint;

    public List<ScriptableWave> Waves = new List<ScriptableWave>();

    private List<ScriptableWave> currentActiveWave = new List<ScriptableWave>();

    public int currentWave;
    public delegate void WheelOfFortuneCall();
    public WheelOfFortuneCall FortuneStart;

    public GameObject easyEnemy;
    public GameObject mediumEnemy;
    public GameObject StrongEnemy;
    public GameObject CamoEnemy;
    public GameObject FrostEnemy;
    public GameObject FireEnemy;



    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
         //   startWave();
    }

    public void startWave()
    {
        if (Waves.Count > currentWave)
        {
            StartCoroutine(SendWave(Waves[currentWave]));
            currentWave++;
        }
        else
        {
            Debug.Log("You win!");
        }

        if(FortuneStart != null)
            FortuneStart();
    }

    IEnumerator SendWave(ScriptableWave newWave)
    {
        int enemiesSpawned = 0;
        currentActiveWave.Add(newWave);

        for (int i = 0; i < 6; i++)
        {
            int newEnemyCount = 0;
            float newInterval = 0;
            GameObject newEnemy = null;

            switch (i)
            {
                case 0:
                    newEnemyCount = newWave.easyEnemies;
                    newInterval = newWave.easyInterval;
                    newEnemy = easyEnemy;
                    break;
                case 1:
                    newEnemyCount = newWave.MediumEnemies;
                    newInterval = newWave.MediumInterval;
                    newEnemy = mediumEnemy;
                    break;
                case 2:
                    newEnemyCount = newWave.StrongEnemies;
                    newInterval = newWave.StrongInterval;
                    newEnemy = StrongEnemy;
                    break;
                case 3:
                    newEnemyCount = newWave.CamoEnemies;
                    newInterval = newWave.CamoInterval;
                    newEnemy = CamoEnemy;
                    break;
                case 4:
                    newEnemyCount = newWave.FrostEnemies;
                    newInterval = newWave.FrostInterval;
                    newEnemy = FrostEnemy;
                    break;
                case 5:
                    newEnemyCount = newWave.FireEnemies;
                    newInterval = newWave.FireInterval;
                    newEnemy = FireEnemy;
                    break;
            }
            enemiesSpawned = 0;
            while (enemiesSpawned < newEnemyCount)
            {
                Instantiate(newEnemy, _startPoint.position, Quaternion.identity);
                enemiesSpawned++;
                yield return new WaitForSeconds(newInterval);
            }
        }
        

        currentActiveWave.Remove(newWave);
    }
}
