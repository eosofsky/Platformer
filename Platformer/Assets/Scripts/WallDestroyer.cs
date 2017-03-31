using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour {

    public GameObject wall;
    public GameObject[] wallPieces;

    private int triggered;
    private int frames;

    private void Start()
    {
        triggered = 0;
        frames = 0;
    }

    private void Update()
    {
        if (triggered == 1)
        {
            foreach (var piece in wallPieces)
            {
                var rg = piece.AddComponent<Rigidbody>();
                rg.useGravity = false;
            }

            triggered++;
        }
        else if (triggered > 1)
        {
            frames++; 
        }

        if (frames > 100)
        {
            foreach (var piece in wallPieces)
            {
                piece.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            triggered++;
            wall.SetActive(false);
        }
    }
}
