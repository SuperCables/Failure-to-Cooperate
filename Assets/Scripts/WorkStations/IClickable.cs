using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    void LeftClick(Vector3 pos);
    void RightClick(Vector3 pos);
}

public interface IClickHoldable
{
    void LeftHold(Vector3 pos);
    void RightHold(Vector3 pos);
}

public interface IScrollable
{
    void Scroll(int ammount);
}

