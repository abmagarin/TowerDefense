using UnityEngine;

public class SelectionBehaviour : MonoBehaviour
{
    public GameObject selected = null;
    private Canvas canvas;
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasShown(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            GameObject clicked = hit.transform.gameObject;
            if (clicked != selected)
            {
                selected = clicked;
                GetComponent<Renderer>().enabled = true;
                // Mueve el objeto con este script a la posición del objeto clicado
                transform.position = new Vector3(hit.transform.position.x, 0.3f, hit.transform.position.z);
            }
            else
            {
                selected = null;
                GetComponent<Renderer>().enabled = false;
            }

            Debug.Log("Objeto clicado: " + hit.transform.name);
        }

        if (selected != null && selected.tag == "TowerSpot")
        {
            canvasShown(true);
        }
        else
        {
            canvasShown(false);
        }
        if (selected == null)
        {
            GetComponent<Renderer>().enabled = false;
            canvasShown(false);
        }

    }

    public void canvasShown(bool state)
    {
        canvas.gameObject.SetActive(state);
    }
}
