using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquationTrigger : MonoBehaviour
{
    public delegate void EquationAction(int value, bool sign, GameObject car);
    public static event EquationAction OnEquationTrigger;
    private Text valuetext;

    private bool sign;
    private int value;

    // Start is called before the first frame update
    void Start()
    {
        valuetext = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        value = int.Parse(valuetext.text);
        sign = !gameObject.CompareTag("Negative");
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEquationTrigger?.Invoke(value, sign, other.gameObject);
    }

    

}
