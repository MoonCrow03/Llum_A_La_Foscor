using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletPoint : MonoBehaviour
{
    private TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        m_Text.text = string.Empty;
    }

    public bool IsEmpty()
    {
        return m_Text.text is "";
    }

    public void SetText(string p_text)
    {
        m_Text.text = "- " + p_text;
    }
}
