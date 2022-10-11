using System.Collections;
using UnityEngine;
using Zenject;

public class BlocksPooler : IInitializable
{
    private Queue _blocks;

    public GameObject GetBlock() 
    {
        return new GameObject();
    }

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
