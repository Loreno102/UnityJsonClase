using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdeM.Sensors
{
    public abstract class SensorGameObject : Sensor<GameObject>
    {
        public override abstract GameObject Sensate(int layerMask);
    }
}