using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CShake : MonoBehaviour
{
    public static CShake instance;

    private CinemachineVirtualCamera cam;
    private float tiempomovimiento;
    private float tiempomovimientotoal;

    private float intensidadInicial; 
    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;

    private void Awake()
    {
        instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
        m_MultiChannelPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCam(float intensity, float frecuency, float time)
    {
        m_MultiChannelPerlin.m_AmplitudeGain = intensity;
        m_MultiChannelPerlin.m_FrequencyGain = frecuency;
        intensidadInicial = intensity;
        tiempomovimiento = time;
        tiempomovimientotoal = time;

    }

    // Update is called once per frame
    void Update()
    {
        if(tiempomovimiento > 0)
        {
            tiempomovimiento -= Time.deltaTime;
            m_MultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(intensidadInicial,0, 1-(tiempomovimiento/tiempomovimientotoal));
        }
    }
}
