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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            startWave();
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

        while (enemiesSpawned < newWave.spawnCount)
        {
            Instantiate(newWave.enemy, _startPoint.position,Quaternion.identity);
            enemiesSpawned++;
            yield return new WaitForSeconds(newWave.spawnInterval);
        }

        currentActiveWave.Remove(newWave);
    }
}
