using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogOption : MonoBehaviour
{
    public string optionText;  // 옵션 텍스트
    public List<string> dialogTexts;  // 해당 옵션에 맞는 대화 텍스트
}
