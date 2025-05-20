using UnityEngine;
using UnityEngine.UI;

public class SelectionBehaviour : MonoBehaviour
{
    public GameObject selected = null;
    private Canvas canvas;
    public GameObject turretButton;
    public GameObject cannonButton;
    public GameObject ballistaButton;
    public GameObject enemyInfo;
    public Slider slider;

    private EnemyWalk currentEnemy = null;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        lifeInterfaceShown(false);
        towerInterfaceShown(false);
    }

    void Update()
    {
        // Detect click
        if (Input.GetMouseButtonDown(0) &&
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            GameObject clicked = hit.transform.gameObject;

            if (clicked != selected && clicked.name != "map")
            {
                selected = clicked;
                GetComponent<Renderer>().enabled = true;
            }
            else
            {
                selected = null;
                GetComponent<Renderer>().enabled = false;
            }

            Debug.Log("Objeto clicado: " + hit.transform.name);
        }

        if (selected != null)
        {
            transform.position = new Vector3(selected.transform.position.x, 0.3f, selected.transform.position.z);

            if (selected.CompareTag("TowerSpot"))
            {
                towerInterfaceShown(true);
                lifeInterfaceShown(false);
                currentEnemy = null; // Limpiar referencia
            }
            else if (selected.CompareTag("Enemy"))
            {
                if (currentEnemy == null || currentEnemy.gameObject != selected)
                {
                    currentEnemy = selected.GetComponent<EnemyWalk>();
                    slider.maxValue = currentEnemy.maxLife; // Solo si tienes maxLife
                }

                slider.value = currentEnemy.life;
                lifeInterfaceShown(true);
                towerInterfaceShown(false);
            }
            else
            {
                towerInterfaceShown(false);
                lifeInterfaceShown(false);
                currentEnemy = null;
            }
        }
        else
        {
            towerInterfaceShown(false);
            lifeInterfaceShown(false);
            currentEnemy = null;
        }
    }

    public void towerInterfaceShown(bool state)
    {
        turretButton.SetActive(state);
        cannonButton.SetActive(state);
        ballistaButton.SetActive(state);
    }

    public void lifeInterfaceShown(bool state)
    {
        enemyInfo.SetActive(state);
    }
}
