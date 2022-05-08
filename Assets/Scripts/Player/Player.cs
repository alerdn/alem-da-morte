using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : Health
{
    [Header("Health animation setup")]
    public PostProcessVolume volume;

    [Header("Animation setup")]
    public Transform topSprite;
    public Transform bottomSprite;
    public Animator anim;

    [Header("Weapon setup")]
    public Weapon _currentWeapon;
    public Weapon simpleWeapon;
    public Weapon weapon;
    public Transform aim;

    [Header("Movement setup")]
    public float moveSpeed = 6f;
    public float attackingMovementSpeed = 2.5f;
    public float runningMovementSpeed = 12f;
    public float rotationSpeed = 10f;

    [Header("Dash setup")]
    public float dashSpeed = 30f;
    public float dashLength = 0.1f;

    [Header("Buffs")]
    public List<Buff> buffs;
    public bool isMoving = false;

    [Header("Debug only")]
    [SerializeField]
    private float _activeSpeed;
    [SerializeField]
    private int _stamina = 0;

    private float _dashCounter;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private bool _isAttacking = false;
    private Vignette _vignette;

    private void Start()
    {
        if (volume.profile.TryGetSettings<Vignette>(out var vig))
        {
            _vignette = vig;
        }

        _currentWeapon = simpleWeapon;
        _activeSpeed = moveSpeed;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        buffs = new List<Buff>();
    }

    private void Update()
    {
        if (GameManager.state == GameManager.STATE.Play)
        {
            _activeSpeed = moveSpeed;

            GetMovementDirection();
            HandleRun();
            HandleAttackingMovement();
            HandleAttack();
            HandleRealoadWeapon();

            UpdateBuffs();
            SwitchWeapon();

            anim.SetBool("IsMoving", isMoving);
            AnimateVignette();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.state == GameManager.STATE.Play)
        {
            HandleMovement();
            HandleAim();
        }
    }

    private void AnimateVignette()
    {
        DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, ((maxHP - currentHP) * .1f), 1f);
    }

    private void UpdateBuffs()
    {
        foreach (var b in buffs)
        {
            b.UpdateBuff();
        }
    }

    #region Health
    public void Heal(float h)
    {
        currentHP += h;
        if (currentHP > maxHP) currentHP = maxHP;
    }

    public override void Damage(float d)
    {
        currentHP -= d;
        if (currentHP <= 0) Kill();
    }

    public override void Kill()
    {
        if (GameManager.state == GameManager.STATE.Play)
        GameManager.Instance.PlayerDead();
    }
    #endregion

    #region Other commands
    public void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentWeapon = simpleWeapon;

        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapon)
                _currentWeapon = weapon;
        }
    }

    public void EquipWeapon(Weapon w)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }

        // Destroy(w.gameObject.GetComponent<BoxCollider2D>());
        w.gameObject.transform.rotation = topSprite.rotation;
        w.gameObject.transform.parent = topSprite;
        w.gameObject.transform.localPosition = new Vector2(-0.45f, 1.25f);

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
        var newVelocity = _moveInput * _activeSpeed;
        _rb.velocity = newVelocity;

        isMoving = newVelocity != Vector2.zero;
    }

    private void HandleRun()
    {
        if (_stamina > 0) _stamina--;
        if (Input.GetKey(KeyCode.Space))
        {
            if (_stamina < 100) _stamina += 2;

            if (_stamina < 100)
                _activeSpeed = runningMovementSpeed;
        }

    }

    #endregion

    #region Handling attacks
    private void HandleAim()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - _rb.position;
        aim.position = mousePosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        topSprite.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (angle >= 0 || angle < -180)
        {
            topSprite.localScale = new Vector3(-1f, 1f, 1f);
            bottomSprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            topSprite.localScale = new Vector3(1f, 1f, 1f);
            bottomSprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void HandleAttack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _isAttacking = true;
            _currentWeapon.Attack();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isAttacking = false;
        }
    }

    private void HandleRealoadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _currentWeapon.ReloadWeapon();
        }
    }
    #endregion
}
