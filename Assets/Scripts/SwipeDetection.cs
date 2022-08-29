using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwipeDetection : MonoBehaviour
{
    public delegate void SwipeAction();
    public static event SwipeAction OnSwipeLeft;
    public static event SwipeAction OnSwipeRight;

    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    void SwipeStart(Vector2 position, float time)
    {
        // Debug.Log("oldu-start");
        startPosition = position;
        startTime = time;
    }
    void SwipeEnd(Vector2 position, float time)
    {
        //Debug.Log("oldu-end");
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            //Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2d);
        }
    }


    void SwipeDirection(Vector2 direction)
    {

        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            //Debug.Log("swipeLEft");
            OnSwipeLeft?.Invoke();
        }
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            //Debug.Log("swipeRight");
            OnSwipeRight?.Invoke();
        }
    }
}
