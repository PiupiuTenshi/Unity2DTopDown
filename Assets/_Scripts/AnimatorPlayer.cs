using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    private static readonly int IS_WALKING = Animator.StringToHash("IsWalking");
    private static readonly int LAST_INPUT_X = Animator.StringToHash("LastInputX");
    private static readonly int LAST_INPUT_Y = Animator.StringToHash("LastInputY");
    private static readonly int INPUT_X = Animator.StringToHash("InputX");
    private static readonly int INPUT_Y = Animator.StringToHash("InputY");
    [SerializeField] private Player player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        bool isWalking = player.IsWalking();
        Vector2 moveDir = player.GetMoveDir();
        animator.SetBool(IS_WALKING, isWalking);
        
        if (player.IsWalking())
        {
            animator.SetFloat(INPUT_X, moveDir.x);
            animator.SetFloat(INPUT_Y, moveDir.y);
            if (moveDir != Vector2.zero)
            {
                animator.SetFloat(LAST_INPUT_X, moveDir.x);
                animator.SetFloat(LAST_INPUT_Y, moveDir.y);
            }
        }
        else
        {
            // not walking, don't do anything about last input x and y
        }
    }
}
