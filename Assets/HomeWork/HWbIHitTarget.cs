using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitTarget
{
    public void HitTarget(RaycastHit hit, int damage);
}
