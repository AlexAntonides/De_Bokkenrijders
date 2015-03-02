/*
 * By Floris de Haan
 * 02 / 2014
 */
using UnityEngine;
using System.Collections;

public class ThumbStick : DragButton
{
    #region Properties

    public GameObject Stick;
    public float Range = 3f;
    public float BreakSpeed = 1f;
    public Vector2 StickUnitDirection = Vector2.zero;

    #endregion

    #region Vars

    private bool _mouseDown = false;
    private Vector3 _startPos;

    private Vector3 stickPos;

    #endregion

    #region Methods

    void Start()
    {
        _startPos = Stick.transform.localPosition;
    }

    public override void ButtonUpdate()
    {
        // Move stick relative to camera size, for multiple resolutions
        stickPos = Stick.transform.localPosition;

        // Calculate stick direction
        StickUnitDirection.x = (stickPos.x - _startPos.x) / Range;
        StickUnitDirection.y = (stickPos.y - _startPos.y) / Range;
    }

    public override void ButtonReleased()
    {
        print(gameObject.name);
        if (_mouseDown)
        { 
            // Drag End
            _mouseDown = false;
        }
        else if (Stick.transform.localPosition != _startPos)
        { 
            // Move back to base pos
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
    }

    public override void ButtonPressed()
    {
        // Get mouse pos relative to world
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;

        // Drag Move
        if (_mouseDown)
        {
            // Offset from stick start pos to mouse pos
            Vector3 offset = (worldMousePos - transform.position);
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

         // Drag Begin when colliding
        else if (collider.bounds.Contains(worldMousePos))
        {
            _mouseDown = true;
        }
        else if (_mouseDown)
        { // Drag End
            _mouseDown = false;
        }
        else if (Stick.transform.localPosition != _startPos)
        { // Move back to base pos
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
    }
    #endregion
}
