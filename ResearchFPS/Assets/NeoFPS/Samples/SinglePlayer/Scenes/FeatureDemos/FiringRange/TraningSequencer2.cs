using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using NeoSaveGames.Serialization;
using NeoSaveGames;
using UnityEditor;

namespace NeoFPS.Samples.SinglePlayer
{
    public class TraningSequencer2 : FiringRangeSequencer
    {
        public override void set_duration(float aaa)
        {
            foreach (var a in m_Targets)
            {
                a.duration = aaa;
            }
        }

        protected override void increaseDuration()
        {
            //Debug.Log("increase");
        }

        protected override void reduceDuration()
        {
           //Debug.Log("reduce");
        }

        public override void OnButtonPush()
        {
            if (m_ButtonCooldown <= 0f)
            {
                // If ongoing, stop
                if (m_SequenceCoroutine != null)
                {
                    if (m_State != SequenceState.Reset)
                    {
                        StopAllCoroutines();
                        m_SequenceCoroutine = StartCoroutine(ResetTargets());
                        m_AudioSource.PlayOneShot(m_AudioCancel, 0.25f);
                    }
                }
                else
                {
                    // Else start‚±‚Á‚¿‚ªŠJŽn@ã‚ª’†Ž~

                    //m_squenceSaver = new SequencerServer();
                    m_squenceSaver.setduration();
                    Debug.Log("TraningDuration is " + m_Targets[0].duration);
                    m_Wave = 0;
                    hits = 0;
                    misses = 0;
                    m_SequenceCoroutine = StartCoroutine(WaveStart(m_TimeBetweenWaves));
                    m_AudioSource.PlayOneShot(m_AudioStart, 0.25f);
                }

                m_ButtonCooldown = 3f;
            }
        }
    }
}

