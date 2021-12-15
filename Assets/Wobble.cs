using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Wobble : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _startPos, _startScale;
    [SerializeField]
    private Vector3 _positionWobbleStrenght, _scaleWobbleStrenght;

    [SerializeField]
    private float _wobbleStrenghtMultiplier, _wobbleSpeed;



    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("MouseEnter");
        _wobbleSpeed *= 5;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _wobbleSpeed /= 5;
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _startScale = transform.localScale;
    }

    void Update()
    {
        transform.position = _startPos + _positionWobbleStrenght * Mathf.Sin(Time.time * _wobbleSpeed) * _wobbleStrenghtMultiplier;
        transform.localScale = _startScale + _scaleWobbleStrenght * Mathf.Sin(Time.time * _wobbleSpeed) * _wobbleStrenghtMultiplier;
    }
}
