using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private UIController uiController;

    void Start()
    {
        uiController = GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiController.isGamePaused)
        {
            Time.timeScale = 0f;
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float time = (horizontalInput != 0 || verticalInput != 0) ? .2f : 1f;
        float lerpTime = (horizontalInput != 0 || verticalInput != 0) ? .05f : .5f;

        Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
    }
}