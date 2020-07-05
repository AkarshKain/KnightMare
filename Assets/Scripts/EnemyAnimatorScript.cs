using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorScript : MonoBehaviour {
    public Enemy Enemy;
    public void EndAttack()
    {
        Enemy.EndAttack();
    }
}
