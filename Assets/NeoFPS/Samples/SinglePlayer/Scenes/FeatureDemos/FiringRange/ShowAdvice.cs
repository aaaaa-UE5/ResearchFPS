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

        private string showtext = "";

        protected override void Update()
        {
            showtext = NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_walking.ToString();
            messageText.text = "isWoking: " + showtext;
        }

        public override void SetMessagePanel(string message)
        {
            showAD.SetActive(true);
            showtext = NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_walking.ToString();
        }

        public override void finish_traing()
        {
            showAD.SetActive(false);
        }

    }
}

