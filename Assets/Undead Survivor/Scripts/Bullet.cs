using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 데미지와 관통 변수 선언
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 변수 초기화 함수 작성
    // 초기화 함수에 속도 관련 매개변수 추가가
    public void Init(float damage, int per, Vector3 dir)
    {
        // this : 해당 클래스의 변수로 접근
        this.damage = damage;
        this.per = per;

        // 관통이 -1(무한)보다 큰 것에 대해서는 속도 적용
        if (per > -1)
        {
            rigid.velocity = dir * 15f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 관통 로직 이전에 조건 추가
        // || (OR) 혹은, 좌측 우측 중 하나만 true면 결과는 true
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        // 관통값이 하나씩 줄어들면서 -1이 되면 비활성화
        per--;

        if (per == -1)
        {
            // 비활성화 이전에 미리 물리 속도 초기화
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}

