using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    public SelectionBehaviour selectionBehaviour;
    public GameManager gameManager;
    public GameObject turretTower;
    public GameObject cannonTower;
    public GameObject ballistaTower;
    public Button crystalCount;
    public TMP_Text crystals;
    public TMP_Text lives;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crystals.text = "Crystals: " + gameManager.GetCoins();
        lives.text = "Lives: " + gameManager.GetLives();

    }

    public void UpdateCoinsLabel(int newAmount)
    {
        crystals.text = "Crystals: " + newAmount.ToString();
    }
    public void UpdateLivesLabel(int newAmount)
    {
        lives.text = "Lives: " + newAmount.ToString();
    }
    public void Turret()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#DF768B", out color);
        if (gameManager.GetCoins() < 25)
        {
            StartCoroutine(FlashButtonDisabledColor(crystalCount, color, 1f));
        }
        else
        {
            gameManager.AddCoins(-25);
            Instantiate(turretTower, new Vector3(selectionBehaviour.selected.transform.position.x, 0f, selectionBehaviour.selected.transform.position.z), selectionBehaviour.selected.transform.rotation);
            selectionBehaviour.selected = null;
        }

    }

    public void Cannon()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#DF768B", out color);
        if (gameManager.GetCoins() < 30)
        {
            StartCoroutine(FlashButtonDisabledColor(crystalCount, color, 1f));
        }
        else
        {
            gameManager.AddCoins(-30);
            Instantiate(cannonTower, new Vector3(selectionBehaviour.selected.transform.position.x, 0f, selectionBehaviour.selected.transform.position.z), selectionBehaviour.selected.transform.rotation);
            selectionBehaviour.selected = null;
        }
    }

    public void Ballista()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#DF768B", out color);
        if (gameManager.GetCoins() < 20)
        {
            StartCoroutine(FlashButtonDisabledColor(crystalCount, color, 1f));
        }
        else
        {
            gameManager.AddCoins(-20);
            Instantiate(ballistaTower, new Vector3(selectionBehaviour.selected.transform.position.x, 0f, selectionBehaviour.selected.transform.position.z), selectionBehaviour.selected.transform.rotation);
            selectionBehaviour.selected = null;
        }
    }

    public IEnumerator FlashButtonDisabledColor(Button button, Color flashColor, float duration)
    {
        // Guardamos los colores originales del botón
        ColorBlock colors = button.colors;
        Color originalDisabledColor = colors.disabledColor;

        // Cambiamos el color de "disabled"
        colors.disabledColor = flashColor;
        button.colors = colors;

        // Esperamos
        yield return new WaitForSeconds(duration);

        // Restauramos el color original de "disabled"
        colors.disabledColor = originalDisabledColor;
        button.colors = colors;
    }

}
