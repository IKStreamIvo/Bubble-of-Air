using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public AudioSource sound;
    public Transform target;
    public float speed = 2f;
    public Vector4 clamping;

	void Start () {
		
	}
	
	void FixedUpdate () {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime * speed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.4f, 8.4f), Mathf.Clamp(transform.position.y, -7.6f, 2.5f), transform.position.z);
        }
    }

    public void PlayBubblesSound()
    {
        sound.Play();
    }
}
