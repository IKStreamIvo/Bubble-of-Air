using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

    static new Animation animation;

    private void Awake()
    {
        animation = GetComponent<Animation>();
    }

    public static IEnumerator FadeOut()
    {
        animation["ScreenFade"].time = animation["ScreenFade"].length;
        animation["ScreenFade"].speed = -1f;
        animation.Play("ScreenFade");
        yield return new WaitForSeconds(animation["ScreenFade"].length);
        //SceneManager.LoadScene(0);
    }

    public static void FadeIn()
    {
        animation["ScreenFade"].time = 0f;
        animation["ScreenFade"].speed = 1f;
        animation.Play("ScreenFade");
    }
}
