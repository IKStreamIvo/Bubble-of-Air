using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-10, 90));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
