using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int playerspeed = 10;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 10f;
    private float dirY = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();

    }

    private enum MovementState {down, right, up, left, downidle, rightidle, upidle, leftidle };
    private void PlayerMove()
    {

        MovementState state = MovementState.downidle;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(dirX * Time.deltaTime, 0));
            state = MovementState.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-dirX * Time.deltaTime, 0));
            state = MovementState.left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector2(0, dirY  * Time.deltaTime));
            state = MovementState.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector2(0, -dirY * Time.deltaTime));
            state = MovementState.down;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            state = MovementState.rightidle;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            state = MovementState.leftidle;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            state = MovementState.upidle;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            state = MovementState.downidle;
        }

        anim.SetInteger("state", (int)state);
    }
    
/*    private enum MovementState {down, right, up, left};
    private void UpdateAnimationUpdate()
    {
        MovementState state = MovementState.down;

        if(dirX > 10f)
        {
            state = MovementState.right;
        }
        else if (dirX < 10f)
        {
            state = MovementState.left;
        }
        else if (dirY > 10f)
        {
            state = MovementState.up;
        }
        else if (dirY < 10f)
        {
            state = MovementState.down;
        }

        anim.SetInteger("state", (int)state);
    }*/
}
