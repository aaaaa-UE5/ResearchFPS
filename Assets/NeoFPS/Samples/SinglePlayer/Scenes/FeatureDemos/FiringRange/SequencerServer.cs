using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using NeoSaveGames.Serialization;
using NeoSaveGames;
using NeoFPS.ModularFirearms;

namespace NeoFPS.Samples.SinglePlayer 
{
    
    public class SequencerServer: SingletonMonoBehaviour<SequencerServer>
    {
        public string objectname = null;
        public int T_one_rate;
        public int T_two_rate;
        public int T_three_rate;
        public int T_four_rate;
        public int T_five_rate;

        [SerializeField]
        private FiringRangeSequencer m_firingRangeSequencer = null;

        [SerializeField]
        private FiringRangeSequencer m_traingsequencer2 = null;

        //歩いているかの情報取得
        [SerializeField]
        private ModularFirearm m_modularfirearm = null;


        public float duration = 2.0f;
        public bool is_walking = false;

        public void getduration(float testduration)
        {
            duration = testduration;
            Debug.Log(duration);
        }

        public void setduration()
        {
            m_traingsequencer2.set_duration(duration);
        }
        private void FixedUpdate()
        {
            Debug.Log(is_walking);
        }


    }
}

/*
namespace NeoFPS.ModularFirearms
{

    public class SequencerServer2 : SingletonMonoBehaviour<SequencerServer2>
    {
        public string objectname = null;
        public int T_one_rate;
        public int T_two_rate;
        public int T_three_rate;
        public int T_four_rate;
        public int T_five_rate;

        //歩いているかの情報取得
        [SerializeField]
        private ModularFirearm m_modularfirearm = null;

        public bool walking = false;

        public void get_is_waking(bool is_waking)
        {
            walking = is_waking;
            Debug.Log(walking);
        }


    }
}
*/
