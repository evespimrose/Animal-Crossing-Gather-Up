using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugNet : Tool
{
    // TODO : BugPreFabData TryGetComponent �ϸ鼭 �����ϸ� Collect �ߵ�
    private void Awake()
    {
        collectCommand = new BugNetCollectCommand();
    }

}
