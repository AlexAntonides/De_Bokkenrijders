/*
 * By Floris de Haan
 * 02 / 2014
 */
using UnityEngine;
using System.Collections;

public class ThumbStick : MonoBehaviour
{
    #region Vars

    public GameObject stick;
    public float range = 3f;
    public float breakSpeed = 1f;
    public Vector2 stickUnitDirection = Vector2.zero;
    public Vector2 relativeScreenPos;

    private int _fingerId = -1;
    private Vector3 _startPos;

    
    #endregion

    #region Methods

    void Start()
    {
        float screenHeight = 2f * Camera.main.orthographicSize;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Move stick relative to camera size, for multiple resolutions
        transform.localPosition = new Vector3(-screenWidth/2 + relativeScreenPos.x * screenWidth, -screenHeight + relativeScreenPos.y * screenHeight, transform.localPosition.z);
        
        _startPos = stick.transform.localPosition;
    }

    void Update()
    {
        // Look for next touch
        if (_fingerId == -1 || _fingerId >= Input.touchCount)
        {
            FindNextTouch();
        }

        // Move to finger
        if (_fingerId != -1)
        {
            Touch touch = Input.GetTouch(_fingerId);

            // Touch end
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _fingerId = -1;
                return;
            }

            // Cancel out if no movement detected
            if (touch.phase == TouchPhase.Moved)
            {
                MoveTo(Camera.main.ScreenToWorldPoint(touch.position));
            }
        }
        // Move back to base pos if not there yet
        else if (stick.transform.localPosition != _startPos)
        {
            MoveBack();
        }

        CalculateDirection();
    }

    private void FindNextTouch()
    {
        _fingerId = -1;

        foreach (Touch c in Input.touches)
        {
            // Only check for new touches
            if (c.phase != TouchPhase.Began) continue;

            // Grab world relative position
            Vector3 pos = Camera.main.ScreenToWorldPoint(c.position);
            pos.z = transform.position.z;

            // Drag Begin when colliding with stick
            if (GetComponent<Collider>().bounds.Contains(pos))
            {
                _fingerId = c.fingerId;
                break;
            }
        }
    }

    private void MoveTo(Vector3 target)
    {
        // Offset from stick start pos to mouse pos
        Vector3 offset = (target - transform.position);
        offset.z = 0;

        // Bound if streched too far
        if (offset.magnitude > range)
        {
            offset.Normalize();
            offset *= range;
        }

        // Apply position
        stick.transform.localPosition = _startPos + offset;
    }

    private void MoveBack()
    {
        Vector3 offset = _startPos - stick.transform.localPosition;
        offset.z = 0;

        // Calc next step
        if (offset.magnitude > breakSpeed)
        {
            offset.Normalize();
            offset *= breakSpeed;
        }

        // Apply step
        stick.transform.localPosition += offset;
    }

    private void CalculateDirection()
    {
        stickUnitDirection.x = (stick.transform.localPosition.x - _startPos.x) / range;
        stickUnitDirection.y = (stick.transform.localPosition.y - _startPos.y) / range;
    }

    #endregion
}
