using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KnownBundles : ScriptableObject
{
    [SerializeField] private CardDataBundle[] _value;

    public CardDataBundle[] Value => _value;
}