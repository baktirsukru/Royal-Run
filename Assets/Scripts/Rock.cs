using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 10f;
    CinemachineImpulseSource cinemachineImpulseSource;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void OnCollisionEnter (Collision other)
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }
}
