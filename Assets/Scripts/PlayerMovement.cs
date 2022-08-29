using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.forward * speed;
    }

    private void SetSpeed()
    {
        speed = 5;
    }

    private void OnEnable()
    {
        FinishTrigger.OnFinish += (positionL,positionR) => SetSpeed();
    }

    private void OnDisable()
    {
        FinishTrigger.OnFinish -= (positionL, positionR) => SetSpeed();
    }
}
