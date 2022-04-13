using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MovementBuff : Buff
{
    public float rate = 1;

    public override bool ApplyBuff(Collider2D player)
    {
        player.gameObject.GetComponent<Movement>().speed *= rate;
        return true;
    }

    public override bool Debuff(Collider2D player)
    {
        player.gameObject.GetComponent<Movement>().speed /= rate;
        return false;
    }
}