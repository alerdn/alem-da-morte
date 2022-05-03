using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Weapon setup")]
    public Weapon weapon;

    [Header("Movement setup")]
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    [Header("Dash setup")]
    public float dashSpeed = 40f;
    public float dashLength = 0.5f;

    public float _activeSpeed;
    public float _dashCounter;
    private Vector2 _moveInput;
    public Rigidbody2D _rb;

    private void Start()
    {
        _activeSpeed = moveSpeed;
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRun();
        HandleAttack();

        if (Input.GetKeyDown(KeyCode.Q) && weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }
    }

    private void FixedUpdate()
    {
        HandleAim();
    }

    public void EquipWeapon(Weapon w)
    {
        if (weapon != null) {
            Destroy(weapon.gameObject);
            weapon = null;
        }

        Destroy(w.gameObject.GetComponent<BoxCollider2D>());
        w.gameObject.transform.rotation = gameObject.transform.rotation;
        w.gameObject.transform.parent = gameObject.transform;
        w.gameObject.transform.localPosition = new Vector2(-0.7f, 0.7f);

        weapon = w;
    }

    private void HandleMovement()
    {
        if (_dashCounter <= 0)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
        }

        _moveInput.Normalize();

        _rb.velocity = _moveInput * _activeSpeed;
    }

    private void HandleRun()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_dashCounter <= 0 && _moveInput.normalized != Vector2.zero)
            {
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
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
        /*Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        
        // O ângulo varia de 180 a -180
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        */
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon?.Attack();
        }
    }
}
