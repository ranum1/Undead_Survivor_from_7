using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 배열 변수
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    void Awake()
    {

        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            // 내용물 오브젝트가 비활성화(대기상태)인지 확인인
            if (!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                // break 키워드로 반복문 종료
                break;
            }
        }

        // 못찾았으면?
        // 미리 선언한 변수가 계속 비어있으면 생성 로직으로 진입
        if (!select)
        {
            // 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
