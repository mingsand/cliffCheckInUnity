using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed = 5f;
    public bool doesJump = false;
    public int playerDirection = 1;
    public bool isCliff = false;



    
    void Awake()
    {
        Application.targetFrameRate = 60;
        rigid = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(h * speed, rigid.velocity.y);

        rigid.velocity = move;
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "platform")
        {
            doesJump = false;
        }        
    }



    void Update()
    {
        if (Input.GetKeyDown("space") && doesJump == false)
        {
            rigid.AddForce(new Vector2(0f, 20f), ForceMode2D.Impulse);
            doesJump = true;
        }

        if (rigid.velocity.x > 0)  // 낭 떨어지
        {
            playerDirection = 1;
        }
        else if (rigid.velocity.x < 0)
        {
            playerDirection = -1;
        }
        
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("platform")); // 플랫폼에 레이캐스트 쏘기
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green);

        if (groundHit.collider != null) // 땅 위에 있을 때만 낭떨어지 체크 실행
        {
            if (playerDirection == 1) // 방향에 따라 레이캐스트 쏘기
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y),
                Vector2.down, 1f, LayerMask.GetMask("platform")); // 앞에 레이캐스트
                Debug.DrawRay(new Vector2(transform.position.x + 1, transform.position.y), Vector2.down * 1f, Color.red); 

                if (hit.collider == null)
                {
                    Debug.Log("앞에 낭떨어지"); // 이곳에 낭떨어지 앞에서 실행할 idle 애니메이션이라던가 그런 거 넣어도 괜찮을 듯
                }
            }
            else if (playerDirection == -1)
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y),
                Vector2.down, 1f, LayerMask.GetMask("platform"));
                Debug.DrawRay(new Vector2(transform.position.x - 1, transform.position.y), Vector2.down * 1f, Color.red);

                if (hit.collider == null)
                {
                    Debug.Log("앞에 낭떨어지");
                }
            }
            
        }





    }
}
