using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class DragButton : MonoBehaviour {
    public Vector2 position;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, -Vector2.up);

        if (hit.collider != null)
        {
            print(hit.collider.name);

            if (Input.GetMouseButton(0) && hit.transform.name == transform.name)
            {
                ButtonPressed();
            }
            else if(Input.GetMouseButtonUp(0) && hit.transform.name == transform.name)
            {
                ButtonReleased();
            }
        }

        ButtonUpdate();
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
