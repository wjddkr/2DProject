using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Ground List")]
    public LayerMask GroundLayer;
    [Header("Speed Stats")]
    public float speed;
    [Header("Jump Stats")]
    public float jumppower;
    bool Cooldown_Jump = true;
    public float Cooldown_Jump_Time = 0.1f;
    

    
    float move;
    float Cooldown_Jump_Timer;
    bool IsGrounded = false;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    BoxCollider2D boxcollider;
    Animator anime;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
        anime = GetComponent<Animator>();


        Cooldown_Jump_Timer = Cooldown_Jump_Time;
    }

    void OnMove(InputValue value)
    {
        Vector2 InputX = value.Get<Vector2>();
        move = Mathf.Abs(InputX.x) <= 0.01f ? 0f : InputX.x;

        if (move != 0)
        {
            spriteRenderer.flipX = move > 0;
            anime.SetBool("IsWalk", true);
        }
        else
            anime.SetBool("IsWalk",false);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && Cooldown_Jump == false && IsGrounded)
        {
            rigid.AddForce(Vector2.up*jumppower,ForceMode2D.Impulse);
            Cooldown_Jump = true;
        }
    }

    void FixedUpdate()
    {
        //이동 값 적용
        rigid.linearVelocityX = move * speed;

        //중력보정

        Jump();

        if(IsGrounded)
        {
            rigid.gravityScale = 0f;
        }   
        else
        {
            rigid.gravityScale = 3f; 
        }


        //경사로 처리
        if (IsGrounded && !Cooldown_Jump && rigid.linearVelocityY > 0.1f)
        {
            rigid.linearVelocityY = 1f;
        }
        
    }


    //점프(쿨타임, 바닥체크)
    void Jump()
    {
        //점프 쿨다운
        if(Cooldown_Jump)
        {
            Cooldown_Jump_Timer -= Time.fixedDeltaTime;
            if(Cooldown_Jump_Timer < 0)
            {
                Cooldown_Jump_Timer = Cooldown_Jump_Time;
                Cooldown_Jump = false;
            }
        }
        //IsGrounded 확인 
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + Vector2.down * boxcollider.bounds.extents.y, new Vector2(boxcollider.bounds.size.x-0.05f, 0.1f), 0f, Vector2.down, 0.1f, GroundLayer);
        IsGrounded = hit.collider != null && hit.normal.y > 0f;
    }
}
