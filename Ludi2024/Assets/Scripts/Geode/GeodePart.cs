using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeodePart : MonoBehaviour
{
    enum KeyPointType{
        Real,
        Fake
    }

    [Header("Geode Settings")]
    [SerializeField] private KeyPointType m_KeyPointType;

    [Header("Components")]
    [SerializeField] private GameObject m_RealParticlesPrefab;
    [SerializeField] private GameObject m_FakeParticlesPrefab;
    [SerializeField] private Animator m_Animator;

    private ParticleSystem m_Particles;

    private void Start()
    {
        GameObject l_particles = Instantiate(m_KeyPointType == KeyPointType.Real ? m_RealParticlesPrefab : m_FakeParticlesPrefab,
            transform.position, transform.rotation, transform);

        m_Particles = l_particles.GetComponent<ParticleSystem>();
        m_Particles.Play();
    }

    private void HideParticles()
    {
        m_Particles.Stop();
    }

    public void OnKeyPointClicked()
    {
        HideParticles();
        m_Animator.SetTrigger("Fall");
        Destroy(gameObject);
    }
}
