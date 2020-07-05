using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaHitBox : MonoBehaviour
{
    public bool canHit = false;
    public AudioSource BloodHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canHit)
        {
            if (!other.GetComponent<Enemy>().isDead)
            {
                canHit = false;
                other.GetComponent<Enemy>().BloodHit();
                BloodHit.Play();
            }
        }
    }
}
