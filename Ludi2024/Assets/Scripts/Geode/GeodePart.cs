using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeodePart : MonoBehaviour
{
    private enum KeyPointType{
        Real,
        Fake
    }

    [Header("Geode Settings")]
    [SerializeField] private KeyPointType m_KeyPointType;
    [SerializeField] private float m_ForceAmount = 10.0f;
    [SerializeField] private float m_LifeTime = 1.5f;
    [SerializeField] private float m_VibrationIntensity = 0.05f;
    [SerializeField] private float m_VibrationDuration = 0.5f;
    [SerializeField] private float m_VibrationSpeed = 50.0f;

    [Header("Components")]
    [SerializeField] private GameObject m_RealParticlesPrefab;
    [SerializeField] private GameObject m_FakeParticlesPrefab;

    private ParticleSystem m_Particles;

    public static Action OnStrike;
    public static Action OnHit;

    private void Start()
    {
        GameObject l_particles = Instantiate(m_KeyPointType == KeyPointType.Real ? m_RealParticlesPrefab : m_FakeParticlesPrefab,
            transform.position, transform.rotation, transform);

        m_Particles = l_particles.GetComponent<ParticleSystem>();
        m_Particles.Play();
    }
    public void OnKeyPointClicked()
    {
        if (m_KeyPointType == KeyPointType.Real)
        {
            OnHit?.Invoke();
            m_Particles.Stop();
            StartCoroutine(AnimateGeode());
            Destroy(gameObject);
        }

        if (m_KeyPointType == KeyPointType.Fake)
        {
            OnStrike?.Invoke();
            StartCoroutine(Vibrate());
        }
    }

    private IEnumerator AnimateGeode()
    {
        GameObject l_geode = transform.parent.gameObject;
        Rigidbody l_rigidbody = l_geode.GetComponent<Rigidbody>();

        l_rigidbody.isKinematic = false;
        l_rigidbody.AddForce(Vector3.up * m_ForceAmount, ForceMode.Impulse);

        yield return new WaitForSeconds(m_LifeTime);

        Destroy(l_geode);
    }

    private IEnumerator Vibrate()
    {
        Vector3 l_originalPosition = transform.parent.position;
        float l_elapsedTime = 0.0f;
        
        while (l_elapsedTime < m_VibrationDuration)
        {
            // Randomly change the position within a small range to simulate vibration
            Vector3 l_randomPosition = l_originalPosition + (Random.insideUnitSphere * m_VibrationIntensity);

            transform.parent.position = l_randomPosition;

            // Wait for the next frame
            l_elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the position back to the original
        transform.parent.position = l_originalPosition;
    }
}
