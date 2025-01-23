using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //이름은 데이터가  지닌 의미를 파악할 수 있도록 짓기
    //public : 다른 스크립트에게 '공개한다'라고 선언하는 키워드
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    //시작할 때 한번만 실행되는 생명주기 Awake에 실행
    void Awake()
    {
        //GetComponent<> : 오브젝트에서 컴포넌트를 가져오는 함수
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame



    //FixedUpdate : 물리 연산 프레임마다 호출되는 생명주기 함수
    void FixedUpdate()
    {
        // normalized : 벡터 값의 크기가 1이 되도록 좌표가 수정된 값
        // fixedDeltaTime : 물리 프레임 하나가 소비한 시간(FixedUpdate에서 사용)
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        // 위치 이동 MovePosition
        // MovePosition은 위치 이동이라 현재 위치도 더해주어야함
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        // Get<> : 프로필에서 설정한 컨트롤 타입 값을 가져오는 함수
        inputVec = value.Get<Vector2>();
    }

    // 프레임이 종료 되기 전 실행되는 생명주기 함수
    void LateUpdate()
    {
        // magnitude : 벡터의 순수한 크기 값값
        anim.SetFloat("Speed", inputVec.magnitude);

        // != : '왼쪽과 오른쪽이 서로 다릅니까?' 의미의 비교 연산자
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
