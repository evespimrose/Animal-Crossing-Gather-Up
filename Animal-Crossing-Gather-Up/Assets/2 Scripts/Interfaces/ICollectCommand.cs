using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectCommand
{
    void Execute();
}

public class NetCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Debug.Log("���ڸ�ä ��� ä��!");
    }
}

public class FishingRodCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Debug.Log("���˴� ��� ä��!");
    }
}

public class AxeCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Debug.Log("���� ��� ä��!");
    }
}

