using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour {

    public Vector3 spawn1;
    public Vector3 spawn2;

    Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = spawn1;
        rb.velocity = new Vector2(0f, .5f);
        transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        one = true;
	}

    bool one;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            if (one)
            {
                transform.position = spawn2;
                rb.velocity = new Vector2(0f, -.5f);
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                one = false;
            }
            else
            {
                transform.position = spawn1;
                rb.velocity = new Vector2(0f, .5f);
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                one = true;
            }
        }
    }
}
