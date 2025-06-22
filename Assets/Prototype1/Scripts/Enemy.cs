using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int scoreValue = 10;

    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;

    [System.Obsolete]
    void Start() 
    { 
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update() 
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * speed); 

        if(transform.position.y < -10)
        {
            gameManager.UpdateScore(scoreValue);
            Destroy(gameObject);
        }
    }
   
}
