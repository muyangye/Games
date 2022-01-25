using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{

    [SerializeField]
    Text textComponent;

    [SerializeField]
    State startingState;

    State state;

    bool hoe;
    bool sword;

    // Start is called before the first frame update
    void Start()
    {
        state = startingState;
        textComponent.text = state.GetStateStory();
        hoe = false;
        sword = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        ManageState();
    }

    private void ManageState()
    {
        var nextStates = state.GetNextStates();
        // If the player dies
        if (state.GetID() == "DD")
        {
            // Reset sword and hoe
            sword = false;
            hoe = false;
        }
        // End of the game
        else if (state.GetID() == "GE" || state.GetID() == "BE")
        {
            // Reset sword and hoe
            sword = false;
            hoe = false;
            if (Input.anyKeyDown)
            {
                state = nextStates[0];
            }
        }
        for (int i = 0; i < nextStates.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (i == 0)
                {
                    // Player may collect sword or hoe in scene B
                    if (state.GetID() == "B1")
                    {
                        sword = true;
                    }
                    else if (state.GetID() == "B2")
                    {
                        hoe = true;
                    }
                    state = nextStates[i];
                }
                else if (i == 1)
                {
                    // Some states can only be entered using hoe and/or sword 
                    if (state.GetID() == "C1")
                    {
                        if (hoe)
                        {
                            state = nextStates[i];
                        }
                    }
                    else if (state.GetID() == "E1")
                    {
                        if (sword)
                        {
                            state = nextStates[i];
                        }
                    }
                    else
                    {
                        state = nextStates[i];
                    }
                }
                else
                {
                    state = nextStates[i];
                } 
            }
        }
        textComponent.text = state.GetStateStory();
    }

}
