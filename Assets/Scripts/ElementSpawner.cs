using UnityEngine;
using DG.Tweening;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;

    public CellHandler Spawn(CardData cardData, Vector2 position, bool doBounceOnStart = false)
    {
        var prefab = Instantiate(_cellPrefab, transform);
        var cellHandler = prefab.GetComponent<CellHandler>();
        var element = cellHandler.Element;

        cellHandler.SetCardData(cardData);
        prefab.transform.position = position;

        if (doBounceOnStart)
        {
            var defaultScale = prefab.transform.localScale;
            prefab.transform.localScale = Vector3.zero;
            cellHandler.EnableSprites();
            BounceWithScale(prefab.transform, defaultScale);
        }
        else
        {
            cellHandler.EnableSprites();
        }


        return cellHandler;
    }

    public void ShakeElement(Transform elementTransform)
    {
        var scale = elementTransform.localScale;
        var seq = DOTween.Sequence();
        seq.Append(elementTransform.DOScale(scale * 0.98f, 0.04f));
        seq.Append(elementTransform.DOScale(scale * 0.99f, 0.04f));
        seq.Append(elementTransform.DOScale(scale * 0.94f, 0.10f));
        seq.Append(elementTransform.DOScale(scale * 0.98f, 0.08f));
        seq.Append(elementTransform.DOScale(scale * 0.75f, 0.2f));
        seq.Append(elementTransform.DOScale(scale * 0.98f, 0.18f));
        seq.Append(elementTransform.DOScale(scale * 0.44f, 0.12f));
        seq.Append(elementTransform.DOScale(scale * 1f, 0.24f));
    }

    public void BounceElement(Transform elementTransform)
    {
        Vector3 scale = elementTransform.localScale;
        BounceWithScale(elementTransform, scale);
    }

    private void BounceWithScale(Transform cellTransform, Vector3 scale)
    {
        var seq = DOTween.Sequence();
        seq.Append(cellTransform.transform.DOScale(scale * 1.2f, 0.2f));
        seq.Append(cellTransform.transform.DOScale(scale * 0.95f, 0.2f));
        seq.Append(cellTransform.transform.DOScale(scale * 1.1f, 0.2f));
        seq.Append(cellTransform.transform.DOScale(scale * 0.98f, 0.2f));
        seq.Append(cellTransform.transform.DOScale(scale * 1f, 0.2f));
    }

    public void ClearScene()
    {
        foreach (Transform cell in transform) Destroy(cell.gameObject);
    }
}