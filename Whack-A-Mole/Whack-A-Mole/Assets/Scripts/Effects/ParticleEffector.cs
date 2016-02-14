using UnityEngine;
using System.Collections;

public class ParticleEffector : MonoBehaviour {

    private float m_defaultSpeed;
    private float _speed = 1;
    private float m_speed { get { return _speed; }
                            set { _speed = value;  if (_speed > m_MaxSpeed) _speed = m_MaxSpeed; if (_speed < 0) _speed = 0; } }


    private float m_MaxSpeed;
    private float m_MinSpeed;
    private float m_Increment = 0.1f;

    private ParticleSystem m_particleSystem;

    public delegate void E_EditSpeed(bool _increase);
    public static event E_EditSpeed OnEditSpeed;




    //unity Lifecycle============================================================

    private void Awake()
    {
        ParticleEffector.OnEditSpeed += EditSpeed;
    }

    private void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();

       // m_defaultSpeed = GetComponent<ParticleSystem>().playbackSpeed;
       // m_speed = m_defaultSpeed;
       // m_MaxSpeed = m_defaultSpeed * 2;
       // m_MinSpeed = m_defaultSpeed / 2;
    }

    private void Update()
    {
       // Debug.Log(GetComponent<ParticleSystem>().playbackSpeed);
    }

    //-------------------------------------------------------------------------------------


    private void EditSpeed(bool increase)
    {
        if (increase)
            m_speed += m_Increment;
        else
            m_speed -= m_Increment;

       // m_particleSystem.playbackSpeed = m_speed;

    }

    public static void InitEditSpeed(bool _increase)
    {
        ParticleEffector.OnEditSpeed(_increase);
    }
}
