using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableSystem : MonoBehaviour {

    public static CollectableSystem instance;

    public Transform pearlsParent;
    public TextMeshProUGUI text;
    public List<Transform> usedLocations;
    public GameObject fishPrefab;
    public double fishChance = .2;
    public GameObject trashPrefab;
    public double trashChance = .5;
    public GameObject pearlPrefab;
    public double pearlChance = .2f;

    public int score;

    private List<Transform> normalChilds;
    private List<Transform> pearlChilds;

    void Start()
    {
        instance = this;
        usedLocations = new List<Transform>();

        text.SetText(score.ToString());

        normalChilds = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            normalChilds.Add(transform.GetChild(i));
        }
        pearlChilds = new List<Transform>();
        for (int i = 0; i < pearlsParent.childCount; i++)
        {
            pearlChilds.Add(pearlsParent.GetChild(i));
        }

        Regenerate1(12);
        RegeneratePearls(5);
    }

    public void Regenerate1(int amount)
    {
        //Fish and trash
        for (int i = 0; i < amount; i++)
        {
            int count = 0;
            Transform slot = normalChilds[Random.Range(0, normalChilds.Count)];
            while (usedLocations.Contains(slot))
            {
                slot = normalChilds[Random.Range(0, normalChilds.Count)];
                count++;
                if (count >= 5)
                    return;
            }
            
            int item = Random.Range(0, 2);
            if (item == 0) //Fish
            {
                GameObject clone = Instantiate(fishPrefab, slot);
                usedLocations.Add(slot);
            }
            else if (item == 1) //Trash
            {
                GameObject clone = Instantiate(trashPrefab, slot);
                usedLocations.Add(slot);
            }
        }
    }
    public void RegeneratePearls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int count = 0;
            Transform slot = pearlChilds[Random.Range(0, pearlChilds.Count)];
            while (usedLocations.Contains(slot))
            {
                slot = pearlChilds[Random.Range(0, pearlChilds.Count)];
                count++;
                if (count >= 5)
                    return;
            }

            int item = Random.Range(0, 1);
            if (item == 0) //Pearls
            {
                GameObject clone = Instantiate(pearlPrefab, slot);
                usedLocations.Add(slot);
            }
        }
    }
    public void UpdateScore(int amount)
    {
        score += amount;
        if (score < 0)
            score = 0;

        text.SetText(score.ToString());
    }

    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";
    public static string descriptionParam;
    private string appStoreLink = "https://ikstreamivo.itch.io/";

    public void ShareToTW()
    {
        string nameParameter = "I scored " + score + " points in @IKStreamIvo his #LD40 game!";
        Application.OpenURL(TWITTER_ADDRESS +
           "?text=" + WWW.EscapeURL(nameParameter + "\n" + descriptionParam + "\n" + "Play the Game:\n" + appStoreLink));
    }
}
