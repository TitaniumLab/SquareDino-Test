using UnityEngine;

public class EditModView : MonoBehaviour
{
    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }
}
