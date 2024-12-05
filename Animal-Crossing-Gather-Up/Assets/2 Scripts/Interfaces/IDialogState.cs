using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogState
{
    Coroutine currentCoroutine { get; }
}
