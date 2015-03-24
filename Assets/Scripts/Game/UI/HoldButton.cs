using UnityEngine;
using System.Collections;

public class HoldButton : ClickButton {

    public float holdTime = 0.4f;
    private float _curholdTime;

    private int _fingerID = -1;

    public override void FixedUpdate()
    {
        if (changePosition == true)
        {
            transform.localPosition = new Vector3(-Camera.main.orthographicSize + position.x, position.y, transform.localPosition.z);
        }

        Touch currentTouch;
        Vector3 touchPos;

        // Validate current touch
        if (_fingerID != -1 && (_fingerID >= Input.touchCount || (currentTouch = Input.GetTouch(_fingerID)).phase == TouchPhase.Ended || currentTouch.phase == TouchPhase.Canceled))
        {
            _fingerID = -1;
            ButtonReleased();
        }

        // Search for finger
        if (_fingerID == -1)
        {
            foreach (Touch current in Input.touches)
            {
                // Skip non beginning touches
                //if (_holdable == true)
                //{
                if (current.phase != TouchPhase.Began)
                {
                    _curholdTime += Time.deltaTime;
                }

                if (current.phase == TouchPhase.Ended)
                {
                    // Check collision
                    Vector3 currentPos = Camera.main.ScreenToWorldPoint(current.position);
                    currentPos.z = transform.position.z;
                    if (GetComponent<Collider2D>().bounds.Contains(currentPos))
                    {
                        touchPos = currentPos;
                        currentTouch = current;
                        _fingerID = current.fingerId;

                        if (_curholdTime > holdTime)
                        {
                            ButtonHold();
                        }

                        _curholdTime = 0;
                    }
                }
                //}
                /*
            else if (_holdable == false)
            {
                if (current.phase == TouchPhase.Began)
                {
                    // Skip non beginning touches.
                }

                // Check collision
                Vector3 currentPos = Camera.main.ScreenToWorldPoint(current.position);
                currentPos.z = transform.position.z;
                if (GetComponent<Collider2D>().bounds.Contains(currentPos))
                {
                    touchPos = currentPos;
                    currentTouch = current;
                    _fingerId = current.fingerId;
                    ButtonPressed();
                }
            } */
            }
        }
    }

    public virtual void ButtonHold()
    {
        print("You held the button: " + gameObject.name);
    }
}
