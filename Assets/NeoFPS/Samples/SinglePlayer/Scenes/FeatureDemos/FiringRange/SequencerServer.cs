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
        private int walkingScore = 0;
        public int shootCount = 0;
        public int notStopingShoot = 0;
        public int firstShootRate = 0;
        public int shootedAmmo = 0;

        [SerializeField]
        private FiringRangeSequencer m_firingRangeSequencer = null;

        [SerializeField]
        private FiringRangeSequencer m_traingsequencer2 = null;

        //スコアボード
        [SerializeField]
        private  ShowScore m_showScore = null;


        public float duration = 2.0f;
        public bool is_walking = false;
        public bool testStart = false;
        public bool targethit = false;
        int count = 0;

        public int score = 0;

        public void getduration(float testduration)
        {
            duration = testduration;
            //Debug.Log("score= " + score);
            //Debug.Log("duration= " + duration);
            //Debug.Log(T_one_rate);
            // Debug.Log(T_two_rate);
            // Debug.Log(T_three_rate);
            //Debug.Log(T_four_rate);
            //Debug.Log(T_five_rate);
            m_showScore.SetMessagePanel(
                "score= " + score + "\n" + "duration= " + duration + "\n" +
                "WakingScore= " + walkingScore + "\n" + "notStopingShoot= " + notStopingShoot + "\n" +
                "FirstShooRate= " + firstShootRate + "\n" + "ShootedAmmo= " + shootedAmmo + "\n\n" + "閉じるにはEnterを押しやす" + "\n" + "(you can close to push Enter Key)"

                ); 

        }

        public void setduration()
        {
            m_traingsequencer2.set_duration(duration);
        }
        private void FixedUpdate()
        {
            //Debug.Log(targethit);
            if (testStart == true && is_walking == true)
            {
                count += 1;
                if(count >= 10){
                    score += 1;
                    walkingScore += 1;
                    count = 0;
                }
            }
            
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
