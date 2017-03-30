using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour {

    public GameObject wall;
    public GameObject[] wallPieces;

    private int triggered;

    private void Start()
    {
        triggered = 0;
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
