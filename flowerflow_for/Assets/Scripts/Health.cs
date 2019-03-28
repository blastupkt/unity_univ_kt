using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int hp = 1;
    //public GameObject explosion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Flower")
        {
            Damage(1);
        }
    }

    public void Damage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            GameManager.instance.OffCamera();
            Destroy(gameObject);
            

        }
    }
}
