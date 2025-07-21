using Prototype3;
using UnityEngine;

public class Target : GameBehaviour
{
    [Header("Basics")]
    public TargetType myType;

    private float mySpeed = 5;
    private int myScore;

    private Transform bounce;

    public int MyScore => myScore;

    public void Initialize(Transform _startPos, string _name)
    {
        switch (myType)
        {
            case TargetType.Slow:
                mySpeed = 1;
                myScore = 10;
                break;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            
        }
    }
}
