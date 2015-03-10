using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ClickButton : MonoBehaviour {
    public Vector2 position;

    [HideInInspector]
    public GameObject player;

    private const string PLAYERTAG = "Player";

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(PLAYERTAG);
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
                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.a - 0.5f);
            }
            else if (Input.GetMouseButtonUp(0) && hit.transform.name == transform.name)
            {
                ButtonReleased();
                gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.a + 0.5f);
            }
        }
    }

    public virtual void FixedUpdate()
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
