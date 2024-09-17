using System.Collections;
using System.Collections.Generic;
using UdeM.Base;
using UnityEngine;

namespace UdeM.Sensors
{
    public abstract class Sensor<T> : CustomMonoBehaviour
    {
        protected bool _showSensorDebug;
        private int _layerMaskFilter;
        protected Vector3 _origin;
        protected GameObject _objectOrigin;
        protected Color _colorNotF = Color.red;
        protected Color _colorF = Color.green;
        protected bool _found = false;

        protected override void Awake()
        {
            base.Awake();
            _showSensorDebug = true;
            _origin = Vector3.zero;
            _layerMaskFilter = Physics.DefaultRaycastLayers;
            _objectOrigin = new GameObject("Sensor");
            _objectOrigin.transform.parent = transform;
            _objectOrigin.transform.localPosition = _origin;
        }

        public string sensorName {
            set {
                _objectOrigin.name = value;
            }
        }

        public Color color {
            set { _colorNotF = value; }
        }

        public Vector3 origin {
            set {
                _origin = value;
                _objectOrigin.transform.localPosition = _origin;
            }
        }

        public int layerMaskFilter {
            set { _layerMaskFilter = value; }
        }

        protected void DrawLine(Vector3 start, Vector3 end)
        {
            Debug.DrawLine(start, end, getColor());
        }

        protected void DrawRay(Vector3 start, Vector3 direction)
        {
            Debug.DrawRay(start, direction, getColor());
        }

        protected void DrawDebugCircle(Vector3 position, float radius, int segments = 32)
        {
            float angle = 0f;
            float angleStep = 360f / segments;

            for (int i = 0; i < segments; i++) {
                Vector3 startPoint = position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * radius, Mathf.Sin(Mathf.Deg2Rad * angle) * radius, 0);
                angle += angleStep;
                Vector3 endPoint = position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * radius, Mathf.Sin(Mathf.Deg2Rad * angle) * radius, 0);

                Debug.DrawLine(startPoint, endPoint, getColor());
            }
        }
        protected Color getColor()
        {
            return (!_found) ? _colorNotF : _colorF;
        }
        protected void checkFound(object obj)
        {
            _found = (obj != null);
        }
        protected abstract void ShowSensorDebug();
        public abstract T Sensate(int layerMask);
    }
}