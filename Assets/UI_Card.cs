using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Card : MonoBehaviour
{
    public TextMeshProUGUI nameText, costText;
    public ScriptableTower myTower;

    void Start()
    {
        nameText.text = myTower.towerName;
        costText.text = myTower.cost.ToString();

        GetComponent<Button>().image.sprite = myTower.UI_Sprite;
    }

}
