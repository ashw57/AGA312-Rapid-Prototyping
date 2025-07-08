using System;
using System.Diagnostics;
using Prototype2;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float lifetime = 5f;
    private int scoreValue = 10;

    private GameManager gameManager;

    public int ScoreValue { get => ScoreValue1; set => ScoreValue1 = value; }
    public int ScoreValue1 { get => scoreValue; set => scoreValue = value; }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (gameManager != null)
        {
            gameManager.UpdateScore(scoreValue);
        }
    }       
}
