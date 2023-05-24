using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class is_target_aiming : MonoBehaviour
{
    private string objName;
    private string targetobjName = "Prop_FiringRangeBuilding_SliderTarget";

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        objName = other.gameObject.name;
        if (NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_traning == true && objName.Contains(targetobjName))
        {

            foreach (int num in NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.popupingTargetnum)
            {

                //Debug.Log(num);
                if (objName.Contains((num + 1).ToString()))
                {
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.aiming = true;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        objName = other.gameObject.name;
        if (NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.is_traning == true && objName.Contains(targetobjName))
        {

            foreach (int num in NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.popupingTargetnum)
            {

                //Debug.Log(num);
                if (objName.Contains((num + 1).ToString()))
                {
                    NeoFPS.Samples.SinglePlayer.SequencerServer.Instance.aiming = false;
                }
            }

        }

    }
}
