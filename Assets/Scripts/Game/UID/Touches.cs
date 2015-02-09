using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Touches : MonoBehaviour {

    public float radius = 3;

    private Vector2 _startingLocation;

    private const string JOYSTICK_NAME = "Joystick";

    void Start()
    {
        _startingLocation = transform.position;
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector2 position;
                GameObject gObject = CheckRayCast(touch.position, out position);

                if (gObject.name == JOYSTICK_NAME && touch.phase == TouchPhase.Moved)
                {
                    gameObject.transform.position = new Vector3(position.x, position.y, gameObject.transform.position.z);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position;
            GameObject gObject = CheckRayCast(Input.mousePosition, out position);

            if (gObject.name == JOYSTICK_NAME)
            {
                gameObject.transform.position = new Vector3(position.y, position.x, gameObject.transform.position.z);
            }
        }
    }

    GameObject CheckRayCast(Vector3 position, out Vector2 worldPoint)
    {
        worldPoint = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
