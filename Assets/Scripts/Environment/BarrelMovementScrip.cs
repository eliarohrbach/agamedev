using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>BarrelMovementScrip</c> controlles the behaviour of the moving
/// barrels in the turorial level
/// </summary>
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

    /// <summary>
    /// Method <c>MoveForward</c> moves the barrel forward
    /// </summary>
    private void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
    }


}
