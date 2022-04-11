using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool _laserActive;
    public Projectile laserPrefab;
    public float speed = 5.0f;

    public System.Action dead;


    private void Update()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.position.x >= (leftEdge.x + 1f))
            {
                this.transform.position += Vector3.left * this.speed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.position.x <= (rightEdge.x - 1f))
            {
                this.transform.position -= Vector3.left * this.speed * Time.deltaTime;
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
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
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
            // Debug.Log("Dead!");
            // Debug.Log("!"+ this.dead.GetType().ToString());
            // this.dead.Invoke();        
            this.gameObject.SetActive(false);
        }
    }
}