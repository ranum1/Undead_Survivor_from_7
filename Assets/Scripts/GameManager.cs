using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // static : 정적으로 사용하겠다는 키워드. 바로 메모리에 얹어버림
    // static으로 선언된 변수는 인스펙터에 나타나지 않음
    // 정적 변수는 즉시 클래스에서 부를 수 있다는 편리함이 존재
    public static GameManager instance;


    public float gameTime;
    public float maxGameTime;

    public PoolManager pool;
    public Player player;

    void Awake()
    {
        // 인스턴스 변수를 가진 자기자신 this로 초기화
        instance = this;
    }

    void Update()
    {
        // 타이머변수에 deltaTime을 계속 더하기기
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            // 타이머가 일정 값에 도달하면 소환하도록 작성
            gameTime = maxGameTime;
        }

    }
}
