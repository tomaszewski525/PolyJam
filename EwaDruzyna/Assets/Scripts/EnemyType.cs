using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public TestMovement.animalType type = TestMovement.animalType.Spider;
    public float jumpForce = 500.0f;
    public float speed = 10;
    public TestMovement.specialAbilityType ability = TestMovement.specialAbilityType.ShootLine;
}
