using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Game_UI_Manager : MonoSingleton<Game_UI_Manager>
{
    
    public TextMeshProUGUI lifeText, moneyText, waveText;

    private bool buildModeActive = false;
    private int towerID_Active = 0;
    public LayerMask buildMask;
    public float buildSize;
    public GameObject buildPreview;
    public GameObject LastValidBuildPosObject;
    public SpriteRenderer buildPreviewSPR;

    private bool canBuild;
    void Update()
    {
        lifeText.text = GameManager.Instance.Life.ToString();
        moneyText.text = GameManager.Instance.money.ToString();
        waveText.text = WaveManager.Instance.currentWave.ToString();

        if (buildModeActive)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var cast = Physics2D.OverlapCircleAll(mousePos,buildSize / 2,buildMask);
            buildPreview.transform.localScale = Vector3.one * buildSize;
            buildPreview.transform.position = mousePos;

            if(cast.Length == 0)
            {
                canBuild = true;
                buildPreviewSPR.color = new Color(0,1,0,0.2f);
                LastValidBuildPosObject.transform.position = mousePos;
                LastValidBuildPosObject.transform.localScale = Vector3.one * buildSize;


            }
            else
            {
                //canBuild = false;
                buildPreviewSPR.color = new Color(1, 0, 0, 0.2f);
            }


        }
        else
        {

        }

        if (buildModeActive && Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.money < GameManager.Instance.scriptableTowerList[towerID_Active].cost || !canBuild)
            {
                //not enough Money
                buildModeActive = false;
                canBuild = false;

            }
            else
            {
                GameManager.Instance.money -= GameManager.Instance.scriptableTowerList[towerID_Active].cost;

                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                var newTower = Instantiate(GameManager.Instance.towerObjectList[towerID_Active], LastValidBuildPosObject.transform.position, Quaternion.identity);
                canBuild = false;
                buildModeActive = false;
            }
        }
    }

    public void SelectTower(int ID)
    {
        buildModeActive = true;
        towerID_Active = ID;
    }
}
