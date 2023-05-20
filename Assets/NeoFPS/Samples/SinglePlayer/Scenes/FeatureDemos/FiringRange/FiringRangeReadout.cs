using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeoFPS.Samples.SinglePlayer
{
	[RequireComponent (typeof (Text))]
	[HelpURL("https://docs.neofps.com/manual/samplesref-mb-firingrangereadout.html")]
	public class FiringRangeReadout : MonoBehaviour
	{
		const string k_ReadoutString = "Score: {0}";

		private Text m_ReadoutText = null;
        private int m_Hits = 0;
        private int m_Misses = 0;

        protected void Awake ()
		{
			m_ReadoutText = GetComponent<Text> ();
            m_ReadoutText.text = string.Format(k_ReadoutString, 0);
        }

		public void OnHitsChanged (int total)
		{
			//Debug.Log("hit=" + SequencerServer.Instance.score);
			m_Hits = SequencerServer.Instance.score;
			m_ReadoutText.text = string.Format (k_ReadoutString, SequencerServer.Instance.score);
		}

		public void OnMissesChanged (int total)
		{
			//Debug.Log("miss=" + SequencerServer.Instance.score);
			m_Misses = total;
			m_ReadoutText.text = string.Format (k_ReadoutString, SequencerServer.Instance.score);
		}
	}
}