// File: SelectCanvas.cs
// Author: Richard Kneale
// Date created: 31st October 2017
// Description: Allows the player to select a character

using UnityEngine;
using UnityEngine.UI;

namespace RolePlayingGame
{
    public class SelectCanvas : MonoBehaviour
    {
        // A reference to the GameController script in the scene
        private GameController gameController;

        // The characters that the player can select from
        [SerializeField]
        private Object[] characters;

        // The number of characters that the player can select from
        private int charactersCount;

        // The character that the player has currently chosen
        private int character = 0;

        // The button to select the character and start the game
        [SerializeField]
        private GameObject characterButton;

        // The left arrow button
        [SerializeField]
        private GameObject leftButton;

        // The right arrow button
        [SerializeField]
        private GameObject rightButton;

        // The name of a character
        [SerializeField]
        private GameObject nameText;

        // The skill of a character
        [SerializeField]
        private GameObject skillText;

        // The stamina of a character
        [SerializeField]
        private GameObject staminaText;

        // The luck of a character
        [SerializeField]
        private GameObject luckText;

        // Strings representing the names of attributes
        private string skillString;
        private string staminaString;
        private string luckString;

        // Arrays representing attributes of the characters
        private Sprite[] characterImage;
        private string[] characterName;
        private string[] characterSkill;
        private string[] characterStamina;
        private string[] characterLuck;


        // Use this to set up references to other scripts
        private void Awake()
        {
            // Get the GameController script in the scene
            gameController = GameObject.Find("GameController").GetComponent<GameController>();

            // Add functionality to the buttons
            characterButton.GetComponent<Button>().onClick.AddListener(StartGame);
            leftButton.GetComponent<Button>().onClick.AddListener(DecreaseCharacter);
            rightButton.GetComponent<Button>().onClick.AddListener(IncreaseCharacter);

            // Get the number of characters that the player can select from
            charactersCount = characters.Length;

            // Get the strings representing attributes
            skillString = gameController.Capitalize(gameController.GetSkillString());
            staminaString = gameController.Capitalize(gameController.GetStaminaString());
            luckString = gameController.Capitalize(gameController.GetLuckString());

            // Initialize the arrays representing attributes of the characters
            characterImage = new Sprite[charactersCount];
            characterName = new string[charactersCount];
            characterSkill = new string[charactersCount];
            characterStamina = new string[charactersCount];
            characterLuck = new string[charactersCount];

            // For each character that the player can select
            for(int characterIndex = 0; characterIndex < charactersCount; characterIndex++)
            {
                // Get the PlayerController script of the character
                GameObject characterCurrent = characters[characterIndex] as GameObject;
                PlayerController characterCurrentScript = characterCurrent.GetComponent<PlayerController>();

                // Get the attributes of the character
                characterImage[characterIndex] = characterCurrentScript.fullImage;
                characterName[characterIndex] = characterCurrent.name;
                characterSkill[characterIndex] = skillString + ": " + characterCurrentScript.GetSkill();
                characterStamina[characterIndex] = staminaString + ": " + characterCurrentScript.GetStamina();
                characterLuck[characterIndex] = luckString + ": " + characterCurrentScript.GetLuck();
            }
        }

        // Use this for initialization
        private void Start()
        {
            // Display the attributes of the first character
            SetAttributes();
        }

        // Update is called once per frame
        private void Update()
        { 
        }

        // Displays the attributes of the next character
        private void DecreaseCharacter()
        {
            character = (character + (charactersCount - 1)) % charactersCount;
            SetAttributes();
        }

        // Displays the attributes of the previous character
        private void IncreaseCharacter()
        {
            character = (++character) % charactersCount;
            SetAttributes();
        }

        // Displays the attributes of the currently selected character
        private void SetAttributes()
        {
            characterButton.GetComponent<Image>().sprite = characterImage[character];
            nameText.GetComponent<Text>().text = characterName[character];
            skillText.GetComponent<Text>().text = characterSkill[character];
            staminaText.GetComponent<Text>().text = characterStamina[character];
            luckText.GetComponent<Text>().text = characterLuck[character];
        }

        // Starts the game
        private void StartGame()
        {
            gameController.StartGame(characters[character], gameObject);
        }
    }
}
