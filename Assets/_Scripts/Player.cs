using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private bool isWalking;
    private Vector2 moveDir;

    private void Awake()
    {   
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        HandlerMovement();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+moveDir*speed*Time.deltaTime);
    }

    private void HandlerMovement()
    {
        Vector2 inputVector = gameInput.GetMovementNormalize();
        moveDir = inputVector;
        isWalking = moveDir != Vector2.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public Vector2 GetMoveDir()
    {
        return moveDir;
    }
}
