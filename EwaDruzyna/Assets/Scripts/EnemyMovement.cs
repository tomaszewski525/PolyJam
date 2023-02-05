using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    private Vector3 initialPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    public float speed = 0.3f;
    public float distance = 2;
    public float waitTime = 2;

    private float direction = 1;
    private float elapsedTime = 0;

   private void Update()
        {
            float movement = direction * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + movement, transform.position.y, transform.position.z);
            animator.SetBool("IsMoving", true);

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= waitTime + distance / speed)
            {
                direction *= -1;
                elapsedTime = 0;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            if (direction < 0 && transform.position.x <= initialPosition.x - distance)
            {
                animator.SetBool("IsMoving", false);
            }
            else if (direction > 0 && transform.position.x >= initialPosition.x + distance)
            {
                animator.SetBool("IsMoving", false);
            }
        }
}