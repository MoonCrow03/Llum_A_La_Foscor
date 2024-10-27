using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;
    private string m_EmptyText = "- ";

    public bool IsEmpty()
    {
        return m_Text.text == m_EmptyText;
    }

    public void SetText(string p_text)
    {
        m_Text.text = m_EmptyText + p_text;
    }

    public void ClearText()
    {
        m_Text.text = string.Empty;
    }
}
