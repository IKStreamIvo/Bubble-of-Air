using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreParticle : MonoBehaviour {

    TextMeshPro text;
    Animation anim;

	public IEnumerator Spawn(int points)
    {
        anim = GetComponent<Animation>();
        text = GetComponent<TextMeshPro>();
        if (points > 0)
            text.SetText("+" + points);
        else
            text.SetText(points.ToString());
        anim.Play("ScoreText");
        yield return new WaitForSeconds(anim["ScoreText"].length);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
