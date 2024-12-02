using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : Tool
{
    private void Awake()
    {
        collectCommand = new AxeCollectCommand();
    }

}
