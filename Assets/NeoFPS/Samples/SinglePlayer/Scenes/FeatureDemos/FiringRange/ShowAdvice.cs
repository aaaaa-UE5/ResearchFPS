using UnityEngine;

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

                if (SequencerServer.Instance.is_walking == true)
                {
                    messageText.text = "isWoking: " + showIsWalking + "\n";
                    messageText.color = Color.green;
                }
                else{
                    messageText.text = "isWoking: " + showIsWalking + "\n";
                    messageText.color = Color.red;
                }
                

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

