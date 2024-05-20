using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private int score = 0;

    // public float ballDamage { get; set; }
    // public float ballCount { get; set; }
    // public float ballSize { get; set; }

    Rigidbody2D rb;
    Vector2 lastVelocity;

    RankData rankData = new RankData();
    ScoreData scoreData = new ScoreData();
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        First_Bounce();

        //어딘가에서 시작할 때
        // scoreData.LoadTool("bestScore", rankData);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    void First_Bounce()
    {
        this.transform.position = new Vector2(0, 0);
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(x * speed, y * speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        var nall = other.gameObject.GetComponent<Icollidable>();
        if (nall != null)
        {
            nall.OnCollide(this.gameObject);
        }

        //magnitude 는 벡터의 길이(거리)를 반환함
        var speed = lastVelocity.magnitude;
        // nomalized 는 크기가 1인 단위벡터를 받아옴. 정규화 방향을 추구함.
        // contacts[0] 은 두 물체 사이의 여러 충돌 지점 중 첫 번째지점의 정보를 가져옴.
        // other.contacts[0].normal > 0이면 위를향하고, < 0 이면 아래를 향함, 0~1 사이에 각도로 차이가있음.
        // Reflect == 지정된 법선이 있는 표면에서 벡터 반사를 반환함.
        // Vector2 Reflect (System.Numerics.Vector2 vector, System.Numerics.Vector2 normal);
        // .normalized은 원본 벡터 ,.noraml은 반사되는 표면의 법선 그리고 반환은 반사벡터가 반환됌
        // 즉 dir이 반사벡터를 대입받음
        var dir = Vector2.Reflect(lastVelocity.normalized, other.contacts[0].normal);
        //  Mathf.Max는 괄호 안에 a 와 b 중에 더 큰 값을 반환합니다.
        rb.velocity = dir * Mathf.Max(speed, 0f);

        score++;

        if (rankData.bestScore < score)
        {
            rankData.bestScore = score;
        }

        // 게임오버시
        // if(게임오버)
        // {
        //     scoreData.SaveTool("bestScore", rankData);
        // }
        //해당 블럭을 부딫치면서 점수를 낼 경우
        // if(other.collider.CompareTag("block"))
        // {
        //     블럭의 수명 -= ballDamage;
        //     if(블럭의 수명 <= 0)
        //     {
        //         Destroy(블럭);
        //         score += 높은점수;
        //     }
        //     else
        //     {
        //         score++;
        //     }
        //     if(rankJson.bestScore < score)
        //     {
        //         rankJson.bestScore = score;
        //     }
        // }
    }
}