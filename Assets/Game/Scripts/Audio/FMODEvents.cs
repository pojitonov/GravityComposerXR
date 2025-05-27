using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: SerializeField] public EventReference wave1 { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance)
        {
            Debug.LogWarning("Instance of a FMODEvents is already exists");
        }

        instance = this;
    }
}