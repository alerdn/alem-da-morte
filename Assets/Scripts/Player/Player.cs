using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement setup")]
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    [Header("Dash setup")]
    public float dashSpeed = 40f;
    public float dashLength = 0.5f;

    public float _activeSpeed;
    public float _dashCounter;
    private Vector2 _moveInput;

    private void Start()
    {
        _activeSpeed = moveSpeed;
    }

    void Update()
    {
        HandleMovement();
        HandleRun();
        HandleAim();
    }

    private void HandleMovement()
    {
        if (_dashCounter <= 0)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
        }

        _moveInput.Normalize();

        rb.velocity = _moveInput * _activeSpeed;
    }

    private void HandleRun()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_dashCounter <= 0 && _moveInput.normalized != Vector2.zero)
            {
                Debug.Log("Dashou");
                _activeSpeed = dashSpeed;
                _dashCounter = dashLength;
            }

        }

        if (_dashCounter > 0)
        {
            _dashCounter -= Time.deltaTime;

            if (_dashCounter <= 0)
            {
                _activeSpeed = moveSpeed;
            }
        }
    }

    private void HandleAim()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        
        // O ângulo varia de 180 a -180
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
