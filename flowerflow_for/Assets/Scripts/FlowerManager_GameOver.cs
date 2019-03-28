using UnityEngine;
using System.Collections.Generic;

public class FlowerManager_GameOver : MonoBehaviour {
    public static FlowerManager_GameOver instance;
    public GameObject[] game_over_FlowerPrefab;
    public Transform m_Player;
    public int maxFlower = 32;
    List<FlowerMove> FlowerList;
    Vector2 m_LeftBottom;
    Vector2 m_RightTop;
    void Awake()
    {
        if (instance)
        {
            Debug.Log("다중인스턴스 실행중입니다. 주의하세요");
        }
        instance = this;
    }

    void Start()
    {
        
        m_LeftBottom = new Vector3(0, 0, 0);
        m_RightTop = new Vector3(640, 360, 0);
        FlowerList = new List<FlowerMove>();
        for (int i = 0; i < maxFlower; i++)
        {
            GameObject temp = (GameObject)Instantiate(game_over_FlowerPrefab[Random.Range(0, game_over_FlowerPrefab.Length)], new Vector3(0, m_RightTop.y + 2, 0), Quaternion.identity);
            //총알을 생성하고
            FlowerList.Add(temp.GetComponent<FlowerMove>());
            //해당 총알의 FlowerMove 스크립트를 리스트에 넣는다.
        }
    }

    public void CreateFlower()
    {
        if (!m_Player) { return; }
        Vector2 pos = GetRandomPosition(); //랜덤한 위치를 받아와서
        Vector2 direction = (Vector2)m_Player.position - pos;

        FlowerMove selectedFlower = FlowerList.Find(o => o.m_isUsed == false);
        //현재 미사용중인 총알을 찾아서
        if (!selectedFlower)
        {
            Debug.Log("화면에 생성가능한 최대 총알수를 초과했습니다!, 최대 총알수를 늘려주세요");
        }
        else {
            //방향과 속도를 설정해준다
            selectedFlower.SetDirection(direction.normalized);
            selectedFlower.SetPosition(pos);
            selectedFlower.m_isUsed = true;
        }
    }

    Vector2 GetRandomPosition()
    {
        int caseNum = Random.Range(0, 4); //0~3까지의 랜덤한 숫자를 만들어냄.
        Vector2 pos = Vector2.zero;
        switch (caseNum)
        {
            case 0: //좌측
                pos.x = m_LeftBottom.x - 180;
                pos.y = Random.Range(m_LeftBottom.y, m_RightTop.y);
                break;
            case 1: //우측
                pos.x = m_RightTop.x + 320;
                pos.y = Random.Range(m_LeftBottom.y, m_RightTop.y);
                break;
            case 2: //상단
                pos.x = Random.Range(m_LeftBottom.x, m_RightTop.x);
                pos.y = m_RightTop.y + 1;
                break;
            case 3: //하단
                pos.x = Random.Range(m_LeftBottom.x, m_RightTop.x);
                pos.y = m_LeftBottom.y - 1;
                break;
        }
        return pos;
    }

    public bool IsInScreen(Vector2 target)
    {
        if (target.x > m_LeftBottom.x && target.x < m_RightTop.x && target.y > m_LeftBottom.y && target.y < m_RightTop.y)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
