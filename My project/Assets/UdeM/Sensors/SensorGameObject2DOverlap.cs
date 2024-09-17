using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Sensors
{
    public class SensorGameObject2DOverlap : SensorGameObject
    {
        protected float _radiusSize;
        
        protected override void Awake()
        {
            base.Awake();
            _radiusSize = 0.5f;
        }

        public float radiusSize {
            get { return _radiusSize; }
            set { _radiusSize = value; }
        }

        protected override void ShowSensorDebug()
        {
            DrawDebugCircle(_objectOrigin.transform.position, _radiusSize);
        }

        public override GameObject Sensate(int layerMask)
        {
            if (_showSensorDebug) {
                ShowSensorDebug();
            }
            GameObject obj = Physics2D.OverlapCircle(_objectOrigin.transform.position, _radiusSize, layerMask)?.gameObject;
            checkFound(obj);
            return obj;
        }
    }
}
