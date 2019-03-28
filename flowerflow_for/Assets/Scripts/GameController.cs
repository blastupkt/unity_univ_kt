using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public float m_speed = 5f;
    
    Rigidbody2D m_rigid;
    float screenTop, screenBottom, screenLeft, screenRight;
    void Awake()
    {
        m_rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float g_x = Input.acceleration.x * m_speed;
        float g_y = Input.acceleration.y * m_speed;
        Physics2D.gravity = new Vector2(g_x, g_y);
        // Vector3 dir = Vector3.zero;
        // dir.x = -Input.acceleration.y;
        // dir.z = Input.acceleration.x;
        // if (dir.sqrMagnitude > 1)
        //     dir.Normalize();

        // dir *= Time.deltaTime;
        // transform.Translate(dir * m_speed);
        //플레이어의 화면이탈 방지
    }
    void FixedUpdate(){
       
        CheckOutOfScreen();
    }
    void CheckInput(){
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");
    }

        // Use this for initialization
        void Start () {
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }
    void CheckOutOfScreen()
    {
        float nextX = Mathf.Clamp(transform.position.x, screenLeft, screenRight);
        float nextY = Mathf.Clamp(transform.position.y, screenBottom, screenTop);

        transform.position = new Vector3(nextX, nextY, transform.position.z);
    }
    // Update is called once per frame

}
