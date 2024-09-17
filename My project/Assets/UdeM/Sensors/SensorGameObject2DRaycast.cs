using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UdeM.Sensors
{
    public class SensorGameObject2DRaycast : SensorGameObject
    {
        protected Vector3 _direction;
        protected float _size;
        protected RaycastHit2D _hit;

        protected override void Awake()
        {
            base.Awake();
            _direction = new Vector3(1,0,0);
            _size = 1.0f;
            origin = new Vector3(0,1,0);
        }

        public Vector3 direction {
            set { _direction = value; }
        }

        public float size {
            set { _size = value; }
        }

        protected override void ShowSensorDebug()
        {
            DrawRay(_objectOrigin.transform.position, GetDirectionDirection() * _size);
        }

        public override GameObject Sensate(int layerMask)
        {
            _hit = Physics2D.Raycast(_objectOrigin.transform.position, GetDirectionDirection(), _size, layerMask);
            checkFound(_hit.collider);
            if (_showSensorDebug) {
                ShowSensorDebug();
            }
            return _hit.collider?.gameObject;
        }

        private Vector2 GetDirectionDirection()
        {
            return new Vector2(_direction.x * transform.localScale.x, _direction.y);
        }
    }


}