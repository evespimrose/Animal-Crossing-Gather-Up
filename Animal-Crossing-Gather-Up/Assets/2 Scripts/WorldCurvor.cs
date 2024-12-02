using UnityEngine;

[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
    [Range(-0.1f, 0.1f)]
    public float curveStrength = 0.0134f;

    [SerializeField]
    private Material[] materialsToAffect;

    private int m_CurveStrengthID;

    private void OnEnable()
    {
        m_CurveStrengthID = Shader.PropertyToID("_CurveStrength");
    }

    private void Update()
    {
        UpdateMaterialsCurveStrength();
    }

    // 상태 변경 메서드
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

    // 외부에서 접근 가능한 메서드
    public void SetCurveStrength(float newStrength)
    {
        curveStrength = newStrength;
        UpdateMaterialsCurveStrength();
    }
}
