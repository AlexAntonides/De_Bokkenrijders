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
        Touch touch = default(Touch);
        Vector3 stickPos = Stick.transform.localPosition;
        Vector3 worldTouchPos = Vector3.zero;
        bool moved = true;
        
        // Move stick relative to camera size, for multiple resolutions
        transform.localPosition = new Vector3(-Camera.main.orthographicSize, 0, transform.localPosition.z);

        // Look for next touch
        if (_fingerId == -1)
        {
            foreach (Touch c in Input.touches)
            {
                // Only check for new touches
                if (c.phase != TouchPhase.Began) continue;

                // Grab world relative position
                worldTouchPos = Camera.main.ScreenToWorldPoint(c.position);
                worldTouchPos.z = 0;

                // Drag Begin when colliding with stick
                if (collider.bounds.Contains(worldTouchPos))
                {
                    touch = c;
                    _fingerId = c.fingerId;
                    break;
                }
            }
        }
        else
        {
            touch = Input.GetTouch(_fingerId);

            // Touch end
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _fingerId = -1;
                return;
            }

            // 
            if (touch.phase != TouchPhase.Moved) return;

            worldTouchPos = Camera.main.ScreenToWorldPoint(touch.position);
            worldTouchPos.z = 0;
        }

        // Drag Move
        if (_fingerId != -1 && worldTouchPos != Vector3.zero)
        {
            // Offset from stick start pos to mouse pos
            Vector3 offset = (worldTouchPos - transform.position);
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
        // Move back to base pos if not there yet
        else if (Stick.transform.localPosition != _startPos)
        {
            Vector3 offset = _startPos - stickPos;
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
        
        // Calculate stick direction
        StickUnitDirection.x = (stickPos.x - _startPos.x) / Range;
        StickUnitDirection.y = (stickPos.y - _startPos.y) / Range;
    }

    #endregion
}
