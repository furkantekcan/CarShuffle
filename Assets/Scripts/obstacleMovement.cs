using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class obstacleMovement : MonoBehaviour
{
    private Vector3 initialPos;
    private bool goback = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (goback)
        {
            transform.position = Vector3.MoveTowards(transform.position, goback ? initialPos : initialPos - new Vector3(10, 0, 0), 5 * Time.deltaTime);
            goback = transform.position.x != initialPos.x;
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, goback ? initialPos : initialPos + new Vector3(10, 0, 0), 5 * Time.deltaTime);
            goback = transform.position.x == initialPos.x + 10;
        }
    }
}
