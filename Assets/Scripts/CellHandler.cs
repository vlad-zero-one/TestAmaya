using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CellHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject _element;
    [SerializeField] GameObject _outline;

    public GameObject Element => _element;
    public CardData CardData => _cardData;

    public ClickEvent OnClick;
    public void OnPointerClick(PointerEventData eventData) => OnClick.Invoke(this);


    private CardData _cardData;
    private SpriteRenderer _elementSpriteRenderer;

    public void SetCardData(CardData cardData)
    {
        _cardData = cardData;
        _element.GetComponent<SpriteRenderer>().sprite = cardData.Sprite;
    }

    public void EnableSprites()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _element.GetComponent<SpriteRenderer>().enabled = true;
        foreach(Transform plankTransform in _outline.transform)
        {
            plankTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}

[System.Serializable]
public class ClickEvent : UnityEvent<CellHandler> { }