using UnityEngine;

public class PlayerView : MonoBehaviour, IView
{
    public Transform GetTransform => transform;
}
