using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Animator animator;
    private Vector3 target;
    private Vector3 lastRotationDirection;
    private float cameraOffset = 3f;
    public float distanceThreshold = 1f;
    public float speed = 5f;
    public float rotSpeed = 5f;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        GenerateTarget();
    }

    private void GenerateTarget() {
        float y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - cameraOffset, 
                              Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + cameraOffset);
        float x = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x - cameraOffset, 
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + cameraOffset);
        
        target = new Vector3(x, y, this.transform.position.z);
    }

    private bool TargetReached() {
        //Debug.Log("Distance: " + Vector3.Distance(this.transform.position, target).ToString());
        return Vector3.Distance(this.transform.position, target) < distanceThreshold;
    }

    private bool TargetIsLeft() {
        return target.x < this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TargetReached()) {
            if (TargetIsLeft()) {
                animator.SetInteger("Direction", 2);
            } else {
                animator.SetInteger("Direction", 1);
            }
            float step = speed * Time.deltaTime;
            float rotStep = rotSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            Vector3 rotationDirection = Vector3.RotateTowards(transform.forward, target - this.transform.position, rotStep, 0.0f);
            if (lastRotationDirection == rotationDirection) 
                animator.SetInteger("Direction", 0);
            lastRotationDirection = rotationDirection;
            transform.rotation = Quaternion.LookRotation(rotationDirection);
        } else {
            GenerateTarget();
        }
        
    }
}
