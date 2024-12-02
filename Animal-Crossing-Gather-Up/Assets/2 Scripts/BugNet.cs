using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugNet : Tool
{
    // TODO : BugPreFabData TryGetComponent 하면서 성공하면 Collect 발동
    private void Awake()
    {
        collectCommand = new BugNetCollectCommand();
    }

}
