using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public delegate void FinishAction(Transform positionL = null , Transform positionR = null);
    public static event FinishAction OnFinish;
    public static event FinishAction OnFinish1;
    public static event FinishAction OnFinish2;
    public static event FinishAction OnFinish3;

    private void OnTriggerEnter(Collider other)
    {
        switch (gameObject.name)
        {
            case "Finish":
                OnFinish?.Invoke();
                break;
            case "Finish1":
                OnFinish1?.Invoke(transform.GetChild(0), transform.GetChild(1));
                break;
            case "Finish2":
                OnFinish2?.Invoke(transform.GetChild(0), transform.GetChild(1));
                break;
            case "Finish3":
                OnFinish3?.Invoke(transform.GetChild(0), transform.GetChild(1));
                break;
            default:
                break;
        }
        
    }
}