using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private CellPositionsManager _cellPositionsManager;
    [SerializeField] private KnownBundles _knownBundles;
    [SerializeField] private ElementSpawner _elementSpawner;
    [SerializeField] private Text _text;

    public UnityEvent Win;
    public UnityEvent EndGame;

    private int winId;
    private CardDataBundle currentBundleOnScene;
    private Vector2[][,] _positions;
    private int _iteration;
    private Dictionary<CardDataBundle, List<int>> _allowedIds;

    private delegate void SceneCLear();
    private event SceneCLear _sceneClearEvent;

    public delegate void SceneLoaded();
    public event SceneLoaded SceneInitAndLoaded;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        _iteration = 0;
        _allowedIds = new Dictionary<CardDataBundle, List<int>>();

        _cellPositionsManager.Init();
        _positions = _cellPositionsManager.GetPlaces();

        SetAllowedIds();

        _sceneClearEvent += ManageThis;
        _sceneClearEvent.Invoke();

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        // Симуляция ожидания загрузки НЕ СРАБОТАЛО
        yield return new WaitForSeconds(5);
        SceneInitAndLoaded?.Invoke();
    }

    private void SetAllowedIds()
    {
        foreach (var bundle in _knownBundles.Value)
        {
            List<int> ids = new List<int>();
            foreach (var card in bundle.CardsData)
            {
                ids.Add(card.Id);
            }
            _allowedIds.Add(bundle, ids);
        }
    }

    public void ManageThis()
    {
        if (_iteration >= _positions.Length)
        {
            EndGame.Invoke();
            _sceneClearEvent -= ManageThis;
            _text.DOFade(0, 0);
            return;
        }

        var currentLevelPositions = _positions[_iteration];
        var cardsData = GetRandomCardsData(currentLevelPositions.Length);

        int i = 0;
        foreach (var position in currentLevelPositions)
        {
            CardData cardData = cardsData[i];

            var cellHandler = _elementSpawner.Spawn(cardData, position, _iteration == 0);
            cellHandler.OnClick.AddListener(cHandler => WinLoseCheck(cHandler));

            i++;
        }

        var selectedCard = cardsData[Random.Range(0, cardsData.Length)];
        winId = selectedCard.Id;
        ShowText(selectedCard);

        _iteration++;
    }

    private void ShowText(CardData cardData)
    {
        _text.text = $"Find {cardData.Name}";
        _text.DOFade(1, 1);
    }

    private CardData[] GetRandomCardsData(int amount)
    {
        CardData[] cardsData = new CardData[amount];

        int length = _knownBundles.Value.Length;

        List<int> bundlesIndexes = new List<int>();
        for (int i = 0; i < length; i++) bundlesIndexes.Add(i);

        int lastIndex = bundlesIndexes[Random.Range(0, bundlesIndexes.Count)];
        while (bundlesIndexes.Count > 0)
        {
            var randomBundle = _knownBundles.Value[lastIndex];
            if (DoesBundleContainsEnoughValues(randomBundle, amount))
            {
                currentBundleOnScene = randomBundle;
                var randomIds = GetRandomIds(randomBundle, amount);
                for(int j = 0; j < amount; j++)
                {
                    cardsData[j] = randomBundle.CardsData[randomIds[j]];
                }
                break;
            }
            else
            {
                bundlesIndexes.RemoveAt(lastIndex);
                lastIndex = bundlesIndexes[Random.Range(0, bundlesIndexes.Count)];
            }
        }
        if (bundlesIndexes.Count == 0) Debug.Log("НЕТ НЕОБХОДИМОГО НАБОРА ДАННЫХ!");

        return cardsData;
    }

    private void WinLoseCheck(CellHandler cellHandler)
    {
        if (cellHandler.CardData.Id == winId)
        {
            _elementSpawner.BounceElement(cellHandler.Element.transform);
            Win.Invoke();
            _allowedIds[currentBundleOnScene].Remove(winId);
            _elementSpawner.ClearScene();
            _sceneClearEvent.Invoke();
        }
        else
        {
            _elementSpawner.ShakeElement(cellHandler.Element.transform);
        }
    }

    private bool DoesBundleContainsEnoughValues(CardDataBundle bundle, int amount)
    {
        return _allowedIds[bundle].Count >= amount;
    }

    private List<int> GetRandomIds(CardDataBundle bundle, int amount)
    {
        List<int> elementsIds = new List<int>();
        var allowedIds = _allowedIds[bundle];
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, allowedIds.Count);
            bool alreadyIn = elementsIds.Contains(allowedIds[randomIndex]);
            while (alreadyIn)
            {
                randomIndex = Random.Range(0, allowedIds.Count);
                alreadyIn = elementsIds.Contains(allowedIds[randomIndex]);
            }
            elementsIds.Add(allowedIds[randomIndex]);
        }
        return elementsIds;
    }
}
