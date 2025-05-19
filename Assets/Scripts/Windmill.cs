using UnityEngine;

public class RotateBlades : MonoBehaviour
{
    public Transform blades;
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f);

    void Update()
    {
        if (blades != null)
        {
            blades.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
