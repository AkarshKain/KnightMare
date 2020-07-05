using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceHitBox : MonoBehaviour
{
    public bool canHit = false;
    public AudioSource BloodHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canHit)
        {
            if (!other.GetComponent<Player>().isDead)
            {
                canHit = false;
                other.GetComponent<Player>().BloodHit();
                BloodHit.Play();
            }
        }
    }
}
