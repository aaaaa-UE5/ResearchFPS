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
        int one_rate = 5;
        int two_rate = 5;
        int three_rate = 5;
        int four_rate = 5;
        int five_rate = 5;
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
                    // Else startこっちが開始　上が中止

                    //m_squenceSaver = new SequencerServer();
                    m_squenceSaver.setduration();
                    //Debug.Log("TraningDuration is " + m_Targets[0].duration);
                    m_Wave = 0;
                    hits = 0;
                    misses = 0;
                    m_SequenceCoroutine = StartCoroutine(WaveStart(m_TimeBetweenWaves));
                    m_AudioSource.PlayOneShot(m_AudioStart, 0.25f);
                    one_rate = 5 + (-5 * NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_one_rate);
                    two_rate = 5 + (-5 * NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_two_rate);
                    three_rate = 5 + (-5 * NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_three_rate);
                    four_rate = 5 + (-5 * NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_four_rate);
                    five_rate = 5 + (-5 * NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_five_rate);
                    Debug.Log("one= "+one_rate);
                    Debug.Log("two= " + two_rate);
                    Debug.Log("three= " + three_rate);
                    Debug.Log("four= " + four_rate);
                    Debug.Log("five= " + five_rate);
                }

                m_ButtonCooldown = 3f;
            }
        }

        protected override IEnumerator WavePhase(float timer)
        {
            m_State = SequenceState.WavePhase;
            var group = m_Targets[m_Wave];

            yield return null;

            // Wait for step timer
            m_Timer = timer;
            while (m_Timer > 0f)
            {
                yield return null;
                m_Timer -= Time.deltaTime;
            }

            // Spawn targets
            if (group.randomise)
            {
                // Pick at random
                while (m_Spawned < m_TargetCount)
                {
                    //テストのそれぞれの的の命中率から偏った的の出現を行う為の計算
                    int range = UnityEngine.Random.Range(0, one_rate+two_rate+three_rate+four_rate+five_rate);
                    int i;
                    if (range <= one_rate)  i = 0;
                    else if(range <= one_rate + two_rate)  i = 1;
                    else if (range <= one_rate + two_rate + three_rate) i = 2;
                    else if (range <= one_rate + two_rate + three_rate + four_rate) i = 3;
                    else i = 4;
                    Debug.Log(range);
                    Debug.Log(i);

                    if (group.targets[i].hidden)
                    {
                        if (i == 0)
                        {
                            if (group.perStep == 2) group.targets[0].Popup(group.duration * 2);
                            else group.targets[0].Popup(group.duration);
                        }
                        else if(i == 1)
                        {
                            if (group.perStep == 2) group.targets[1].Popup(group.duration * 2);
                            else group.targets[1].Popup(group.duration);
                        }
                        else if (i == 2)
                        {
                            if (group.perStep == 2) group.targets[2].Popup(group.duration * 2);
                            else group.targets[2].Popup(group.duration);
                        }
                        else if (i == 3)
                        {
                            if (group.perStep == 2) group.targets[3].Popup(group.duration * 2);
                            else group.targets[3].Popup(group.duration);
                        }
                        else if (i ==4)
                        {
                            if (group.perStep == 2) group.targets[4].Popup(group.duration * 2);
                            else group.targets[4].Popup(group.duration);
                        }
                        ++m_Spawned;
                    }
                    else
                        yield return null; // Yield to prevent endless loop
                }
            }
            else
            {
                // Trigger sequentially
                while (m_Spawned < m_TargetCount)
                {
                    int index = m_Spawned;
                    while (index >= group.targets.Length)
                        index -= group.targets.Length;
                    if (group.targets[index] != null)
                        if (group.perStep == 2)
                            group.duration = group.duration * 2;
                    group.targets[index].Popup(group.duration);
                    ++m_Spawned;
                }
            }

            if (m_Spawned < group.total)
            {
                m_TargetCount += group.perStep;
                m_SequenceCoroutine = StartCoroutine(WavePhase(group.delay));
            }
            else
            {
                m_SequenceCoroutine = StartCoroutine(WaitForReset(m_Wave + 1 >= m_Targets.Length));
            }
        }
    }
}

