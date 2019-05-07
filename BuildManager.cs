using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject rifleTurretPrefab;
    public GameObject mgTurretPrefab;
    public GameObject fieldGunPrefab;

    public GameObject emptyNode;

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;
    private NodeBluebrint buildNode;


    public void SetTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public bool CanBuildTurret { get { return turretToBuild != null; } }
    public bool CanBuildNode { get { return buildNode != null; } }

    public bool HasTurretMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    public bool HasNodeMoney { get { return PlayerStats.Money >= buildNode.cost; } }

    public void SetBuildNode(NodeBluebrint node)
    {
        buildNode = node;
    }

    public void BuildTurretOn(NodeScript node)
    {
        if(PlayerStats.Money < turretToBuild.cost)
        {
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret;
        if (turretToBuild.prefab == fieldGunPrefab)
        {
            Quaternion tRot = new Quaternion((node.transform.rotation.x), (node.transform.rotation.y - 180), (node.transform.rotation.z), node.transform.rotation.w);
          
            turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), tRot);
        }
        else
        {
            turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        }

        node.turret = turret;
        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }

    public void BuildNodeOn(NodeSpaceScript nodeSpace)
    {
        if (PlayerStats.Money < buildNode.cost)
        {
            return;
        }
        PlayerStats.Money -= buildNode.cost;
        GameObject node = (GameObject)Instantiate(buildNode.prefab, nodeSpace.transform.position + nodeSpace.positionOffset
        , Quaternion.identity);
        nodeSpace.rend.enabled = false;
        nodeSpace.node = node;
        nodeSpace.gameObject.SetActive(false);
        GameObject effect = (GameObject)Instantiate(buildEffect, nodeSpace.transform.position + nodeSpace.positionOffset, Quaternion.identity);
        Destroy(effect, 3f);
    }
}
