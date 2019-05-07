using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject turret;
    private Color startColor;
    private Renderer rend;
    public Color notEnoughMoneyColor;

    BuildManager buildManager;

    // Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }


    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuildTurret)
        {
            return;
        }
        if (turret != null)
        {
            Debug.Log("Cant build there!");
            return;
        }

        buildManager.BuildTurretOn(this);

       }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuildTurret)
        {
            return;
        }
        if (buildManager.HasTurretMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
