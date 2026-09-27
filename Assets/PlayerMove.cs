    using Unity.VisualScripting.FullSerializer;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerMove : MonoBehaviour
    {
        [Header("Ground List")]
        public LayerMask GroundLayer;
        [Header("HP")]
        public float hp;
        [Header("Speed Stats")]
        public float speed;
        [Header("Jump Stats")]
        public float jumppower;
        public float Cooldown_Jump_Time;
        [Header("Dash Stats")]
        public float DashSpeedMuliflier;
        public float Cooldown_Dash_Time;
        

        float RealSpeed;
        float move;
        bool IsGrounded = false;
        SpriteRenderer spriteRenderer;
        Rigidbody2D rigid;
        BoxCollider2D boxcollider;
        Animator anime;

        //쿨타임 생성
        CoolTimer Jump;
        CoolTimer Dash;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxcollider = GetComponent<BoxCollider2D>();
            anime = GetComponent<Animator>();

            //쿨타임 설정
            Jump = new CoolTimer(Cooldown_Jump_Time);
            Dash = new CoolTimer(Cooldown_Dash_Time);

            //실제 속도값 설정
            RealSpeed = speed;

        }

        //이동
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

        //대쉬
        void OnDash(InputValue value)
        {
        if(!anime.GetBool("IsDefense")){
            if(value.isPressed&&!Dash.Cooldown)
            {
                anime.SetTrigger("IsDash");
                RealSpeed = speed * DashSpeedMuliflier;
                Dash.Cooldown = true;
            }
            else
            {
                RealSpeed = speed;
            }
        }
        }

        //수비
        void OnDefense(InputValue value)
        {
            if(value.isPressed)
            {
                anime.SetBool("IsDefense", true);
                RealSpeed = speed/5f;
            }
            else
            {
                anime.SetBool("IsDefense", false);
                RealSpeed = speed;
            }
        }

        //점프
        void OnJump(InputValue value)
        {
            if (value.isPressed && !Jump.Cooldown && IsGrounded)
            {
                rigid.AddForce(Vector2.up*jumppower,ForceMode2D.Impulse);
                Jump.Cooldown = true;
            }
        }

        void FixedUpdate()
        {
            //이동 값 적용
            rigid.linearVelocityX = move * RealSpeed;
            anime.SetFloat("CurrentSpeed",RealSpeed/5);


            //IsGrounded 확인 
            RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + Vector2.down * boxcollider.bounds.extents.y, new Vector2(boxcollider.bounds.size.x-0.1f, 0.1f), 0f, Vector2.down, 0.1f, GroundLayer);
            IsGrounded = hit.collider != null && hit.normal.y > 0f;

            //중력보정
            if(IsGrounded)
            {
                rigid.gravityScale = 0f;
            }   
            else
            {
                rigid.gravityScale = 3f; 
            }


            //경사로 처리
            if (IsGrounded && !Jump.Cooldown && rigid.linearVelocityY > 0.1f)
            {
                rigid.linearVelocityY = 1f;
            }


            //쿨타임    
            Jump.Cooltimer();
            Dash.Cooltimer();
        }

    public void damage(int damage)
        {
            hp -= damage;
            Debug.Log(hp);
        }
    }
