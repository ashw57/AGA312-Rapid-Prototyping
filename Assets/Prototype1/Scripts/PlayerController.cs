using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;

    void Start() { 
        playerRb = GetComponent<Rigidbody>(); }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
