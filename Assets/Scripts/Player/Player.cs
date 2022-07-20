using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] Transform playerCamera;

    Settings _settings;
    Vector3 _velocity;
    Vector3 _playerMovementInput;
    Vector2 _playerMouseInput;
    float _xRot;

    [Inject]
    void Construct(Settings settings)
    {
        _settings = settings;
    }

    void Update()
    {
        _playerMovementInput = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0f,
            Input.GetAxisRaw("Vertical")
        );

        _playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    void FixedUpdate()
    {
        MovePlayer();
        MoveCamera();
    }

    void MovePlayer()
    {
        var moveVector = transform.TransformDirection(_playerMovementInput);

        if (Input.GetKey(KeyCode.E))
        {
            _velocity.y = 1f;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            _velocity.y = -1f;
        }

        transform.Translate(moveVector * (_settings.speed * Time.deltaTime));
        transform.Translate(_velocity * (_settings.speed * Time.deltaTime));

        _velocity.y = 0f;
    }

    void MoveCamera()
    {
        _xRot -= _playerMouseInput.y * _settings.sensitivity;
        transform.Rotate(0f, _playerMouseInput.x * _settings.sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
    }
    
    [Serializable]
    public class Settings
    {
        public float speed;
        public float sensitivity;
    }
}
