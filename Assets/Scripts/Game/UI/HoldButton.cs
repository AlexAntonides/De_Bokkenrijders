using UnityEngine;
using System.Collections;

public class HoldButton : ClickButton {

    public float holdTime = 0.4f;
    private float _curholdTime;

    private int _fingerID = -1;

    public override void Update()
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

                        if (_curholdTime > holdTime && UserData.loaded.bullets > 0)
                        {
                            ButtonHold();
                        }
                        else
                        {
                            ButtonPressed();
                        }
                    }

                    _curholdTime = 0;
                }
            }
        }
    }

    public virtual void ButtonHold()
    {
        print("You held the button: " + gameObject.name);
    }
}
