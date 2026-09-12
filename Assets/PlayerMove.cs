using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jumppower;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Vector2 move;
    Animator anime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }
    void OnMove(InputValue Value)
    {
        //애니메이션 걷기/스탠딩 변경
        move = Value.Get<Vector2>();
        move.x = Mathf.Abs(move.x) <= 0.01f ? 0f : move.x;

        if (move.x != 0){
        spriteRenderer.flipX = move.x<0;
        anime.SetBool("IsWalk", true);}

        else{
        anime.SetBool("IsWalk", false);}
    }

    void OnJump(InputValue value)
    {
        rigid.AddForce(Vector2.up*jumppower, ForceMode2D.Impulse);
        
    }

    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(move.x*speed, rigid.linearVelocityY);

        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position,Vector3.down, 1);
    }
}