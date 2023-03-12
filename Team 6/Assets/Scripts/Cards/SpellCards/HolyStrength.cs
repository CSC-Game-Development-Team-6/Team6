using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyStrength : Card
{

    public HolyStrength()
    {
        Name = "Holy Strength";
    }

    public override void DoAction(Hex hex)
    {
        Unit target = hex.Units()[0];
        if (target.Color == "Black")
        {
            target.HitPoints -= 2;
            target.Strength -= 1;
            Debug.Log(target.Name);
        }
        else
        {
            Debug.Log(target.Name);
            target.heal(2);
            target.Strength += 1;
        }
    }
}
