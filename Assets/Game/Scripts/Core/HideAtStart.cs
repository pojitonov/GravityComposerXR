using UnityEngine;

public class HideAtPoint : MonoBehaviour
{
    [SerializeField] GameObject objectToHide;

    private void Awake()
    {
        objectToHide.GetComponent<MeshRenderer>().enabled = false;
    }
}