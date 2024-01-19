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

    // Generate the first target before starting moving towards it
    void Start()
    {
        animator = this.GetComponent<Animator>();
        GenerateTarget();
    }

    // It creates a new vector3 within the camera space with a little offset to give the bird the chance to go out of the visible space
    private void GenerateTarget() {
        float y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - cameraOffset, 
                              Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + cameraOffset);
        float x = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x - cameraOffset, 
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + cameraOffset);
        
        target = new Vector3(x, y, this.transform.position.z);
    }

    // Returns true when the bird is at his target
    private bool TargetReached() {
        //Debug.Log("Distance: " + Vector3.Distance(this.transform.position, target).ToString());
        return Vector3.Distance(this.transform.position, target) < distanceThreshold;
    }

    // Returns true when the target is at his left side to use it in the animation control
    private bool TargetIsLeft() {
        return target.x < this.transform.position.x;
    }

    // Depending on the target side we update the animation, if we are fully rotated we use Direction = 0 to fly straight
    // MoveTowards and RotateTowards slowly rotate and moves the bird to his target, if we have reach it we generate a new one
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
