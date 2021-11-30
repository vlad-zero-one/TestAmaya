using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

    public int Id => _id;
    public string Name => _name;
    public Sprite Sprite => _sprite;
}
