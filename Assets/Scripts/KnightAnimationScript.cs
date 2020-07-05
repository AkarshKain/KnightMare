using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimationScript : MonoBehaviour
{
    public GameObject KatanaOnBack;
    public GameObject KatanaInHand;
    public Player Player;

    public void ArmKatana()
    {
        KatanaOnBack.SetActive(false);
        KatanaInHand.SetActive(true);
    }

    public void DisarmKatana()
    {
        KatanaOnBack.SetActive(true);
        KatanaInHand.SetActive(false);
    }

    public void CanMove()
    {
        Player.CanMove();
    }

    public void EndAttack()
    {
        Player.EndAttack();
    }

    public void Combo()
    {
        Player.katanaHitBox.canHit = true;
    }

    public void EndDeathAnimation()
    {
        Player.GameScene.Death();
    }
}
