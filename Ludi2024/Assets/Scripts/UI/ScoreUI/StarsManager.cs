using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class StarsManager : MonoBehaviour
{
    [SerializeField] private Star[] m_Stars;

    [SerializeField] private float m_EnlargeScale = 1.5f;
    [SerializeField] private float m_ShrinkScale = 1.0f;
    [SerializeField] private float m_EnlargeDuration = 0.25f;
    [SerializeField] private float m_ShrinkDuration = 0.25f;

    private void OnEnable()
    {
        GameEvents.OnShowStars += ShowStars;
    }

    private void OnDisable()
    {
        GameEvents.OnShowStars -= ShowStars;
    }

    private void Update()
    {
        if (InputManager.Instance.Enter.Tap)
        {
            ShowStars(3);
        }
    }

    private void ShowStars(int p_numberOfStars)
    {
        StartCoroutine(ShowStarsRoutine(p_numberOfStars));
    }

    private IEnumerator ShowStarsRoutine(int p_numberOfStars)
    {
        foreach (var t_star in m_Stars)
        {
            t_star.SetStarLocalScale(Vector3.zero);
        }

        for (int i = 0; i < p_numberOfStars; i++)
        {
            yield return StartCoroutine(EnlargeAndShrinkStar(m_Stars[i]));
        }
    }

    private IEnumerator EnlargeAndShrinkStar(Star p_star)
    {
        yield return StartCoroutine(ChangeStarScale(p_star, m_EnlargeScale, m_EnlargeDuration));
        yield return StartCoroutine(ChangeStarScale(p_star, m_ShrinkScale, m_ShrinkDuration));
    }

    private IEnumerator ChangeStarScale(Star p_star, float p_targetScale, float p_duration)
    {
        Vector3 l_initialScale = p_star.GetImage().transform.localScale;
        Vector3 l_finalScale = new Vector3(p_targetScale, p_targetScale, p_targetScale);

        float l_elapseTime = 0.0f;

        while (l_elapseTime < p_duration)
        {
            p_star.SetStarLocalScale(Vector3.Lerp(l_initialScale, l_finalScale, l_elapseTime / p_duration));
            l_elapseTime += Time.deltaTime;
            yield return null;
        }

        p_star.SetStarLocalScale(l_finalScale);
    }
}
