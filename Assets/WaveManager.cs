using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    public static int EnemiesAlive;
    private float timer;
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

    [Space, SerializeField]
    private Popups[] popups;
    private bool popupActive;

    private void Update()
    {
        if (popupActive)
            Time.timeScale = 0;

        if (EnemiesAlive == 0)
            timer += Time.deltaTime;
        else
            timer = 0;

        if(timer > 1 && Game_UI_Manager.Instance.autoPlayActive)
        {
            startWave();
            timer = 0;
        }
    }

    public void ClosePopup(GameObject closeObject)
    {
        closeObject.SetActive(false);
        popupActive = false;
        Time.timeScale = 1;
    }

    public void startWave()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            if(popups[i].wavePopup == currentWave)
            {
                popups[i].popupObject.SetActive(true);
                popupActive = true;
            }
        }

        if (Waves.Count > currentWave)
        {
            StartCoroutine(SendWave(Waves[currentWave]));
            currentWave++;
            if (FortuneStart != null)
                FortuneStart();
        }
        else
        {
            if (EnemiesAlive != 0)
                return;
            Game_UI_Manager.Instance.GameWon = true;
        }

        
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
