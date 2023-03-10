using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Card
{
    public Minotaur()
    {
        Name = "Minotaur";
        Color = "Red";
    }
    public override void DoAction(Hex hex)
    {
        if (hexMap == null)
        {
            Debug.Log("no map");
        }
        hexMap.SpawnUnitAt(new MinotaurUnit(), hexMap.Minotaur, hex.Column, hex.Row);

    }
}
