using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUnit : Unit
{
    public KnightUnit()
    {
        this.MaxHitPoints = this.HitPoints = 4;
        this.Strenth = 1;
        this.Movement = 1;
    }

}