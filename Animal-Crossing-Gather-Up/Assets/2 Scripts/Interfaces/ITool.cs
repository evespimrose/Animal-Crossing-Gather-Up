using UnityEngine;

public interface ITool
{
    ToolInfo ToolInfo { get; }

    void Execute(Vector3 position, Vector3 foward);
}