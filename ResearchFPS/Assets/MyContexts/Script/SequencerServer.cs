using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeoFPS.Samples.SinglePlayer
{
    public class SequencerServer : MonoBehaviour
    {
        [SerializeField]
        private FiringRangeSequencer m_firingRangeSequencer = null;

        [SerializeField]
        private FiringRangeSequencer m_traingsequencer2 = null;

        private float duration = 2.0f;

        private void Update()
        {
            duration = m_firingRangeSequencer.get_duration();
            m_traingsequencer2.set_duration(duration);
        }

    }
}

