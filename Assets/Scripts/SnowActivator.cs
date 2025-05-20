using UnityEngine;

public class SnowActivator : MonoBehaviour
{
    private bool lastCooldownState = true; // para detectar cambios

    void Update()
    {
        if (GameManager.Instance == null) return;

        bool currentCooldown = GameManager.Instance.snowCooldown;

        // Solo cambia si el estado ha cambiado
        if (currentCooldown != lastCooldownState)
        {
            if (currentCooldown)
                Show();
            else
                Hide();

            lastCooldownState = currentCooldown;
        }
    }

    // Detectar clic sobre este objeto
    private void OnMouseDown()
    {
        Hide();
        GameManager.Instance.snowCooldown = false;
        GameManager.Instance.snowSelected = true;
    }

    // Hace que el objeto desaparezca (no se vea ni se pueda pulsar)
    public void Hide()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            rend.enabled = false;
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    // Hace que el objeto vuelva a aparecer y ser interactivo
    public void Show()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            rend.enabled = true;
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }
}
