using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTexts : MonoBehaviour
{
    Vector3 startPos;
    public float Speed = 1;
    public float EndPosition = 300;
    bool first = true;
    private void Start()
    {
        startPos = transform.position;
    }

    void Update ()
    {
        if (transform.position.y < EndPosition)
        {
            transform.position += new Vector3(0, Speed, 0);
        }
	}

    public void Restart()
    {
        if (first)
        {
            first = false;
        } else
        {
            transform.position = startPos;
        }
    }
}
