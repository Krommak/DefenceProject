using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RuntimeData
{
    internal PlayField PlayField;

    internal Node[,] StandartField => PlayField.StandartField;
}

public class PlayField
{
    internal Node[,] StandartField { get; set; }
    internal Dictionary<int, Node[,]> fieldsByPlayerNum = new Dictionary<int, Node[,]>();

    internal PlayField(Vector2Int size)
    {
        StandartField = new Node[size.x, size.y];
    }

    internal Node[,] GetFieldsForPlayer(int playerIndex)
    {
        if (fieldsByPlayerNum.Keys.Contains(playerIndex))
            return fieldsByPlayerNum[playerIndex];
        else
        {
            Node[,] copy = StandartField.Clone() as Node[,];
            fieldsByPlayerNum.Add(playerIndex, copy);
        }
        return fieldsByPlayerNum[playerIndex];
    }

    internal Vector2Int GetFieldSize => new Vector2Int(StandartField.GetLength(0) - 1, StandartField.GetLength(1) - 1);
}
