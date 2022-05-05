using System;
using System.Collections;
using System.Collections.Generic;
using Animations.Console_Menu;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifes = 3;
    public int score;
    bool _laserActive;
    public PlayerScores scores;
    public GameObject explosion;
    public GameObject GameOverScreen;
    public bool deathTrigger;
    [HideInInspector] public bool isGamePaused;
    [HideInInspector] public float acceleration = 50f;

    private void Start()
    {
        scores = new PlayerScores();
    }
    
    
    
    private void Update()
    {


        //Death
        if (lifes <= 0 && !deathTrigger)
        {
            deathTrigger = true;
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            StartCoroutine(OnDeath());
        }


        // Movement
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        if ((Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && this.transform.position.x >= (leftEdge.x + 1f) )
        {
            this.GetComponent<Movement>().MoveLeft();
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x <= (rightEdge.x - 1f))
        {
            this.GetComponent<Movement>().MoveRight();
        }


        // Shooting
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (!isGamePaused)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (!_laserActive)
        {
            Projectile projectile = this.GetComponent<Shoot>().Fire();
            projectile.hit += IncrementScore;
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void IncrementScore()
    {
        this.score += 1;
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

    private IEnumerator OnDeath()
    {
        Debug.Log("OnDeathStarted");
        scores.SaveScores();
        scores.UpdateScore(score);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        GameOverScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}