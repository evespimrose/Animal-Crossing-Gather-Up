using UnityEngine;

[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
    [Range(-0.1f, 0.1f)]
    public float curveStrength = 0f;

    [SerializeField]
    private Material[] materialsToAffect;

    private int m_CurveStrengthID;

    private void OnEnable()
    {
        m_CurveStrengthID = Shader.PropertyToID("_CurveStrength");
        SetCurveStrength(0f); // �⺻�� ����
    }

    private void Update()
    {
        UpdateMaterialsCurveStrength();
    }

    private void OnDisable()
    {
        ResetCurveStrength(); // Play ��� ���� �� ����
    }

    private void OnApplicationQuit()
    {
        ResetCurveStrength(); // ���ø����̼� ���� �� ����
    }

    private void UpdateMaterialsCurveStrength()
    {
        if (materialsToAffect != null)
        {
            foreach (Material mat in materialsToAffect)
            {
                if (mat != null)
                {
                    mat.SetFloat(m_CurveStrengthID, curveStrength);
                }
            }
        }
    }

    public void SetCurveStrength(float newStrength)
    {
        curveStrength = newStrength;
        UpdateMaterialsCurveStrength();
    }

    private void ResetCurveStrength()
    {
        curveStrength = 0f;
        if (materialsToAffect != null)
        {
            foreach (Material mat in materialsToAffect)
            {
                if (mat != null)
                {
                    mat.SetFloat(m_CurveStrengthID, 0f);
                }
            }
        }
    }
}
