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
        public double T_one_rate;
        public double T_two_rate;
        public double T_three_rate;
        public double T_four_rate;
        public double T_five_rate;
        private int walkingScore = 0;
        public int shootCount = 0; // 的を打つのに使った弾(1wave)
        public bool targethit = false; // shootcountを正しく(的に当てる前の弾を)数えるための変数
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

        //アドバイス表示
        [SerializeField]
        private ShowScore m_showAdvice = null;


        public float duration = 2.0f;
        public bool is_walking = false;
        public bool testStart = false;
        public bool is_traning = false; 
        public bool aiming = false; // トレーニングで標準が的に合っているか判別
        int count = 0; //  walkingscoreをカウントするために必要
        public List<int> popupingTargetnum = new List<int>(); // wava間で上がっているターゲットの番号を取得

        public int score = 0;

        public void Check()
        {

        }
        private string bad_feature (int score) //ここにスコアと特徴の数値を比較して悪い特徴に関するアドバイスを返す。
        {
            string bad_feature = "";

            if (score >= 700) bad_feature = "goodscore"; //  ここからとりあえずのサンプル
            else bad_feature = "not good score";

            return bad_feature; //  アドバイスを返す予定orもっかいアドバイス作成する関数を作成してそっちに投げる
        }

        public void getduration(float testduration) //  テスト側で最後に起動、テストの値を渡すならここ。初期化もできればここ※値をトレーニングに渡すならトレーニングのボタンプッシュで初期化推奨
        {
            duration = testduration;
            //Debug.Log("score= " + score);
            //Debug.Log("duration= " + duration);

            //Debug.Log(T_one_rate);
            //Debug.Log(T_two_rate);
            //Debug.Log(T_three_rate);
            //Debug.Log(T_four_rate);
            //Debug.Log(T_five_rate);

            T_one_rate = Math.Floor((double)T_one_rate * 10d) / 10d;
            T_two_rate = Math.Floor((double)T_two_rate * 10d) / 10d;
            T_three_rate = Math.Floor((double)T_three_rate * 10d) / 10d;
            T_four_rate = Math.Floor((double)T_four_rate * 10d) / 10d;
            T_five_rate = Math.Floor((double)T_five_rate * 10d) / 10d;

            score -= notStopingShoot*5;
            score += firstShootRate * 10;

            string adword = bad_feature(score);

            m_showScore.SetMessagePanel(
                "score= " + score + "\n" + "duration= " + (double)Math.Floor(duration * 100) / 100 + "\n" +
                "WakingScore= " + walkingScore + "\n" + "notStopingShoot= " + notStopingShoot + "\n" +
                "FirstShootRate= " + firstShootRate + "\n" + "ShootedAmmo= " + shootedAmmo + "\n" +
                "each target accuracy= " + "\n" + T_one_rate + "% " + T_two_rate + "% " + T_three_rate + "% " + T_four_rate + "% " + T_five_rate + "% " + "\n" +
                "advaice→" + adword + "\n\n" +
                "閉じるにはEnterを押しやす" + "\n" + "(you can close to push Enter Key)"

                ) ;
            score = 0;
            walkingScore = 0;
            notStopingShoot = 0;
            firstShootRate = 0;
            shootCount = 0;
            shootedAmmo = 0;
            if(is_traning == true)
            {
                m_showAdvice.finish_traing();
            }
            

        }

        public void setduration() //  トレーニング側で起動、トレーニングに値与えるならここ
        {
            m_traingsequencer2.set_duration(duration);
            m_showAdvice.SetMessagePanel(is_walking.ToString()); //  完成図は悪い特徴のboolを送ること。サンプルとして今はwalking 
        }
        private void FixedUpdate()
        {
            //Debug.Log(targethit);
            if (testStart == true && is_walking == true)
            {
                count += 1;
                if(count >= 10){
                    score += 1;
                    walkingScore += 2;
                    count = 0;
                }
            }
            
        }


    }
}
