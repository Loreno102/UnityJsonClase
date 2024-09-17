using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Characters
{
    public class Character2DPlayerBehaviour : Character2DBehaviour
    {
        protected float _axisH;
        
        protected override void Update()
        {
            base.Update();
            _axisH = Input.GetAxisRaw("Horizontal");
            Move();
            if (Input.GetButtonUp("Jump") && _isGrounded)
            {
                Jump();
            }
        }

        protected override void Move() 
        {
            base.Move();
            if (_canMove) 
            {
                _rb.velocity = new Vector2(_axisH * _moveSpeed, _rb.velocity.y);
            } else {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }      
        }

        protected override void Jump()
        {
            base.Jump();
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }

        protected override void Flip()
        {
            base.Flip();
            if (_axisH > 0) {
                transform.localScale = new Vector3(1,1,1);
            } else if (_axisH < 0) {
                transform.localScale = new Vector3(-1,1,1);
            }
        }
    }   
}
