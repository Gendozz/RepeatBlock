using System;
using UnityEngine;

public class DirectionDeterminant
{
    private int _oneDirectionCounter = 0;

    private int _maxOneSideSteps = 3;

    private Direction _previousDirection;

    public DirectionDeterminant()
    {
        _previousDirection = (Direction)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Direction)).Length);
    }

    public Direction GetNextDirection()                 // TODO: Change algorythm. This doesn't change direction after limit in row
    {
        Direction newDirection = GetRandomDirection();

        if (!newDirection.Equals(_previousDirection))
        {            
            _oneDirectionCounter = 1;
            return newDirection;
        }
        else
        {
            _oneDirectionCounter++;
        }

        //Debug.Log($"_oneDirectionCounter = {_oneDirectionCounter}");


        if (_oneDirectionCounter >= _maxOneSideSteps)
        {
            //Debug.Log($"Direction was {newDirection}");
            newDirection = (Direction)Mathf.Abs((int)newDirection - 1);
            //Debug.Log($"Direction is {newDirection}");
            _oneDirectionCounter = 1;
        }

        return newDirection;
    }

    private Direction GetRandomDirection()
    {
        return (Direction)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Direction)).Length);
    }
}
