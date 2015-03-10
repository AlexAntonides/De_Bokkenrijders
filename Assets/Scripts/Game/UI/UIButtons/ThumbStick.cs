/*
 * By Floris de Haan
 * 02 / 2014
 */
using UnityEngine;
using System.Collections;

public class ThumbStick : MonoBehaviour
{
    #region Properties

    public GameObject Stick;
    public float Range = 3f;
    public float BreakSpeed = 1f;
    public Vector2 StickUnitDirection = Vector2.zero;
    
    #endregion

    #region Vars

    private int _fingerId = -1;
    private Vector3 _startPos;
    
    #endregion

    #region Methods

    void Start()
    {
        _startPos = Stick.transform.localPosition;
    }

    void Update()
    {
        // Move stick relative to camera size, for multiple resolutions
        transform.localPosition = new Vector3(-Camera.main.orthographicSize, -Camera.main.orthographicSize/2, transform.localPosition.z);

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
        else if (Stick.transform.localPosition != _startPos)
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
            if (collider.bounds.Contains(pos))
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
        if (offset.magnitude > Range)
        {
            offset.Normalize();
            offset *= Range;
        }

        // Apply position
        Stick.transform.localPosition = _startPos + offset;
    }

    private void MoveBack()
    {
        Vector3 offset = _startPos - Stick.transform.localPosition;
        offset.z = 0;

        // Calc next step
        if (offset.magnitude > BreakSpeed)
        {
            offset.Normalize();
            offset *= BreakSpeed;
        }

        // Apply step
        Stick.transform.localPosition += offset;
    }

    private void CalculateDirection()
    {
        StickUnitDirection.x = (Stick.transform.localPosition.x - _startPos.x) / Range;
        StickUnitDirection.y = (Stick.transform.localPosition.y - _startPos.y) / Range;
    }

    #endregion
}
