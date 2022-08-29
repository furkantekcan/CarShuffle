using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquationManager : MonoBehaviour
{
    public GameObject stationary;
    public GameObject moving;
    public GameObject negativePref;
    public GameObject pozitivePref;

    private Text valueText;

    // Start is called before the first frame update
    void Start()
    {
        DecideType();
    }

    private void DecideType()
    {
        if (Random.Range(0, 3) % 2 == 0)
        {
            stationary.SetActive(true);
            GameObject newObj1 = Random.Range(0, 2) == 0 ? negativePref : pozitivePref;
            GameObject newObj2 = Random.Range(0, 2) == 0 ? negativePref : pozitivePref;

            Instantiate(newObj1, stationary.transform.GetChild(0));
            Instantiate(newObj2, stationary.transform.GetChild(1));

            GenerateRandomNumber(newObj1.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>());
            GenerateRandomNumber(newObj2.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>());
        }

        else
        {
            moving.SetActive(true);
            GameObject pref;
            if (Random.Range(0, 2) == 0) pref = Instantiate(negativePref, moving.transform.GetChild(0));
            else pref =Instantiate(pozitivePref, moving.transform.GetChild(0));
            GenerateRandomNumber(pref.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>());

        }
    }

    private void GenerateRandomNumber(Text obj)
    {
        obj.text = Random.Range(1, 3).ToString();
    }
}
