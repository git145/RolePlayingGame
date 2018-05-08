// File: GameController.cs
// Author: Richard Kneale
// Date created: 15th October 2017
// Description: Manages the game

using UnityEngine;
using UnityEngine.UI;

namespace RolePlayingGame
{
    public class GameController : MonoBehaviour
    {
        // The room in which the player will start
        [SerializeField]
        private Object firstRoom;

        // Values representing the offset of the camera from its target GameObject
        [SerializeField]
        private float cameraOffsetX = 0f;

        [SerializeField]
        private float cameraOffsetY = 0f;

        [SerializeField]
        private float cameraOffsetZ = -1f;

        // Strings representing the names of attributes
        [SerializeField]
        private string skillString;

        [SerializeField]
        private string staminaString;

        [SerializeField]
        private string luckString;

        //[SerializeField]
        //private string staminaPotionString = "health";

        //[SerializeField]
        //private string luckPotionString = "chance";

        [SerializeField]
        [TextArea(15, 20)]
        private string introduction = "The adventure is set at the bottom of an ancient well where nobles and princes of long ago used to " +
                                      "come, cast in their gold coins and make their wishes. All of this gold collected in the bottom of the well. " +
                                      "When the well dried up, treasure hunters from far and wide set off to find it, hoping for riches. But when " +
                                      "they reached the well, they found that the quests ahead of them were far more dangerous than they had thought ...";

        private GameObject dialogCanvas;

        // Use this for initialization
        private void Start()
        {
            // Add the MainCamera and SplashCanvas to the scene
            Instantiate(Resources.Load("MainCamera"));
            Instantiate(Resources.Load("Canvases/SplashCanvas"));
        }

        // Update is called once per frame
        private void Update()
        {
        }

        // Starts the game
        public void StartGame(Object _character, GameObject _destroyMe)
        {
            // Add the first room to the scene
            Instantiate(firstRoom);

            // Add the given character to the scene
            GameObject _newCharacter = Instantiate(_character) as GameObject;

            // Reference the main camera in the scene
            GameObject _camera = GameObject.FindGameObjectWithTag("MainCamera");

            // Reference the CameraFollow script on the main camera
            CameraFollow _cameraFollow = _camera.GetComponent<CameraFollow>();

            // Create the offset vector for the camera
            Vector3 _cameraOffset = new Vector3(cameraOffsetX, cameraOffsetY, cameraOffsetZ);

            // Set the main camera to follow the character
            _cameraFollow.SetTarget(_newCharacter, _cameraOffset);

            // Destroy the given canvas
            Destroy(_destroyMe);

            // Add the description canvas to the scene
            GameObject _descriptionCanvas = Instantiate(Resources.Load("Canvases/DescriptionCanvas")) as GameObject;
            GameObject descriptionText = _descriptionCanvas.transform.Find("DescriptionText").gameObject;
            Text descriptionTextComponent = descriptionText.GetComponent<Text>();
            descriptionTextComponent.text = introduction;

            // Access the PlayerController script on the character
            PlayerController _playerController = _newCharacter.GetComponent<PlayerController>();

            // Add the inventory canvas to the scene
            GameObject _inventoryCanvas = Instantiate(Resources.Load("Canvases/InventoryCanvas")) as GameObject;

            // Provide the character with a reference to the inventory canvas
            _playerController.SetInventoryCanvas(_inventoryCanvas);

            // Set the name of the character on the Inventory Canvas
            GameObject characterText = _inventoryCanvas.transform.Find("CharacterText").gameObject;
            Text characterTextComponent = characterText.GetComponent<Text>();
            characterTextComponent.text = _character.name;

            // Set the skill of the character on the Inventory Canvas
            GameObject skillText = characterText.transform.Find("SkillText").gameObject;
            Text skillTextComponent = skillText.GetComponent<Text>();
            skillTextComponent.text = Capitalize(skillString) + ": " + _playerController.GetSkill();

            // Set the stamina of the character on the Inventory Canvas
            GameObject staminaText = characterText.transform.Find("StaminaText").gameObject;
            Text staminaTextComponent = staminaText.GetComponent<Text>();
            staminaTextComponent.text = Capitalize(staminaString) + ": " + _playerController.GetStamina();

            // Set the luck of the character on the Inventory Canvas
            GameObject luckText = characterText.transform.Find("LuckText").gameObject;
            Text luckTextComponent = luckText.GetComponent<Text>();
            luckTextComponent.text = Capitalize(luckString) + ": " + _playerController.GetLuck();

            // Provide the character with a reference to the description canvas
            _playerController.SetDescriptionCanvas(_descriptionCanvas);

            // Instantiate a dialog canvas
            dialogCanvas = Instantiate(Resources.Load("Canvases/DialogCanvas")) as GameObject;

            // Provide the character with a reference to the dialog canvas
            _playerController.SetDialogCanvas(dialogCanvas);
        }

        // Convert a string to a string that starts with an upper case letter that is followed with lower case letters
        public string Capitalize(string _string)
        {
            return _string.Substring(0, 1).ToString().ToUpper() + _string.Substring(1).ToLower();
        }

        // Returns the DialogCanvas
        public GameObject GetDialogCanvas()
        {
            return dialogCanvas;
        }

        public string GetSkillString()
        {
            return skillString;
        }

        public string GetStaminaString()
        {
            return staminaString;
        }

        public string GetLuckString()
        {
            return luckString;
        }
    }
}
