using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class FirstPersonCameraController : MonoBehaviour
{
    private InputManager _inputManager;
    public new Camera camera;
    public float sensitivity = 7;
    [Range(0f, 90f)] public float yRotationLimit = 88f;
    private float _yRotation;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        var xRotation = _inputManager.GetCameraX() * sensitivity;
        transform.rotation *= Quaternion.AngleAxis(xRotation, Vector3.up);

        _yRotation +=  _inputManager.GetCameraY() * sensitivity;
        _yRotation = Mathf.Clamp(_yRotation, -yRotationLimit, yRotationLimit);
        camera.transform.localRotation = Quaternion.AngleAxis(_yRotation, Vector3.left);
    }
}