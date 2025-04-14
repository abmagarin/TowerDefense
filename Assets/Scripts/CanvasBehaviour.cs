using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    public SelectionBehaviour selectionBehaviour;
    public GameObject turretTower;
    public GameObject cannonTower;
    public GameObject ballistaTower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Turret()
    {
        Instantiate(turretTower, new Vector3(selectionBehaviour.selected.transform.position.x, 0f, selectionBehaviour.selected.transform.position.z), selectionBehaviour.selected.transform.rotation);
        selectionBehaviour.selected = null;
    }

    public void Cannon()
    {
        Instantiate(cannonTower, new Vector3(selectionBehaviour.selected.transform.position.x, 0f, selectionBehaviour.selected.transform.position.z), selectionBehaviour.selected.transform.rotation);
        selectionBehaviour.selected = null;
    }

    public void Ballista()
    {
        Instantiate(ballistaTower, new Vector3(selectionBehaviour.selected.transform.position.x, 0f, selectionBehaviour.selected.transform.position.z), selectionBehaviour.selected.transform.rotation);
        selectionBehaviour.selected = null;
    }
}
