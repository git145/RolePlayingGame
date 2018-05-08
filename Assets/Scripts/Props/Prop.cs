// File: Prop.cs
// Author: Richard Kneale
// Date created: 30th October 2017
// Description: The superclass of Prop scripts

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RolePlayingGame
{
    public class Prop : MonoBehaviour
    {
        // The object that will allow the player to return to the room containing this Prop
        [SerializeField]
        private string returnObject;

        // The location where a player will be warped to
        [SerializeField]
        private Object warpLocation;

        // The room that leads to the room containing this Prop
        private GameObject parentRoom;

        // Remembers whether the action button has been pressed
        [HideInInspector]
        public bool isActionPressed = false;

        // Represents whether the action button has been pressed
        [HideInInspector]
        public float action;

        // Describes the state of the prop
        [HideInInspector]
        public int propState = -1;

        // Describes the DialogCanvas in the scene
        [HideInInspector]
        public GameObject dialogCanvas;

        // Describes the script on the DialogCanvas in the scene
        [HideInInspector]
        public DialogCanvas dialogCanvasScript;

        // The room in which the prop is located
        [SerializeField]
        private GameObject room;

        // The Text script on the TextBox
        [HideInInspector]
        public Text textScript;

        // A reference to the inventory canvas
        protected GameObject inventoryCanvas;

        // Triggers the sequence of events relating to a prop
        // Can be overridden
        public virtual void StartProp()
        {
        }

        // The standard error output for Props
        public void ActionError()
        {
            Debug.Log("Error!");
        }

        // Changes the state by a given amount
        public void ChangePropState(int _change)
        {
            propState += _change;
        }

        // Return the prop to the state before the prop is activated
        public void ResetPropState()
        {
            propState = -1;
        }

        // Warps the player to a new location
        public void Warp()
        {
            // If the new location does not currently exist in the scene
            if(parentRoom == null)
            {
                // Instantiate and get a reference to the new room
                GameObject newRoom = Instantiate(warpLocation) as GameObject;

                // If the player wants to return to the room with this prop
                if((returnObject != null) && (returnObject != ""))
                {
                    // Remember the new room
                    SetParentRoom(newRoom);

                    // Provide an object in the new room with a link to this room
                    newRoom.transform.Find(returnObject).GetComponent<Prop>().SetParentRoom(room);
                }
            }

            // If the new location already exists in the scene
            else
            {
                // Show the new location
                parentRoom.SetActive(true);
            }

            // Reset the dialog canvas
            dialogCanvasScript.ResetDialogCanvas();

            // Hide the old room
            room.SetActive(false);
        }

        // Remember the room before the room containing this object
        public void SetParentRoom(GameObject _parentRoom)
        {
            parentRoom = _parentRoom;
        }

        // Adds a question to the DialogCanvas
        public void AddQuestion(string _text, UnityAction _yesAction, UnityAction _noAction)
        {
            // Add the question to the DialogCanvas
            textScript.text = _text;
            dialogCanvasScript.answerPanel.SetActive(true);

            // Show the yes button and add functionality
            dialogCanvasScript.yesButton.SetActive(true);
            dialogCanvasScript.yesButton.GetComponent<Button>().onClick.AddListener(_yesAction);

            // Show the no button and add functionality
            dialogCanvasScript.noButton.SetActive(true);
            dialogCanvasScript.noButton.GetComponent<Button>().onClick.AddListener(_noAction);

            // Show the DialogCanvas if it is hidden
            if (dialogCanvas.activeInHierarchy == false)
            {
                dialogCanvas.SetActive(true);
            }
        }

        // References a given inventory canvas
        public void SetInventoryCanvas(GameObject _inventoryCanvas)
        {
            inventoryCanvas = _inventoryCanvas;
        }
    }
}
