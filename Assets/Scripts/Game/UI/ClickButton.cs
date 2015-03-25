using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickButton : MonoBehaviour {
    public Vector2 position;

    [HideInInspector]
    public GameObject player;

    private const string PLAYERTAG = "Player";
    [SerializeField]
    private int _fingerId = -1;

    public bool changePosition = true;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(PLAYERTAG);
        }
    }

    public virtual void Update()
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
                if (current.phase != TouchPhase.Began) continue;

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
            }
        }
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
}
