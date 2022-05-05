using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGenerator : MonoBehaviour
{
    public Powerup[] powerUps;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(DropPowerup), 1, 3);
    }

    private void DropPowerup()
    {
        float spawnY = 16f;
        int powerType = Random.Range(0, (this.powerUps.Length));
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(this.powerUps[powerType], spawnPosition, Quaternion.identity);
    }
}