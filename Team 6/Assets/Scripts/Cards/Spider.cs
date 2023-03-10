using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spider : Card
{
    public Spider()
    {
        Name = "Spider";
        Color = "Black";
    }
    Random random = new Random();

    public override void DoAction(Hex hex)
    {
        hexMap.SpawnUnitAt(new SpiderUnit(), hexMap.spiders[random.Next() % 2], hex.Column, hex.Row);

    }
}