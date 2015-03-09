using UnityEngine;
using System.Collections;

public class ParallaxBG : MonoBehaviour {
    public ThumbStick InputStick;

    [SerializeField]
    private Transform[] backgrounds; 

    void Update()
    {
        if (InputStick.StickUnitDirection.x > 0 || InputStick.StickUnitDirection.y > 0 || InputStick.StickUnitDirection.x < 0 || InputStick.StickUnitDirection.y < 0)
        {
            float valueX = InputStick.StickUnitDirection.x;
            float valueY = InputStick.StickUnitDirection.y;

            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].transform.Translate(new Vector2(valueX, valueY) * 0.5f * Time.deltaTime);
            }
        }
    }
}
