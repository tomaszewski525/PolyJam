using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public TestMovement.animalType type = TestMovement.animalType.Spider;
    public float jumpForce = 500.0f;
    public float speed = 10;
    public float gravityDownForce = 2000;
    public float gravityForce = 1000;
    public bool canJump = true;
    public bool canMove = true;
    public bool canWalkOnWall = true;
    public Sprite sprite;
    public RuntimeAnimatorController animator;
    public RuntimeAnimatorController jumpAnimator;
    //public TestMovement.specialAbilityType ability = TestMovement.specialAbilityType.ShootLine;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = sprite;
    }
}
