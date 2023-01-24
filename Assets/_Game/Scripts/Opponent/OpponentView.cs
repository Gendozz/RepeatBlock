using UnityEngine;

public class OpponentView : MonoBehaviour, IView
{
    public Transform GetTransform => transform;
}