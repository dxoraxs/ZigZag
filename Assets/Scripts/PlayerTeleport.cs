using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public void Teleport(Transform portal)
    {
        transform.position = new Vector3(portal.position.x, transform.position.y, portal.position.z);
    }
}
