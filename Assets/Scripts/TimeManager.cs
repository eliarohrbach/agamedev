using Player;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject player;
    public float velocityToggle = 1;
    private PlayerMovementController _playerMovementController;

    private void Start()
    {
        _playerMovementController = player.GetComponent<PlayerMovementController>();
    }

    void OnEnable()
    {
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        var velocity = _playerMovementController.Velocity;
        float time = velocity > velocityToggle ? .2f : 1f;
        float lerpTime = velocity > velocityToggle ? .05f : .5f;
        Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
    }
}
