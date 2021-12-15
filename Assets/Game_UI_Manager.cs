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

    public bool FastForward;
    public Image fastForwardButton;
    public Sprite fastForwardButtonUnpressed, fastForwardButtonDepressed;

    [Space]
    public TextMeshProUGUI towerDescriptionText;
    public Image towerCardImage;
    void Update()
    {
        lifeText.text = GameManager.Instance.Life.ToString();
        moneyText.text = GameManager.Instance.money.ToString();
        waveText.text = WaveManager.Instance.currentWave.ToString();

        towerDescriptionText.text = GameManager.Instance.scriptableTowerList[towerID_Active].description;
        towerCardImage.sprite = GameManager.Instance.scriptableTowerList[towerID_Active].UI_Sprite;

        buildPreview.SetActive(true);

        if (buildModeActive)
        {
            buildPreviewSPR.sprite = GameManager.Instance.scriptableTowerList[towerID_Active].tower_Sprite;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var cast = Physics2D.OverlapCircleAll(mousePos,buildSize / 2,buildMask);
            buildPreview.transform.localScale = Vector3.one;
            buildPreview.transform.position = mousePos + Vector3.up * 0.4f;

            if(cast.Length == 0)
            {
                canBuild = true;
                buildPreviewSPR.color = new Color(0,1,0,0.2f);
                LastValidBuildPosObject.transform.position = mousePos;
                LastValidBuildPosObject.transform.localScale = Vector3.one * buildSize;
                LastValidBuildPosObject.SetActive(true);


            }
            else
            {
                if (Vector2.Distance(LastValidBuildPosObject.transform.position, mousePos) < 0.5f)
                {
                }
                else
                {
                    LastValidBuildPosObject.SetActive(false);
                }
                //canBuild = false;
                buildPreviewSPR.color = new Color(1, 0, 0, 0.2f);
            }


        }
        else
        {
            LastValidBuildPosObject.SetActive(false);
            buildPreview.SetActive(false);
        }

        if (buildModeActive && Input.GetMouseButtonDown(0))
        {
            if ((GameManager.Instance.money < GameManager.Instance.scriptableTowerList[towerID_Active].cost || !canBuild) || !LastValidBuildPosObject.activeInHierarchy)
            {
                //not enough Money or no place
                buildModeActive = false;
                canBuild = false;

            }
            else
            {
                GameManager.Instance.money -= GameManager.Instance.scriptableTowerList[towerID_Active].cost;

                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                var newTower = Instantiate(GameManager.Instance.towerObjectList[towerID_Active], LastValidBuildPosObject.transform.position + Vector3.up * 0.4f, Quaternion.identity);
                canBuild = false;
                buildModeActive = false;
            }
        }

        if (Time.timeScale == 1f && FastForward)
            Time.timeScale = 2f;
    }

    public void SelectTower(int ID)
    {
        buildModeActive = true;
        towerID_Active = ID;
    }

    public void SwitchFastForwardMode()
    {
        FastForward = !FastForward;

        if (FastForward)
        {
            fastForwardButton.sprite = fastForwardButtonDepressed;
            Time.timeScale = 2f;
        }
        else
        {
            fastForwardButton.sprite = fastForwardButtonUnpressed;
            Time.timeScale = 1f;
        }
    }
}
