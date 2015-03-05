using UnityEngine;
using System.Collections;

public class ParallaxBG : MonoBehaviour {
    public ThumbStick InputStick;

    [SerializeField]
    private Transform[] backgrounds; 

    void Update()
    {
        if (InputStick.StickUnitDirection.x > 0 || InputStick.StickUnitDirection.y > 0)
        {
            float valueX = InputStick.StickUnitDirection.x;
            float valueY = InputStick.StickUnitDirection.y;

            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x - valueX, backgrounds[i].transform.position.y - valueY, backgrounds[i].transform.position.z);
            }
        }
    }
}
