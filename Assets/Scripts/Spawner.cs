using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spwanPoint;

    int level;
    float timer;

    void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        // 타이머변수에 deltaTime을 계속 더하기
        timer += Time.deltaTime;
        // 적절한 숫자로 나누어 시간에 맞춰 레벨이 올라가도록 작성
        // FloorToInt : 소수점 아래를 버리고 Int 형으로 바꾸는 함수 <-> CeilToInt : 소수점 아래를 올리리고 Int 형으로 바꾸는 함수
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

        if (timer > (level == 0 ? 0.5 : 0.2f))
        {
            // 타이머가 일정 값에 도달하면 소환하도록 작성
            timer = 0;
            Spwan();
        }

    }

    void Spwan()
    {
        GameObject enemy = GameManager.instance.pool.Get(level);
        enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
    }
}
