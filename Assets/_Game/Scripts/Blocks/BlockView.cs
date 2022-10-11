using UnityEngine;
using Zenject;

public class BlockView : MonoBehaviour
{
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

}
