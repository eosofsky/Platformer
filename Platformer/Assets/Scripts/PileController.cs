using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileController : MonoBehaviour {

    public GameObject smallPiece;
    public GameObject mediumPiece;
    public GameObject largePiece;

    public GameObject[] cells;

	// Update is called once per frame
	void Update () {
        int WallsDestroyed = 0;
        foreach (var wall in cells)
        {
            if (!wall.activeSelf)
            {
                WallsDestroyed++;
            }
        }
        if (WallsDestroyed > 6)
        {
            largePiece.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
        }
        else if (WallsDestroyed > 3)
        {
            mediumPiece.SetActive(false);
        }
        else if (WallsDestroyed > 1)
        {
            smallPiece.SetActive(false);
        }
    }
}
