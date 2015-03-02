using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickButton : MonoBehaviour {
    public Vector2 position;

    [HideInInspector]
    public GameObject player;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && hit.transform.name == transform.name)
            {
                ButtonPressed();
            }
        }
    }

    void FixedUpdate()
    {
        transform.localPosition = new Vector3(-Camera.main.orthographicSize + position.x, position.y, transform.localPosition.z);
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
