using UnityEngine;

public interface ITool
{
    ToolInfo ToolInfo { get; }
    void Execute();
}