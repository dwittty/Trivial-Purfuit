using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpdate()
    {

        RuleController rc = new RuleController();

        rc.receiveAnswer(2);
        int ans = rc.getAnswer();
        


    }
}
