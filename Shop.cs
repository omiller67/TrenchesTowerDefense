
using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;

    public TurretBlueprint rifleTurret;
    public TurretBlueprint mgTurret;
    public TurretBlueprint fieldGun;

    public NodeBluebrint emptyNode;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectRifleSquad()
    {
        buildManager.SetTurretToBuild(rifleTurret);

    }

    public void SelectMGSquad()
    {
        buildManager.SetTurretToBuild(mgTurret);
    }

    public void SelectFieldGun()
    {
        buildManager.SetTurretToBuild(fieldGun);
    }

    public void SelectNode()
    {
        buildManager.SetBuildNode(emptyNode);
    }
}
