using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardDataBundle", menuName = "Create CardDataBundle", order = 0)]
public class CardDataBundle : ScriptableObject
{

    [SerializeField] private CardData[] _cardsData;

    public CardData[] CardsData => _cardsData;
}
