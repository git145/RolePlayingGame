// File: Well.cs
// Author: Richard Kneale
// Date created: 2nd November 2017
// Description: Manages a Well

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RolePlayingGame
{
    public class Well : Prop
    {
        // A string describing the location of the bucket in the well
        [SerializeField]
        private string bucketLocation;

        // Strings to be added to the DialogCanvas
        private string text0;
        private string text1 = "Will you turn the handle?";

        // A reference to the GameController script in the scene
        private GameController gameController;

        // Update is called once per frame
        public void Start()
        {
            text0 = "At the " + bucketLocation + " of the well is a bucket that can be wound up and down the well with a handle.";
        }

        // Update is called once per frame
        public void Update()
        {
            // If the well events have been started
            if((propState >= 0) && (!inventoryCanvas.activeInHierarchy))
            {
                // Perform the action appropriate for the state
                switch(propState)
                {
                    case 0:
                        ActionZero();
                        break;

                    default:
                        break;
                }
            }
        }

        // Triggers the sequence of events relating to a Well
        public override void StartProp()
        {
            // Recognise that the action buton was pressed
            isActionPressed = true;

            // Get the GameController script in the scene
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

            // Get a reference to the DialogCanvas in the scene
            dialogCanvas = gameController.GetDialogCanvas();

            // Get a reference to the DialogCanvas script in the scene
            dialogCanvasScript = dialogCanvas.GetComponent<DialogCanvas>();

            // Get a reference to the Text script in the DialogCanvas
            textScript = dialogCanvasScript.textBox.GetComponent<Text>();

            // Change the text
            textScript.text = text0;

            // Display the DialogCanvas
            dialogCanvas.SetActive(true);

            // Change the state
            ChangePropState(1);
        }

        // The action taken when the Prop is in the first state
        private void ActionZero()
        {
            // Determine whether the user has pressed the action button
            float action = Input.GetAxisRaw("Action");

            // If the user presses the action button
            if((action != 0) && (!isActionPressed))
            {
                // Change the state to a null state
                ChangePropState(1);

                // Prepare the question
                UnityAction yesAction = new UnityAction(YesResult);
                UnityAction noAction = new UnityAction(NoResult);
                AddQuestion(text1, yesAction, noAction);

                // Recognise that the action button has been pressed
                isActionPressed = true;
            }

            // If the action button was released
            if((action == 0) && (isActionPressed))
            {
                // Recognise that the action button was released
                isActionPressed = false;
            }
        }

        // The result when the yes button is pressed
        private void YesResult()
        {
            // Return the prop to the state before the prop is activated
            ResetPropState();

            // Warps the player to a new location
            Warp();
        }

        // The result when the no button is pressed
        private void NoResult()
        {
            // Return the prop to the state before the prop is activated
            ResetPropState();

            // Reset the DialogCanvas
            dialogCanvasScript.ResetDialogCanvas();
        }
    }
}
