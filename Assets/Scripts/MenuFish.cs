using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFish : MonoBehaviour {
    public float speed;

    Vector3 startpos;

    private void Start()
    {
        startpos = transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(speed - .2f, speed + .2f), 0f);
        if (speed < 0f)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            transform.position = startpos;
        }
    }
}
