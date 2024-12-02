using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandFlowerManager : BaseIslandManager
{
    public override void CatchBug(Bug bug)

    {
        int bugValue = bug.GetValue();

        //인벤에 버그인포를 추가하는 로직

        RemoveBug();
    }
}
