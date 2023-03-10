using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrizzlyBears : Card
{
    
    public GrizzlyBears()
    {
        Name = "Grizzly Bears";
        Color = "Green";
    }

    public override void DoAction(Hex hex)
    {
        GameComponent.SpawnUnitAt(new BearUnit(), GameComponent.GrizzlyBears, hex.Column, hex.Row);
    }
}
