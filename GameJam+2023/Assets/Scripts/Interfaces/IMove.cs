using System;
using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public interface IMove
{
    float TimeToMove { get; set; }
    Vector2 CurrentPosition { get; set; }
    Vector2 TargetPosition { get; set; }
    IEnumerator Move(Vector2 direction);

}
