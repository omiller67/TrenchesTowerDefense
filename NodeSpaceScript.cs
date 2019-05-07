using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSpaceScript : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject node;
    private Color startColor;
    public Renderer rend;
    public Color notEnoughMoneyColor;

    BuildManager buildManager;

    // Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
	}

    void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuildNode)
        {
            return;
        }

        if (node != null)
        {
            Debug.Log("Cant build there!");
            return;
        }
        buildManager.BuildNodeOn(this);
       
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuildNode)
        {
            return;
        }

        if (buildManager.HasNodeMoney)
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

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
