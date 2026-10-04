using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Profile;
using UnityEngine;
using UnityEngine.UIElements;

public class Mob : MonoBehaviour
{

    BehaviorGraphAgent behaviorAgent;
    Rigidbody2D rigid;
    Animator Anime;

    List<Collider2D> hitted = new List<Collider2D>();

    [SerializeField] Transform pos;
    [SerializeField] Vector2 boxsize;

    void Start()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        rigid = GetComponent<Rigidbody2D>();
        Anime = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

    }

    public void NormalAttack1()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxsize, 0f);
        foreach(Collider2D hit in collider2Ds)
        {
                if(hit.tag == "Player")
                {

                    if(PlayerMove.Instance.Defensing() && !hitted.Contains(hit))
                    {
                        hitted.Add(hit);
                    }
                    else if(!hitted.Contains(hit))
                    {
                        PlayerMove.Instance.Stat.Damage(30);
                        hitted.Add(hit);
                    }
                }
        }
    }

    public void EndingAttack()
    {
        hitted.Clear();
    }
    

    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxsize);
    }
}
