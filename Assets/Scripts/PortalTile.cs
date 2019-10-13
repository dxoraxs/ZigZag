using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTile : TileBlock
{
    private PortalType _portalType = PortalType.Exit;
    private Transform ExitPortal;

    public void SetPortalTypeEntry()
    {
        _portalType = PortalType.Entry;
    }

    public void SetExitPortal(Transform portal)
    {
        ExitPortal = portal;
    }
    
    //protected override void OnBlockReset()
    //{
    //    if (_crystalTransform != null)
    //        Destroy(_crystalTransform.gameObject);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _portalType==PortalType.Entry)
        {
            other.GetComponent<PlayerTeleport>().Teleport(ExitPortal);
        }
    }

    private enum PortalType
    {
        Entry,
        Exit
    }
}
