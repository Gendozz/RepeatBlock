using UnityEngine;

public class BlockView : MonoBehaviour, IView
{
    
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public Transform GetTransform => transform;
}
