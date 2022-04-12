using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifes = 3;
    bool _laserActive;
    

    public System.Action dead;


    private void Update()
    {
        if (lifes <= 0)
        {
            Destroy(this.gameObject);
        }
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.position.x >= (leftEdge.x + 1f))
            {
                this.GetComponent<Movement>().MoveLeft();
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.position.x <= (rightEdge.x - 1f))
            {
                this.GetComponent<Movement>().MoveRight();
            }
        }

        // Shooting
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!_laserActive)
        {
            Projectile projectile = this.GetComponent<Shoot>().Fire();
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            this.lifes -= 1;
            Debug.Log(this.lifes + " lifes left");
        }
    }
}