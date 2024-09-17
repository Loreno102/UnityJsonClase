using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Characters 
{
    public class Character2DBehaviour : CharacterBehaviour
    {
        protected float _radiusOverlap;
        protected Rigidbody2D _rb;
        
        protected override void Awake()
        {
            base.Awake();
            _radiusOverlap = 0.2f;
        }
        
        protected override void Start()
        {
            base.Start();
            _rb = GetComponent<Rigidbody2D>();
            if (_rb == null)
            {
                Debug.LogError("The character2D object must contain a Rigidbody2D component");
            }
            _rb.freezeRotation = true;
        }

        protected override void Update()
        {
            base.Update();
            if (_canMove)
            {
                Flip();
            }
            CheckGrounded();
        }
        
        protected virtual void CheckGrounded()
        {
            _isGrounded = Physics2D.OverlapCircle(transform.position, _radiusOverlap, _terrainLayer) != null;
        }

        protected override void CheckHeight() 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity,~_playerLayer);
            if (hit.collider != null) 
            {
                _height = hit.distance;
            } else {
                _height = 0;
            }
        }

        protected virtual void Flip() {}
    }
}
