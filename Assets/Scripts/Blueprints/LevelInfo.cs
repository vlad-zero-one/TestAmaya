using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private LevelInfoUnit[] _levelInfoUnits;
    public LevelInfoUnit[] LevelInfoUnits => _levelInfoUnits;
}


[System.Serializable]
public struct LevelInfoUnit
{
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    public int Rows => _rows;
    public int Columns => _columns;
}