using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPositionsManager : MonoBehaviour
{
    [SerializeField] private LevelInfo _levelInfo;
    [SerializeField] private Transform _cellPrefab;

    private float prefabHeight;
    private float prefabWidth;

    public void Init()
    {
        prefabHeight = _cellPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        prefabWidth = _cellPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public Vector2[][,] GetPlaces()
    {
        var infoArray = _levelInfo.LevelInfoUnits;
        Vector2[][,] returnArray = new Vector2[infoArray.Length][,];
        for (int i = 0; i < infoArray.Length; i++)
        {
            returnArray[i] = new Vector2[infoArray[i].Rows, infoArray[i].Columns];

            int rows = returnArray[i].GetUpperBound(0) + 1;
            int columns = returnArray[i].Length / rows;

            float firstTopPointY;
            float firstTopPointX;
            if (rows == 1) firstTopPointY = 0;
            else firstTopPointY = prefabHeight * rows / 2 - prefabHeight / 2;
            if (columns == 1) firstTopPointX = 0;
            else firstTopPointX = -(prefabWidth * columns / 2 - prefabWidth / 2);

            for (int j = 0; j < rows; j++)
            {
                float currentY = firstTopPointY - j * prefabHeight;
                for (int k = 0; k < columns; k++)
                {
                    float currentX = firstTopPointX + k * prefabWidth;
                    returnArray[i][j, k] = new Vector2(currentX, currentY);
                }
            }
        }

        return returnArray;
    }
}
