using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickButton : MonoBehaviour {
    public Vector2 position;

    [HideInInspector]
    public GameObject player;

    public float holdTime = 0.4f;

    private const string PLAYERTAG = "Player";
    [SerializeField]
    private int _fingerId = -1;
    private float _curholdTime;

    public bool changePosition = true;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(PLAYERTAG);
        }
    }

    void Update()
    {
        Touch currentTouch;
        Vector3 touchPos;

        // Validate current touch
        if (_fingerId != -1 && (_fingerId >= Input.touchCount || (currentTouch = Input.GetTouch(_fingerId)).phase == TouchPhase.Ended || currentTouch.phase == TouchPhase.Canceled))
        {
            _fingerId = -1;
            ButtonReleased();
        }

        // Search for finger
        if (_fingerId == -1)
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
                            _fingerId = current.fingerId;

                            if (_curholdTime < holdTime)
                            {
                                ButtonPressed();
                            }
                            else
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

        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //if (hit.collider != null)
        //{
        //    if (Input.GetMouseButtonDown(0) && hit.transform.name == transform.name)
        //    {
        //        ButtonPressed();
        //        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.g, 0.5f);
        //    }
        //    else if (Input.GetMouseButtonUp(0) && hit.transform.name == transform.name)
        //    {
        //        ButtonReleased();
        //        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.g, 1f);
        //    }
        //}
    }

    public virtual void FixedUpdate()
    {
        if (changePosition == true)
        {
            transform.localPosition = new Vector3(-Camera.main.orthographicSize + position.x, position.y, transform.localPosition.z);
        }
    }

    public virtual void ButtonUpdate()
    {
        print("Updating button: " + gameObject.name);
    }

    public virtual void ButtonReleased()
    {
        print("Button: " + gameObject.name + " has been released.");
    }

    public virtual void ButtonPressed()
    {
        print("You clicked the button: " + gameObject.name);
    }

    public virtual void ButtonHold()
    {
        print("You held the button: " + gameObject.name);
    }
}
