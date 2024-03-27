using Player;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject player;
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
        var timeScale = 1f;
        var lerpSpeed = 0.7f;
        if (_playerMovementController.CurrentAcceleration > 0)
        {
            timeScale = 0.2f;
            lerpSpeed = 0.3f;
        }

        Time.timeScale = Mathf.Lerp(Time.timeScale, timeScale,
            1 - Mathf.Pow(1 - lerpSpeed, Time.unscaledDeltaTime * 10));
    }
}