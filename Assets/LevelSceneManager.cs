using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSceneManager : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
}
