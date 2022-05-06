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
    public GameObject explosion;
    [HideInInspector] public GameObject GameOverScreen;
    [HideInInspector] public bool deathTrigger;
    [HideInInspector] public bool isGamePaused;
    [HideInInspector] public float acceleration = 50f;
    public GameObject scoreManager;
    public PlayerScores leaderboard;

    private void Start()
    {
        leaderboard = gameObject.GetComponent<PlayerScores>();
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
        int? previousScore = leaderboard.GetPlayerScore(PlayerScores.currentPlayer);
        if (score > previousScore)
        {
            leaderboard.UpdatePlayerScore(score);
            leaderboard.SaveLeaderboard();
        }
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        GameOverScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}