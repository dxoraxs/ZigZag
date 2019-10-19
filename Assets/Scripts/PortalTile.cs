using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTile : TileBlock
{
    [SerializeField]private Transform _portalTransform;
    private Material[] m_Material;
    private PortalType _portalType = PortalType.Exit;
    private Transform ExitPortal;
    private Renderer _renderer;

    public void SetPortalTypeEntry()
    {
        _portalType = PortalType.Entry;
    }

    public void SetExitPortal(Transform portal)
    {
        ExitPortal = portal;
        Color setDuetPortal = GetRandomColor();
        ExitPortal.GetComponent<PortalTile>().SetColorPortal(setDuetPortal);
        SetColorPortal(setDuetPortal);
    }
    
    public void SetColorPortal(Color setMaterial)
    {
        if (_renderer == null) Start();
        m_Material[1].color = setMaterial;
        _renderer.materials= m_Material;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, 1);
    }

    private void Start()
    {
        _renderer = _portalTransform.GetComponent<Renderer>();
        m_Material = _renderer.materials;
    }

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
