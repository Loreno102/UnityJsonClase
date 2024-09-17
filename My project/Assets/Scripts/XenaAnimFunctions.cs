using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenaAnimFunctions : MonoBehaviour
{
    public void AttackColliderOn()
    {
        GetComponentInParent<XenaBehaviour>().AttackColliderOn();
    }

    public void AttackColliderOff()
    {
        GetComponentInParent<XenaBehaviour>().AttackColliderOff();
    }
}
