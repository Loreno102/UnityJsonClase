using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;    
using UdeM.Base;

namespace UdeM.Characters 
{
    public abstract class CharacterBehaviour : CustomMonoBehaviour
    {
        protected float _moveSpeed;
        protected float _jumpForce;
        protected float _lastHeight;
        [SerializeField] protected float _height;
        [SerializeField] protected bool _isGrounded;
        [SerializeField] protected bool _isFalling;
        [SerializeField] protected bool _canMove;
        [SerializeField] protected bool _canAction;
        protected LayerMask _terrainLayer;
        protected LayerMask _playerLayer;
        protected UnityEvent _onStartFall;
        protected UnityEvent _onLand;

        protected override void Awake()
        {
            base.Awake();
            _moveSpeed = 5f;
            _jumpForce = 6f;
            _isGrounded = false;
            _canMove = true;
            _canAction = true;
        }

        protected override void Start()
        {
            base.Start();
            InitEvents();
            _terrainLayer = LayerMask.GetMask("Terrain");
            _playerLayer = LayerMask.GetMask("Player");
            CheckHeight();
            StartCoroutine(UpdateLastHeight());
        }

        private void InitEvents()
        {
            _onStartFall = new UnityEvent();
            _onLand = new UnityEvent();
            _onStartFall.AddListener(OnStartFall);
            _onLand.AddListener(OnLand);
        }

        protected virtual void OnStartFall()
        {
            Debug.Log("The Character has started to fall");
        }

        protected virtual void OnLand()
        {
            Debug.Log("The Character is on land");
        }

        protected override void Update()
        {
            base.Update();
            CheckHeight();
            CheckFalling();
        }
        protected virtual void Move() {}
        protected virtual void Jump() {}

        protected abstract void CheckHeight();

        private IEnumerator UpdateLastHeight()
        {
            while (true) 
            {
                _lastHeight = _height;
                yield return new WaitForSeconds(0.5f);
            }
        }

        protected IEnumerator StopMoveDueTime(float time)
        {
            _canMove = false;
            yield return new WaitForSeconds(time);
            _canMove = true;
        }

        protected IEnumerator StopActionDueTime(float time)
        {
            _canAction = false;
            yield return new WaitForSeconds(time);
            _canAction = true;
        }

        protected void CheckFalling()
        {
            if (_isGrounded)
            {
                if (_isFalling) 
                {
                    _onLand.Invoke();
                }
                _isFalling = false;
                _lastHeight = _height;
                return;
            }

            if (_lastHeight > _height)
            {
                if (!_isFalling) 
                {
                    _onStartFall.Invoke();
                }
                _isFalling = true;
            } else if (_lastHeight < _height) {
                _isFalling = false;
            }
        }
    }
}

