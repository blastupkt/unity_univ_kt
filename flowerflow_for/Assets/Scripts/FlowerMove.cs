﻿using UnityEngine;
using System.Collections;

public class FlowerMove : MonoBehaviour
{
    public float m_speed = 5f;
    public Vector2 m_direction;
    public bool m_isUsed = false;
    bool onScreen = false; //화면에 들어왔는가?
    bool onScreenOut = false; //화면에서 나갔는가?
    Rigidbody2D m_rigid;

    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 value)
    {
        m_direction = value;
    }

    void Update()
    {
        if (!onScreen && FlowerManager.instance.IsInScreen(transform.position))
        {
            //화면밖에 있는 상황에서 화면 안으로 들어왔다면
            onScreen = true;
        }
        else if (onScreen && !FlowerManager.instance.IsInScreen(transform.position))
        {
            //화면안에 있는 상황에서 화면 밖으로 나갔다면
            onScreenOut = true;
            m_isUsed = false;
        }
    }

    void FixedUpdate()
    {
        if (!onScreenOut) //화면에 등장하고 나갈때까지 계속 총알이 진행됨.
            m_rigid.velocity = m_direction * m_speed;
    }

    //총알 리셋하기.
    public void SetPosition(Vector2 value)
    {
        transform.position = value;
        onScreen = false;
        onScreenOut = false;
    }
}
