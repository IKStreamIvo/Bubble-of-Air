using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectable : Collectable {

    Rigidbody2D rb;
    public Vector2 velocity;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(velocity.x, velocity.y), 0f);
        if(rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
	}
	
	void Update () {
		
	}
}
