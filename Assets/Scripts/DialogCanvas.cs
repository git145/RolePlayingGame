// File: DialogCanvas.cs
// Author: Richard Kneale
// Date created: 2nd November 2017
// Description: Manages a DialogCanvas

using UnityEngine;
using UnityEngine.UI;

namespace RolePlayingGame
{
    public class DialogCanvas : MonoBehaviour
    {
        // The TextBox of the DialogCanvas
        public GameObject textBox;
        public Text textBoxScript;

        // The AnswerPanel of the DialogCanvas
        public GameObject answerPanel;

        // The yeas and no buttons of the AnswerPanel
        public GameObject yesButton;
        public GameObject noButton;

        // The default text in the TextBox
        private string defaultText = "Text";

        // Use this to set up references to other scripts
        public void Awake()
        {
            // Get the script on the TextBox
            textBoxScript = textBox.GetComponent<Text>();
        }

        // Resets the DialogCanvas
        public void ResetDialogCanvas()
        {
            // Remove functionality from the yes button and hide it
            if(yesButton.activeInHierarchy)
            {
                yesButton.GetComponent<Button>().onClick.RemoveAllListeners();
                yesButton.SetActive(false);
            }

            if(noButton.activeInHierarchy)
            {
                // Remove functionality from the no button and hide it
                noButton.GetComponent<Button>().onClick.RemoveAllListeners();
                noButton.SetActive(false);
            }

            if(answerPanel.activeInHierarchy)
            {
                // Hide the AnswerPanel
                answerPanel.SetActive(false);
            }

            // Reset the text in the textBox
            textBoxScript.text = defaultText;

            // Hide the DialogCanvas
            gameObject.SetActive(false);
        }
    }
}
