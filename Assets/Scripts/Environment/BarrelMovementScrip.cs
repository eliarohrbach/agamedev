using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovementScrip : MonoBehaviour
{
    [SerializeField] float dictance;
    [SerializeField] float speed;
    [SerializeField] bool movementOn;
    [SerializeField] GameObject startPoint;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x + dictance, transform.position.y, transform.position.z);
    }

    void Update()
    {
        movementOn = startPoint.GetComponent<TutorialTriggerControllerScript>().moveBarrel;

        if (movementOn)
        {
            currentPosition = transform.position;

            if (currentPosition != endPosition)
            {
                MoveForward();
            }
            else
            {
                endPosition = startPosition;
                startPosition = currentPosition;
            }
        }
    }

    private void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
    }


}
