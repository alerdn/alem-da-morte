using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Health
{
    [Header("Weapon setup")]
    public Weapon weapon;
    public Transform aim;

    [Header("Movement setup")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 10f;

    [Header("Dash setup")]
    public float dashSpeed = 30f;
    public float dashLength = 0.1f;

    [Header("Attacking movement")]
    public float attackingMovementSpeed = 4f;

    private float _activeSpeed;
    private float _dashCounter;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private bool _isAttacking = false;

    private void Start()
    {
        _activeSpeed = moveSpeed;
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetMovementDirection();
        HandleAttackingMovement();
        // BUGADO -> HandleRun();
        HandleAttack();
        HandleRealoadWeapon();

        /* Apenas DEV
        if (Input.GetKeyDown(KeyCode.Q) && weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }*/
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleAim();
    }

    #region Health
    public override void Damage(int d)
    {
        currentHP -= d;
        if (currentHP <= 0) Kill();
    }

    public override void Kill()
    {
        Debug.Log("Player is dead");
    }
    #endregion

    #region Other commands
    public void EquipWeapon(Weapon w)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }

        Destroy(w.gameObject.GetComponent<BoxCollider2D>());
        w.gameObject.transform.rotation = gameObject.transform.rotation;
        w.gameObject.transform.parent = gameObject.transform;
        w.gameObject.transform.localPosition = new Vector2(-0.7f, 0.7f);

        weapon = w;
    }
    #endregion

    #region Handling movements
    private void HandleAttackingMovement()
    {
        if (_isAttacking)
        {
            _activeSpeed = attackingMovementSpeed;
        }
        else
        {
            _activeSpeed = moveSpeed;
        }
    }

    private void GetMovementDirection()
    {
        if (_dashCounter <= 0)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
        }

        _moveInput.Normalize();
    }

    private void HandleMovement()
    {
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

    #endregion

    #region Handling attacks
    private void HandleAim()
    {

        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rb.position;
        aim.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }

    private void HandleAttack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (weapon)
            {
                _isAttacking = true;
                weapon.Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isAttacking = false;
        }
    }

    private void HandleRealoadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.ReloadWeapon();
        }
    }
    #endregion
}
