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

    //private Vector3 _scale;
    //private Vector3 _cellScale;
    private CardData _cardData;
    private SpriteRenderer _elementSpriteRenderer;

    void Start()
    {
        //_scale = _element.transform.localScale;
        //_cellScale = transform.localScale;
    }

    void Update()
    {
        
    }

    public void SetCardData(CardData cardData)
    {
        _cardData = cardData;
        _element.GetComponent<SpriteRenderer>().sprite = cardData.Sprite;

        //_elementSpriteRenderer.sprite = cardData.Sprite;
    }

    //public void ShakeElement()
    //{
    //    Transform elementTransform = _element.transform;
    //    var seq = DOTween.Sequence();
    //    seq.Append(elementTransform.DOScale(_scale * 0.98f, 0.04f));
    //    seq.Append(elementTransform.DOScale(_scale * 0.99f, 0.04f));
    //    seq.Append(elementTransform.DOScale(_scale * 0.94f, 0.10f));
    //    seq.Append(elementTransform.DOScale(_scale * 0.98f, 0.08f));
    //    seq.Append(elementTransform.DOScale(_scale * 0.75f, 0.2f));
    //    seq.Append(elementTransform.DOScale(_scale * 0.98f, 0.18f));
    //    seq.Append(elementTransform.DOScale(_scale * 0.44f, 0.12f));
    //    seq.Append(elementTransform.DOScale(_scale * 1f, 0.24f));
    //}

    //public void BounceElement(bool bounceCell = false)
    //{
    //    var seq = DOTween.Sequence();
    //    Vector3 scale;
    //    Transform elementTransform;

    //    if (bounceCell)
    //    {
    //        //transform.localScale = Vector3.zero;
    //        elementTransform = transform;
    //        scale = _cellScale;
    //        //EnableSprites();
    //    }
    //    else
    //    {
    //        elementTransform = _element.transform;
    //        scale = _scale;
    //    }


    //    seq.Append(elementTransform.transform.DOScale(scale * 1.2f, 0.2f));
    //    seq.Append(elementTransform.transform.DOScale(scale * 0.95f, 0.2f));
    //    seq.Append(elementTransform.transform.DOScale(scale * 1.1f, 0.2f));
    //    seq.Append(elementTransform.transform.DOScale(scale * 0.98f, 0.2f));
    //    seq.Append(elementTransform.transform.DOScale(scale * 1f, 0.2f));
    //}

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