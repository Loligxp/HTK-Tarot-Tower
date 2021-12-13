using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoSingleton<PathManager>
{
    public GameObject[] _pathList;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetNextNodePosition(GameObject currentNode)
    {

        for (int i = 0; i < _pathList.Length; i++)
        {
            if (_pathList[i] == currentNode)
            {
                if(i + 1 == _pathList.Length)
                {
                    return new Vector3(0,0,0); // 0,0,0 == DeathVector
                }
                else
                {
                    return _pathList[i + 1].transform.position;
                }
            }
        }

        Debug.LogError("No valid nextNode found; check code",this);
        return new Vector3(0,0,0); // DeathVector
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _pathList.Length; i++)
        {
            if(i == 0)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(_pathList[i].transform.position,0.5f);
            }

            Gizmos.color = Color.blue;

            if(i + 1 != _pathList.Length)
            {
                Gizmos.DrawLine(_pathList[i].transform.position, _pathList[i + 1].transform.position);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_pathList[i].transform.position, 0.5f);
            }
        }
    }
}
