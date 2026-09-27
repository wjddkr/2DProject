using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    BehaviorGraphAgent behaviorAgent;
    Rigidbody2D rigid;
    Animator Anime;

    [SerializeField] Transform pos;
    [SerializeField] Vector2 boxsize;

    void Start()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        rigid = GetComponent<Rigidbody2D>();
        Anime = GetComponent<Animator>();

        InvokeRepeating("attack",1f,3f);
    }

    void FixedUpdate()
    {

    }

    void attack()
    {
        Anime.SetTrigger("Attack");
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0f);
        foreach(Collider2D hit in collider2Ds)
        {
            if(hit.gameObject.name == "Wolf")
            {
                hit.GetComponent<PlayerMove>().damage(1);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }
}
