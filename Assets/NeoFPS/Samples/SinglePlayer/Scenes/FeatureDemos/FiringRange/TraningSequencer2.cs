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
        double one_rate = 5;
        double two_rate = 5;
        double three_rate = 5;
        double four_rate = 5;
        double five_rate = 5;

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
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.testStart = false;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_traning = false;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootedAmmo = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootCount = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.notStopingShoot = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_one_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_two_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_three_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_four_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_five_rate = 0;
                }
                else
                {
                    // Else startこっちが開始　上が中止

                    //m_squenceSaver = new SequencerServer();
                    m_squenceSaver.setduration(); //  ここでテストで計測したdurationを設定

                    //Debug.Log("TraningDuration is " + m_Targets[0].duration);
                    m_Wave = 0;
                    hits = 0;
                    misses = 0;
                    m_SequenceCoroutine = StartCoroutine(WaveStart(m_TimeBetweenWaves));
                    m_AudioSource.PlayOneShot(m_AudioStart, 0.25f);

                    one_rate = (100 - NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_one_rate);
                    two_rate = (100 - NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_two_rate);
                    three_rate = (100 - NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_three_rate);
                    four_rate = (100 - NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_four_rate);
                    five_rate = (100 - NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_five_rate);

                    //NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.testStart = true;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_traning = true;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootedAmmo = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootCount = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.notStopingShoot = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_one_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_two_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_three_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_four_rate = 0;
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_five_rate = 0;
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
                    int rangeamount = (int)(one_rate + two_rate + three_rate + four_rate + five_rate);
                    int range = UnityEngine.Random.Range(0, (rangeamount));
                    int i;
                    if (range <= one_rate) i = 0;
                    else if (range <= one_rate + two_rate) i = 1;
                    else if (range <= one_rate + two_rate + three_rate) i = 2;
                    else if (range <= one_rate + two_rate + three_rate + four_rate) i = 3;
                    else i = 4;

                    if (group.targets[i].hidden)
                    {
                        if (i == 0)
                        {
                            if (group.perStep == 2) group.targets[0].Popup(group.duration * 2);
                            else group.targets[0].Popup(group.duration);
                        }
                        else if (i == 1)
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
                        else if (i == 4)
                        {
                            if (group.perStep == 2) group.targets[4].Popup(group.duration * 2);
                            else group.targets[4].Popup(group.duration);
                        }
                        ++m_Spawned;
                        NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.popupingTargetnum.Add(i);
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

        protected override IEnumerator WaitForReset(bool completed)
        {
            if (completed)
                m_State = SequenceState.Reset;
            else
                m_State = SequenceState.Waiting;

            yield return null;

            bool allTargetsHidden = false;
            while (!allTargetsHidden)
            {
                yield return null;

                allTargetsHidden = true;
                for (int i = 0; i < m_Targets[m_Wave].targets.Length; ++i)
                {
                    FiringRangeTarget t = m_Targets[m_Wave].targets[i];
                    if (t != null && !t.hidden)
                    {
                        allTargetsHidden = false;
                        break;
                    }
                }
            }

            // Reset the sequence if completed or start next wave　終わりなら終わり、まだウェーブあるなら次のウェーブにいく部分
            if (completed)
            {

                m_Wave = 0;
                m_State = SequenceState.Stopped;
                m_SequenceCoroutine = null;
                //それぞれの的の命中率
                //Debug.Log(T_hit_one);
                //Debug.Log(T_one);
                //Debug.Log(T_hit_two);
                //Debug.Log(T_two);
                //Debug.Log(T_five);

                if (T_one != 0)
                {
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_one_rate = (double)T_hit_one / (double)T_one * 100d;
                    //Debug.Log(NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_one_rate);
                }
                if (T_two != 0)
                {
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_two_rate = (double)T_hit_two / (double)T_two * 100d;
                    //Debug.Log(NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_two_rate);
                }
                if (T_three != 0)
                {
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_three_rate = (double)T_hit_three / (double)T_three * 100d;
                    //Debug.Log(NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_three_rate);
                }
                if (T_four != 0)
                {
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_four_rate = (((double)T_hit_four / (double)T_four) * 100d);
                    //Debug.Log(NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_four_rate);
                }
                if (T_five != 0)
                {
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_five_rate = (((double)T_hit_five / (double)T_five) * 100d);
                    //Debug.Log(NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.T_five_rate);
                }

                ///テスト内容＆処理が終わった後、durationをサーバーに送る
                //float sentduration = get_duration();
                //m_squenceSaver.getduration(sentduration);
                NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.testStart = false;
                NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_traning = false;

            }
            else
            {
                ++m_Wave;
                m_SequenceCoroutine = StartCoroutine(WaveStart(m_TimeBetweenWaves));

                NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.targethit = false;
                //Debug.Log(NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootCount);
                NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootedAmmo += NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootCount;
                NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.shootCount = 0;
                NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.popupingTargetnum.Clear();
            }
        }

    }
}

