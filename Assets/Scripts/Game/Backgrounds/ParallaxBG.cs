using UnityEngine;
using System.Collections;

public class ParallaxBG : MonoBehaviour {
    public ThumbStick InputStick;

    [SerializeField]
    private Transform[] backgrounds;

    private float _speed = 10;

    private GameObject _player;
    private GameObject _camera;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Constants.PLAYERTAG);
        _camera = GameObject.FindGameObjectWithTag(Constants.CAMERATAG);
    }

    void Update()
    {
        if (InputStick.stickUnitDirection.x != 0)
        {
            float valueX = InputStick.stickUnitDirection.x;
            float valueY = 0;

            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (backgrounds[i].transform.position.x != _player.transform.position.x + _camera.GetComponent<Camera>().orthographicSize)
                {
                    float speed = _speed * (i + 1);
                    backgrounds[i].transform.Translate(new Vector2(valueX, valueY) * speed * Time.deltaTime);
                }
            }
        }
    }
}
