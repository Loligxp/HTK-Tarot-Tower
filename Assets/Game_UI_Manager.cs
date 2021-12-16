using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Game_UI_Manager : MonoSingleton<Game_UI_Manager>
{
    
    public TextMeshProUGUI lifeText, moneyText, waveText;

    private bool buildModeActive = false;
    private int towerID_Active = 0;
    public LayerMask buildMask;
    public Vector2 buildSize;
    public GameObject buildPreview;
    public GameObject LastValidBuildPosObject;
    public SpriteRenderer buildPreviewSPR;

    private bool canBuild;
    public bool autoPlayActive = false;
    public bool FastForward;
    public Image fastForwardButton;
    public Sprite fastForwardButtonUnpressed, fastForwardButtonDepressed;

    [Space]
    public TextMeshProUGUI towerDescriptionText;
    public Image towerCardImage;

    private bool sellModeActive;
    private GameObject sellTowerObject;
    private TowerBase sellTowerScript;
    public GameObject sellEffect;
    public GameObject rangeIndicator;
    public GameObject sellButton;

    private void Start()
    {
        towerID_Active = 4;
    }

    void Update()
    {
        sellButton.SetActive(sellModeActive);

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
            var cast = Physics2D.OverlapBoxAll(mousePos,buildSize,0,buildMask);
            buildPreview.transform.localScale = Vector3.one;
            buildPreview.transform.position = mousePos + Vector3.up * 0.4f;

            rangeIndicator.transform.position = mousePos + Vector3.up * 0.4f;
            rangeIndicator.transform.localScale = Vector3.one * GameManager.Instance.scriptableTowerList[towerID_Active].range;

            if (cast.Length == 0)
            {
                canBuild = true;
                buildPreviewSPR.color = new Color(0,1,0,0.2f);
                rangeIndicator.SetActive(true);
                LastValidBuildPosObject.transform.position = mousePos;
                LastValidBuildPosObject.transform.localScale = Vector3.one * buildSize;
                LastValidBuildPosObject.SetActive(true);


            }
            else
            {
                rangeIndicator.SetActive(false);

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

        if (sellModeActive)
        {
            rangeIndicator.SetActive(true);
            sellEffect.transform.position = sellTowerObject.transform.position + -Vector3.up * 0.4f;
            rangeIndicator.transform.position = sellTowerObject.transform.position;
            rangeIndicator.transform.localScale = Vector3.one * sellTowerScript.ReturnRange();
        }
        else
        {
            sellEffect.transform.position = Vector3.up * 9000;

            if(!buildModeActive)
                rangeIndicator.transform.position = Vector3.up * 9000;

        }

        if (sellModeActive && Input.GetKeyDown(KeyCode.F))
        {
            sellTowerObject.GetComponent<TowerBase>().Sell();
            sellModeActive = false;
        }

        if (Time.timeScale == 1f && FastForward)
            Time.timeScale = 2f;

        if (Input.GetMouseButtonDown(1))
        {
            sellModeActive = false;
            buildModeActive = false;
        }
    }

    public void SellTower()
    {
        if (sellModeActive)
        {
            sellTowerObject.GetComponent<TowerBase>().Sell();
            sellModeActive = false;
        }
    }

    public void OnDrawGizmos()
    {
        Vector3 buildSizeee = buildSize;
        buildSizeee.z = 50;

        Vector3 mous = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mous.z = 0;
        Gizmos.DrawWireCube(mous,buildSize);
    }

    public void SelectActiveTower(ScriptableTower towerType, GameObject towerObject)
    {
        for (int i = 0; i < GameManager.Instance.scriptableTowerList.Count; i++)
        {
            if(GameManager.Instance.scriptableTowerList[i] == towerType)
            {
                towerID_Active = i;
                sellModeActive = true;
                buildModeActive = true;
                sellTowerObject = towerObject;
                sellTowerScript = towerObject.GetComponent<TowerBase>();
                break;
            }
        }
    }

    public void SelectTower(int ID)
    {
        buildModeActive = true;
        sellModeActive = false;
        towerID_Active = ID;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void SetAutoPlay(bool set)
    {
        autoPlayActive = set;
    }
}
