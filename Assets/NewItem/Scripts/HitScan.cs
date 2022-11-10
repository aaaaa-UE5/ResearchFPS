using NeoFPS;
using NeoSaveGames.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScan : MonoBehaviour, IDamageHandler
{
    [SerializeField, Tooltip("The damage threshold for the target to drop and register as a hit.")]
    private float m_DamageThreshold = 1f;

    [SerializeField, Tooltip("The duration the target will be visible. If the target is not hit in this time it registers as a miss.")]
    private float m_PopupDuration = 0.5f;

    [SerializeField, Tooltip("The axis to rotate the target around when it pops up.")]
    private Vector3 m_RotationAxis = new Vector3(1f, 0f, 0f);

    [SerializeField, Tooltip("The rotation of the target around the specified axis when it is fully hidden.")]
    private float m_HiddenRotation = 180f;

    [SerializeField, Tooltip("The rotation of the target around the specified axis when it is fully hidden.")]
    private float m_HiddenTransform = 1f;

    private Collider m_Collder = null;
    private Transform m_RotationTransform = null;
    private float m_Lerp = 0f;
    private float m_Timer = 0f;
    bool test = false;
    private bool m_Hit { get { return test; } set { test = value; } }
    private TargetState m_State = TargetState.Idle;

    enum TargetState
    {
        Idle,
        Raising,
        Raised,
        Lowering
    }

    protected bool initialised
    {
        get;
        private set;
    }

    public IHealthManager healthManager
    {
        get { return null; }
    }

    public bool hit
    {
        get { return m_Hit; }
    }

    private DamageFilter m_InDamageFilter = DamageFilter.AllDamageAllTeams;
    public DamageFilter inDamageFilter
    {
        get { return m_InDamageFilter; }
        set { m_InDamageFilter = value; }
    }

    public DamageResult AddDamage(float damage)
    {
        return AddDamage(damage, null);
    }

    public DamageResult AddDamage(float damage, IDamageSource source)
    {
        if (damage >= m_DamageThreshold && !hit)
        {
            m_Hit = true;

            // Report damage dealt
            //if (damage > 0f && source != null && source.controller != null)
            //    source.controller.currentCharacter.ReportTargetHit(false);

            DamageEvents.ReportDamageHandlerHit(this, source, m_Collder.bounds.center, DamageResult.Standard, damage);
        }
        return DamageResult.Standard;
    }

    public DamageResult AddDamage(float damage, RaycastHit raycasthit)
    {
        return AddDamage(damage, raycasthit, null);
    }

    public DamageResult AddDamage(float damage, RaycastHit raycasthit, IDamageSource source)
    {
        
        if (damage >= m_DamageThreshold && !hit)
        {
            
            m_Hit = true;

            // Report damage dealt
            //if (damage > 0f && source != null && source.controller != null)
            //    source.controller.currentCharacter.ReportTargetHit(false);

            DamageEvents.ReportDamageHandlerHit(this, source, raycasthit.point, DamageResult.Standard, damage);
        }
        return DamageResult.Standard;
    }

    void Start()
    {   
        m_RotationTransform = transform;
        m_Collder = GetComponent<Collider>();

        if (!initialised)
        {
            m_RotationTransform.localRotation = Quaternion.AngleAxis(m_HiddenRotation, m_RotationAxis);
            m_Hit = false;
            initialised = true;
        }
    }
}
