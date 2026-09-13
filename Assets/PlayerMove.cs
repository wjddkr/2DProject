using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public bool IsJumping;
    public float jumppower;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Vector2 move;
    BoxCollider2D boxcollider;
    Animator anime;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
        anime = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
        move.x = Mathf.Abs(move.x) <= 0.01f ? 0f : move.x;

        if (move.x != 0)
        {
            spriteRenderer.flipX = move.x > 0;
            anime.SetBool("IsWalk", true);
        }
        else
            anime.SetBool("IsWalk",false);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && IsJumping == false)
        {
            rigid.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
            IsJumping = true;
        }
    }

    //다단점프 방지
    void OnCollisionEnter2D(Collision2D collision)
    {
    foreach (ContactPoint2D contact in collision.contacts){
        //바닥 인지 확인
            bool IsFloor = collision.gameObject.CompareTag("Floor");

            if(contact.normal.y >= 0.7f&&IsFloor&&rigid.linearVelocityY <= 0)
            IsJumping = false;
            break;
    }
}


    
    void FixedUpdate()
    {
        //이동 값 적용
        rigid.linearVelocity = new Vector2(move.x * speed, rigid.linearVelocityY);

        //중력보정
     if (rigid.linearVelocityY < 0)
        rigid.gravityScale = 2f; 
    else
        rigid.gravityScale = 1f;

    }
}
