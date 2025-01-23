using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    // Collider2D 변수 생성 및 초기화
    Collider2D coll;

    void Awake()
    {
        // Collider2D는 기본 도형의 모든 콜라이더2D를 포함함
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        // OnTriggerExit2D의 매개변수 상대방 콜라이더의 테그를 조건으로로
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        // 플레이어 위치 - 타일맵 위치 계산으로 거리 구하기
        // Mathf.Abs : 절댓값 함수수
        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        // 플레이어의 이동 방향을 저장하기 위한 변수 추가
        Vector3 playerDir = GameManager.instance.player.inputVec;
        // 3항 연산자 (조건) ? (true일 때 값) : (false일 때 값)
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        // switch ~ case : 값의 상태에 따라 로직을 나눠줌 
        switch (transform.tag)
        {
            case "Ground":
                // 두 오브잭트의 거리 차이에서, X축이 Y축보다 크면 수평 이동동
                if (diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                if (diffx < diffy)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":
                if (coll.enabled)
                {
                    // 플레이어의 이동방향에 따라 맞은 편에서 등장하도록 이동
                    // 랜덤한 위치에서 등장하도록 벡터더하기
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }

    }

}
