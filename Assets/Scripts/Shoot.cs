using Unity.Mathematics;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float bulletSpeedMultiplier = 1f;
    public float bulletSizeMultiplier = 1f;
    public GameObject bullet;
    private GameObject clone;
    [HideInInspector]
    public bool isGamePaused;

    public GameObject Clone
    {
        get { return clone; }
    }


    public Projectile Fire()
    {
        if(!isGamePaused)
        {
            clone = Instantiate(bullet, this.transform.position, quaternion.identity);
            clone.GetComponent<Projectile>().speed *= bulletSpeedMultiplier;
            clone.transform.localScale *= bulletSizeMultiplier;
            return clone.GetComponent<Projectile>();
        }
        else
        {
            return null;
        }

        
    }
}
