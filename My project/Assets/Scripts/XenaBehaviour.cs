using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdeM.Characters;
using UdeM.Sounds;

public sealed class XenaBehaviour : Character2DPlayerBehaviour
{
    private GameObject _xenaModel;
    private Animator _anim;
    private SoundManager _stepsSounds;
    private SoundManager _effectSounds;

    protected override void Start() 
    {
        base.Start();
        _xenaModel = transform.Find("XenaModel").gameObject;
        _anim = _xenaModel.GetComponent<Animator>();
        AttackColliderOff();

        _stepsSounds = gameObject.AddComponent<SoundManager>();
        _stepsSounds.InitClip("Xena/Sounds/", "Step");
        _stepsSounds.PrepareClip("Step");

        _effectSounds = transform.Find("SoundEffects").gameObject.AddComponent<SoundManager>();
        _effectSounds.InitClips("Xena/Sounds/", new List<string>() {"Jump1", "Land", "Attack1"});
    }

    protected override void Move()
    {
        base.Move();
        if (_isGrounded && _canMove && _axisH != 0) 
        {
            _stepsSounds.PlaySound(0.38f);
        } else {
            _stepsSounds.StopSound();
        }
    }

    protected override void OnLand()
    {
        _effectSounds.PlayOneShotSound("Land");
    }

    protected override void Update()
    {
        base.Update();
        _anim.SetBool("isRunning", _axisH != 0);
        _anim.SetBool("isFalling", _isFalling);
        _anim.SetBool("isGrounded", _isGrounded);

        if (Input.GetButtonDown("Fire1") && _isGrounded && _canAction)
        {
            Attack();
        }
    }

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void OnStartFall()
    {
        _anim.SetTrigger("onStartFall");
    }

    protected override void Jump()
    {
        base.Jump();
        _anim.SetTrigger("onJump");
        _effectSounds.PlayOneShotSound("Jump1");
    }

    private void Attack()
    {
        StartCoroutine(StopMoveDueTime(1.25f));
        StartCoroutine(StopActionDueTime(1.3f));
        _anim.SetTrigger("onAttack");
        _effectSounds.PlayOneShotSound("Attack1", 2);
    }

    public void AttackColliderOn()
    {
        transform.Find("AttackHitBox").GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void AttackColliderOff()
    {
        transform.Find("AttackHitBox").GetComponent<PolygonCollider2D>().enabled = false;
    }
}
