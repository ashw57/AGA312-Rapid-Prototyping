using Prototype3;
using UnityEngine;
using UnityEngine.EventSystems;


public class Target : GameBehaviour
{
    [Header("Basics")]
    public TargetType myType;

    private float mySpeed = 5;
    private int myScore;

    private Transform bounce;
    private float bounceSpeed = 5;

    private Transform moveToPos;
    private TargetState myState;
    private Vector3 moveDirection = Vector3.forward;

    public int MyScore => myScore;

    private void Update()
    {
        if (myState == TargetState.Moving)
        {
            transform.Translate(moveDirection * mySpeed * Time.deltaTime);
        }
    }
    public void Initialize(Transform _startPos, string _name)
    {
        transform.position = _startPos.position;
        gameObject.name = _name;


        switch (myType)
        {
            case TargetType.Slow:
                mySpeed = 1;
                myScore = 5;
                break;

            case TargetType.Average:
                mySpeed = 2;
                myScore = 10;
                break;

            case TargetType.Fast:
                mySpeed = 3;
                myScore = 15;
                break;

            default:
                mySpeed = 1;
                myScore = 10;
                break;
        }

        myState = TargetState.Moving;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection = -moveDirection;
        }
    }
}
