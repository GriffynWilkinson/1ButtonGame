using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput  {

    public delegate void HandleButtonPress();

    public event HandleButtonPress HandlePress;

    // checks to see if the buttonn is pressed, and handles events accordingly
    public void HandleInput()
    {
        // for now, our input will be the 'f' key
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (HandlePress != null)
            {
                HandlePress();
            }
        }
    }

}
