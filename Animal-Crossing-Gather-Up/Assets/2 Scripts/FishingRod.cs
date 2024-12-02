using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Tool
{
    private void Awake()
    {
        collectCommand = new FishingRodCollectCommand();
    }

}
