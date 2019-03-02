using UnityEngine;
using System.Collections;

public interface IEffectable
{
    string GetName();
    float GetValue();
    void AddModifier(float modifier);
    void RemoveModifier(float modifier);
}
