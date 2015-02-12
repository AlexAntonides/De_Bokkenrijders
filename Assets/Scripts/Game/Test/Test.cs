using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public GameObject bokkenRijder;
    public GameObject ghost;

	void Update () {
        gameObject.transform.Translate(Vector2.right * Time.deltaTime * 4);

        if (PhaseController.test == true)
        {
            bokkenRijder.SetActive(true);
            ghost.SetActive(false);
        }
        else if (PhaseController.test == false)
        {
            ghost.SetActive(true);
            bokkenRijder.SetActive(false);
        }
	}
}
