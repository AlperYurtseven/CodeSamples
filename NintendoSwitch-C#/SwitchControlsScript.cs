using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_SWITCH
using nn.hid;
#endif

/*
    Nintendo Switch Joy-Con Buttons Control Script

    Created By : Alper Yurtseven
    Created On : 23.12.2021
*/

public class SwitchControlsScript : MonoBehaviour
{
#if UNITY_SWITCH
    private NpadId npadId = NpadId.Invalid;
    private NpadStyle npadStyle = NpadStyle.Invalid;
    private NpadState npadState = new NpadState();
#endif

    // All the buttons on Nintendo Switch console.
    // We need to block its interaction when the scene is loaded, or when they pressed, since switch takes it as continous input.
    // If continous input needed like throttle or break in car, dont use these.
    // In this script we will deactivate it when pressed and activate them when released.

    bool A_interactable = false;
    bool B_interactable = false;
    bool Y_interactable = false;
    bool X_interactable = false;

    bool L_interactable = false;
    bool R_interactable = false;
    bool ZL_interactable = false;
    bool ZR_interactable = false;

    bool Plus_interactable = false;
    bool Minus_interactable = false;

    bool Up_interactable = false;
    bool Down_interactable = false;
    bool Left_interactable = false;
    bool Right_interactable = false;

    bool SL_left_interactable = false;
    bool SL_right_interactable = false;
    bool SR_left_interactable = false;
    bool SR_right_interactable = false;

    // Time will use to block continous input from use when stick in use.

    float timeLeftStick = 0.0f;
    float timeRightStick = 0.0f;
    float timeGeneral = 0.0f;
    float timeSceneLoad = 0.0f;

    // Thresholds for Left and Right Stick, will take input if delta time is bigger than this.

    float thresholdLeftStick = 0.3f;
    float thresholdRightStick = 0.3f;

    // Boolean values will be used during Update Function to decrease workload.

    bool buttonsEnabledAfterSceneLoad = false;

    // Start is called before the first frame update
    void Start()
    {

        // Just in case we will call this function to make them not interactable at scene load
        MakeAllNotInteractable();

#if UNITY_SWITCH
        Npad.Initialize();
        Npad.SetSupportedIdType(new NpadId[] { NpadId.Handheld, NpadId.No1 });
        Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
#endif

    }

    // Update is called once per frame
    void Update()
    {
        // Makes all buttons interactable x seconds later than scene load. Default suggestion 0.3f for x
        if (timeSceneLoad > 0.3f && !buttonsEnabledAfterSceneLoad)
        {
            MakeAllInteractable();
            buttonsEnabledAfterSceneLoad = true;
        }

#if UNITY_SWITCH
        if (UpdatePadState())
        {
            AnalogStickState lStick = npadState.analogStickL;
            AnalogStickState rStick = npadState.analogStickR;

            // Movements w/ analog stick, (checks both if the input is bigger than 10000 threshold && bigger than other axis)

            // Left Stick

            // Left Stick Go Up
            if (lStick.y > 10000 && Mathf.Abs(lStick.y) > Mathf.Abs(lStick.x))
            {
                if (timeLeftStick > thresholdLeftStick)
                {
                    // Call your functions here

                    timeLeftStick = 0.0f;
                }
            }

            // Left Stick Go Down
            else if (lStick.y < -10000 && Mathf.Abs(lStick.y) > Mathf.Abs(lStick.x))
            {
                if (timeLeftStick > thresholdLeftStick)
                {
                    // Call your functions here

                    timeLeftStick = 0.0f;
                }
            }

            // Left Stick Go Right
            else if (lStick.x > 10000 && Mathf.Abs(lStick.x) > Mathf.Abs(lStick.y))
            {
                if (timeLeftStick > thresholdLeftStick)
                {
                    // Call your functions here

                    timeLeftStick = 0.0f;
                }
            }

            // Left Stick Go Left
            else if (lStick.x < -10000 && Mathf.Abs(lStick.x) > Mathf.Abs(lStick.y))
            {
                if (timeLeftStick > thresholdLeftStick)
                {
                    // Call your functions here

                    timeLeftStick = 0.0f;
                }
            }

            // Right Stick

            // Right Stick Go Up
            if (rStick.y > 10000 && Mathf.Abs(rStick.y) > Mathf.Abs(rStick.x))
            {
                if (timeRightStick > thresholdRightStick)
                {
                    // Call your functions here

                    timeRightStick = 0.0f;
                }
            }

            // Right Stick Go Down
            else if (rStick.y < -10000 && Mathf.Abs(rStick.y) > Mathf.Abs(rStick.x))
            {
                if (timeRightStick > thresholdRightStick)
                {
                    // Call your functions here

                    timeRightStick = 0.0f;
                }
            }

            // Right Stick Go Right
            else if (rStick.x > 10000 && Mathf.Abs(rStick.x) > Mathf.Abs(rStick.y))
            {
                if (timeRightStick > thresholdRightStick)
                {
                    // Call your functions here

                    timeRightStick = 0.0f;
                }
            }

            // Right Stick Go Left
            else if (rStick.x < -10000 && Mathf.Abs(rStick.x) > Mathf.Abs(rStick.y))
            {
                if (timeRightStick > thresholdRightStick)
                {
                    // Call your functions here

                    timeRightStick = 0.0f;
                }
            }

            // Buttons

            // Up Button pressed  
            if (npadState.GetButtonDown(NpadButton.Up))
            {
                if (Up_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.Up))
            {
                Up_interactable = true;
            }

            // Down Button pressed
            if (npadState.GetButtonDown(NpadButton.Down))
            {
                if (Down_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.Down))
            {

                Down_interactable = true;

            }

            // Left Button pressed
            if (npadState.GetButtonDown(NpadButton.Left))
            {
                if (Left_interactable)
                {
                    // Call your functions here
                }

            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.Left))
            {
                Left_interactable = true;
            }

            // Right Button pressed
            if (npadState.GetButtonDown(NpadButton.Right))
            {
                if (Right_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.Right))
            {
                Right_interactable = true;
            }

            // A Button pressed
            if (npadState.GetButtonDown(NpadButton.A))
            {
                if (A_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.A))
            {
                A_interactable = true;
            }

            // B Button pressed
            if (npadState.GetButtonDown(NpadButton.B))
            {
                if (B_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.B))
            {
                B_interactable = true;
            }

            // X Button pressed
            if (npadState.GetButtonDown(NpadButton.X))
            {
                if (X_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.X))
            {
                X_interactable = true;
            }

            // Y Button pressed
            if (npadState.GetButtonDown(NpadButton.Y))
            {
                if (Y_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.Y))
            {
                Y_interactable = true;
            }

            // ZR Button pressed
            if (npadState.GetButtonDown(NpadButton.ZR))
            {
                if (ZR_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.ZR))
            {
                ZR_interactable = true;
            }

            // ZL Button pressed
            if (npadState.GetButtonDown(NpadButton.ZL))
            {
                if (ZL_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.ZL))
            {
                ZL_interactable = true;
            }

            // Plus Button pressed
            if (npadState.GetButtonDown(NpadButton.Plus))
            {
                if (Plus_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.Plus))
            {
                Plus_interactable = true;
            }

            // Minus Button pressed
            if (npadState.GetButtonDown(NpadButton.Minus))
            {
                if (Minus_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.Minus))
            {
                Minus_interactable = true;
            }

            // R Button pressed
            if (npadState.GetButtonDown(NpadButton.R))
            {
                if (R_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.R))
            {
                R_interactable = true;
            }

            // L Button pressed
            if (npadState.GetButtonDown(NpadButton.L))
            {
                if (L_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.L))
            {
                L_interactable = true;
            }

            // Left SL Button pressed
            if (npadState.GetButtonDown(NpadButton.LeftSL))
            {
                if (SL_left_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.LeftSL))
            {
                SL_left_interactable = true;
            }

            // Left SR Button pressed
            if (npadState.GetButtonDown(NpadButton.LeftSR))
            {
                if (SL_left_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.LeftSR))
            {
                SR_left_interactable = true;
            }

            // Right SL Button pressed
            if (npadState.GetButtonDown(NpadButton.RightSL))
            {
                if (SL_right_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.RightSL))
            {
                SL_right_interactable = true;
            }

            // Right SR Button pressed
            if (npadState.GetButtonDown(NpadButton.RightSR))
            {
                if (SR_right_interactable)
                {
                    // Call your functions here
                }
            }

            // Makes the button interactable if it is not pressed
            if (!npadState.GetButtonDown(NpadButton.RightSR))
            {
                SR_right_interactable = true;
            }

        }
#endif

        // increases current time
        timeSceneLoad += Time.deltaTime;

        timeLeftStick += Time.deltaTime;
        timeRightStick += Time.deltaTime;
    }

    // Function makes all the buttons interactable
    void MakeAllInteractable()
    {
        A_interactable = true;
        B_interactable = true;
        Y_interactable = true;
        X_interactable = true;

        L_interactable = true;
        R_interactable = true;
        ZL_interactable = true;
        ZR_interactable = true;

        Plus_interactable = true;
        Minus_interactable = true;

        Up_interactable = true;
        Down_interactable = true;
        Left_interactable = true;
        Right_interactable = true;

        SL_left_interactable = true;
        SL_right_interactable = true;
        SR_left_interactable = true;
        SR_right_interactable = true;
    }

    // Function makes all the buttons NOT interactable
    void MakeAllNotInteractable()
    {
        A_interactable = false;
        B_interactable = false;
        Y_interactable = false;
        X_interactable = false;

        L_interactable = false;
        R_interactable = false;
        ZL_interactable = false;
        ZR_interactable = false;

        Plus_interactable = false;
        Minus_interactable = false;

        Up_interactable = false;
        Down_interactable = false;
        Left_interactable = false;
        Right_interactable = false;

        SL_left_interactable = false;
        SL_right_interactable = false;
        SR_left_interactable = false;
        SR_right_interactable = false;
    }

#if UNITY_SWITCH
    private bool UpdatePadState()
    {
        NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
        NpadState handheldState = npadState;
        if (handheldStyle != NpadStyle.None)
        {
            Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
            if (handheldState.buttons != NpadButton.None)
            {
                npadId = NpadId.Handheld;
                npadStyle = handheldStyle;
                npadState = handheldState;
                return true;
            }
        }

        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = npadState;
        if (no1Style != NpadStyle.None)
        {
            Npad.GetState(ref no1State, NpadId.No1, no1Style);
            if (no1State.buttons != NpadButton.None)
            {
                npadId = NpadId.No1;
                npadStyle = no1Style;
                npadState = no1State;
                return true;
            }
        }

        if ((npadId == NpadId.Handheld) && (handheldStyle != NpadStyle.None))
        {
            npadId = NpadId.Handheld;
            npadStyle = handheldStyle;
            npadState = handheldState;
        }
        else if ((npadId == NpadId.No1) && (no1Style != NpadStyle.None))
        {
            npadId = NpadId.No1;
            npadStyle = no1Style;
            npadState = no1State;
        }
        else
        {
            npadId = NpadId.Invalid;
            npadStyle = NpadStyle.Invalid;
            npadState.Clear();
            return false;
        }
        return true;
    }
#endif
}
