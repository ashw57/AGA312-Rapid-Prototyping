using UnityEngine;

public class Playground : GameBehaviour
{
    public GameObject player;

    void Start()
    {
        ObjectX.ScaleObjectToZero(player);
        ExecuteAfterSeconds(1, () =>
        {
            SetupPlayer();
        });

        ExecuteAfterFrames(3, () => print("3 frames later..."));
    }

    private void SetupPlayer()
    {
        player.GetComponent<Renderer>().material.color = ColorX.GetRandomColour();
        ObjectX.ScaleObjectToValue(player);
    }

    private void OnJump()
    {
        print("Jumped");
    }
    

    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space))
       //     player.GetComponent<Renderer>().material.color = ColorX.GetRandomColour();    
    }

    private void OnMove(Vector2 _moveVector)
    {
        print(_moveVector);
        player.transform.position += new Vector3(_moveVector.x, 0, _moveVector.y);
    }

    private void OnEnable()
    {
        InputManager.OnMove += OnMove;
        InputManager.OnJump += OnJump;
    }

    private void OnDisable()
    {
        InputManager.OnMove -= OnMove;
    }
}
