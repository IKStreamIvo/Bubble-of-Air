using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountScore : MonoBehaviour {

    static TextMeshProUGUI text;
    
	void Start () {
        text = GetComponent<TextMeshProUGUI>();
    }

    public static IEnumerator Activate(int amount)
    {
        //yield return new WaitForSeconds(1f);
        for (int i = 1; i <= amount; i++)
        {
            yield return new WaitForSeconds(.1f);
            text.SetText(i.ToString());
        }
        yield return new WaitForSeconds(.1f);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
