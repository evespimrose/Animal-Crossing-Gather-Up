using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandFlowerManager : BaseIslandManager
{
    public override void CatchBug(Bug bug)

    {
        int bugValue = bug.GetValue();

        //�κ��� ���������� �߰��ϴ� ����

        RemoveBug();
    }
}
