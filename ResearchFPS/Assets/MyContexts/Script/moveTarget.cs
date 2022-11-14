using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTarget : MonoBehaviour
{
    float counter = 0;
    float move = 0.01f;
    // Start is called before the first frame update
    void Start()
    {

    }




    // Update is called once per frame
    void Update()
    {
        Vector3 p = new Vector3(move, 0, 0);
        transform.Translate(p);
        counter++;

        //count‚ª100‚É‚È‚ê‚Î-1‚ðŠ|‚¯‚Ä‹t•ûŒü‚É“®‚©‚·
        if (counter == 1000)
        {
            counter = 0;
            move *= -1;
        }
    }
}
