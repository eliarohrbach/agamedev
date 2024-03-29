using Player;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private PlayerMovementController _playerMovementController;
    private const float MinTimeScale = 0.15f;
    private const float BaseSlowedDownTimeScale = 0.25f;

    private void Awake()
    {
        _playerMovementController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
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
            timeScale = Mathf.Max(MinTimeScale, BaseSlowedDownTimeScale -
                                                _playerMovementController.CurrentAcceleration /
                                                (_playerMovementController.acceleration * 200));
            lerpSpeed = 0.3f;
        }

        Time.timeScale = Mathf.Lerp(Time.timeScale, timeScale,
            1 - Mathf.Pow(1 - lerpSpeed, Time.unscaledDeltaTime * 10));
    }
}