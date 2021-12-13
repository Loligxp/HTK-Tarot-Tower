using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    [SerializeField]
    Transform _startPoint;

    [SerializeField]
    float _spawnsPerSecond, _timer;

    public GameObject _enemy;
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > 1 / _spawnsPerSecond)
        {
            Instantiate(_enemy,_startPoint.position,Quaternion.identity);
            _timer = 0;
        }
    }
}
