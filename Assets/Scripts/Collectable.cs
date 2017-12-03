using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public GameObject scoreText;
    public double chance = 1;
    public int points = 1;

	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish") && gameObject.name.Contains("Fish"))
        {
            CollectableSystem.instance.Regenerate1(1);
            CollectableSystem.instance.usedLocations.Remove(transform.parent);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            //Debug.Log(gameObject.name);
            PickUp();
        }
    }

    public void PickUp()
    {
        Camera.main.GetComponent<CameraMovement>().PlayBubblesSound();
        GameObject GO = Instantiate(scoreText, GameObject.FindGameObjectWithTag("Player").transform);
        StartCoroutine(GO.GetComponent<ScoreParticle>().Spawn(points));
        CollectableSystem.instance.UpdateScore(points);
        if (Random.value < chance)
        {
            if(Random.value < .3f)
                CollectableSystem.instance.RegeneratePearls(1);
            else
                CollectableSystem.instance.Regenerate1(1);
            CollectableSystem.instance.usedLocations.Remove(transform.parent);
        }
        Destroy(gameObject);
    }
}
