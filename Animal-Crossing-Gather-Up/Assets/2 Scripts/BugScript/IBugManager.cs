using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBugManager 
{
    bool CanSpawnBug();
    void AddBug();
    void RemoveBug();
    void CatchBug(Bug bug);
}
