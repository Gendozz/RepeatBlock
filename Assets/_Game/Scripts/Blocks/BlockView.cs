using UnityEngine;

public class BlockView : MonoBehaviour
{
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

}
