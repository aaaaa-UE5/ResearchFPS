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
        protected override void increaseDuration()
        {
            Debug.Log("increase");
        }

        protected override void reduceDuration()
        {
            Debug.Log("reduce");
        }
    }
}

