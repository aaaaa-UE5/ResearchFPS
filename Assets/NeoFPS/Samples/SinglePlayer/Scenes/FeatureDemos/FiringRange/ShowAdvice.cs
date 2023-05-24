using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using NeoSaveGames.Serialization;
using NeoSaveGames;
using NeoFPS.ModularFirearms;

namespace NeoFPS.Samples.SinglePlayer
{
    public class ShowAdvice : ShowScore
    {

        [SerializeField]
        private GameObject showAD;

        private string showIsWalking = "";
        private string showIsAiming = "";

        protected override void Update()
        {
            if(NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_traning == true)
            {
                showIsWalking = NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_walking.ToString();
                showIsAiming = NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.aiming.ToString();

                messageText.text = "isWoking: " + showIsWalking + "\n" + 
                    "isAniming: " + showIsAiming;

            }
            else
            {
                finish_traing();
            }

        }

        public override void SetMessagePanel(string message)
        {
            showAD.SetActive(true);
            showIsWalking = NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_walking.ToString();
            showIsAiming = NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.aiming.ToString();

        }

        public override void finish_traing()
        {
            showAD.SetActive(false);
        }

    }
}

