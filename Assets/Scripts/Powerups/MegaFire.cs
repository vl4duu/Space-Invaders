using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MegaFire : Buff
{
    public float speedRate = 5;
    public float sizeRate = 5;

    public override bool ApplyBuff(Collider2D player)
    {
        GameObject playerBullet = player.GetComponent<Shoot>().bullet;
        player.GetComponent<Shoot>().bulletSpeedMultiplier *= speedRate;
        player.GetComponent<Shoot>().bulletSizeMultiplier *= this.sizeRate;
        return true;
    }

    public override bool Debuff(Collider2D player)
    {
        GameObject playerBullet = player.GetComponent<Shoot>().bullet;
        player.GetComponent<Shoot>().bulletSpeedMultiplier /= speedRate;
        player.GetComponent<Shoot>().bulletSizeMultiplier /= this.sizeRate;
        return false;
    }
}