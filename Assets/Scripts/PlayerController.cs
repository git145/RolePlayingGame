// File: PlayerController.cs
// Author: Richard Kneale
// Date created: 15th October 2017
// Description: Manages the player

using UnityEngine;

namespace RolePlayingGame
{
    public class PlayerController : MonoBehaviour
    {
        // The attributes of the player
        [SerializeField]
        [Range(7, 12)]
        private int skill;

        [SerializeField]
        [Range(14, 24)]
        private int stamina;

        [SerializeField]
        [Range(7, 12)]
        private int luck;

        // The speed that the player will move at
        [SerializeField]
        private float speed = 2f;

        // The vector to store the direction of the movement of the player
        private Vector3 movement;

        // Represents whether the action button has been pressed
        private bool isActionPressed = false;

        // Represents whether the inventory button has been pressed
        private bool isInventoryPressed = false;

        // Represents a prop that the player is touching
        private GameObject collidingProp;

        // A reference to the inventory canvas
        private GameObject inventoryCanvas;

        // A reference to the dialog canvas
        private GameObject dialogCanvas;

        // A reference to the description canvas
        private GameObject descriptionCanvas;

        // The Rigidbody component on the player
        Rigidbody rBody;

        // A larger image representing the character
        public Sprite fullImage;

        /*
        // Lists of weapons and provisions
        private LinkedList<MonoBehaviour> weapons;
        private LinkedList<MonoBehaviour> provisions;
        */

        /*
        // A reference to the animator component
        private Animator anim;
        */

        // Use this to set up references to other scripts
        private void Awake()
        {
            // Reference the Rigidbody component on the player
            rBody = GetComponent<Rigidbody>();

            /*
            // Reference the animator component
            anim = GetComponentInChildren<Animator>();
            */
        }

        // Update is called once per frame
        private void Update()
        {
            // If there is not a description box on the screen
            if (!(descriptionCanvas.activeInHierarchy))
            {
                // Execute the Inventory() method if there is a reference to an inventory canvas
                if (inventoryCanvas != null)
                {
                    Inventory();
                }

                // If the inventory canvas is not displayed and a dialog box is not displayed
                if (!(inventoryCanvas.activeInHierarchy) && !(dialogCanvas.activeInHierarchy))
                {
                    // Execute Move()
                    Move();

                    // Execute Action()
                    Action();
                }
            }

            /*
            // Animate the player.
            Animating(h, v);
            */
        }

        // Displays the inventory
        private void Inventory()
        {
            // Determine whether the user has pressed the inventory button
            float _inventory = Input.GetAxisRaw("Inventory");

            // If the user presses the inventory button
            if ((_inventory != 0) && (!isInventoryPressed))
            {
                // Recognise that the user pressed the inventory button
                isInventoryPressed = true;

                // If the inventory canvas is displayed
                if (inventoryCanvas.activeInHierarchy)
                {
                    // Hide the inventory canvas
                    inventoryCanvas.SetActive(false);
                }

                // If the inventory canvas is not displayed
                else 
                {
                    // Show the inventory canvas
                    inventoryCanvas.SetActive(true);
                }
            }
            // If the user releases the inventory button
            else if ((_inventory == 0) && (isInventoryPressed))
            {
                // Recognise that the user has released the inventory button
                isInventoryPressed = false;
            }
        }

        // Moves the player
        private void Move()
        {
            // Determine whether the user has pressed the horizontal or vertical movement buttons
            float _movementX = Input.GetAxisRaw("Horizontal");
            float _movementY = Input.GetAxisRaw("Vertical");

            // Set the movement vector based on the axis input
            if ((_movementX != 0) && (_movementY == 0)) // Horizontal movement
            {
                movement.Set(_movementX, 0f, 0f);
            }
            else if ((_movementX == 0) && (_movementY != 0)) // Vertical movement
            {
                movement.Set(0f, _movementY, 0f);
            }
            else // No movement
            {
                movement.Set(0f, 0f, 0f);
            }

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player
            rBody.MovePosition(rBody.position + movement);
        }

        /*
        // Animates the player
        private void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking
            anim.SetBool("IsWalking", walking);
        }
        */

        // Performs an action
        private void Action()
        {
            // Determine whether the user has pressed the action button
            float _action = Input.GetAxisRaw("Action");

            // If the user presses the action button
            if ((_action != 0) && (!isActionPressed))
            {
                // Recognise that the action button has been pressed
                isActionPressed = true;

                // If the player is touching a prop
                if (collidingProp != null)
                {
                    // Attempt to access the script on the Prop
                    Prop collidingPropScript = collidingProp.GetComponent<Prop>();

                    // If the Prop has a script
                    if(collidingPropScript != null)
                    {
                        collidingPropScript.SetInventoryCanvas(inventoryCanvas);

                        // Start the actions on the rop
                        collidingPropScript.StartProp();

                        // Forget the collision with the Prop
                        collidingProp = null;
                    }
                }
            }

            // If the action button was released
            if((_action == 0) && (isActionPressed))
            {
                // Recognise that the action button was released
                isActionPressed = false;
            }
        }

        // On a collision
        private void OnCollisionEnter(Collision other)
        {
            // Recognise if the player is touching a Prop
            if (other.gameObject.tag == "Prop")
            {
                collidingProp = other.gameObject;
            }
        }

        // On leaving a collision
        private void OnCollisionExit(Collision other)
        {
            // Recognise if the player is no longer touching a Prop
            if (other.gameObject == collidingProp)
            {
                collidingProp = null;
            }
        }

        // Returns the skill of the player
        public int GetSkill()
        {
            return skill;
        }

        // Returns the stamina of the player
        public int GetStamina()
        {
            return stamina;
        }

        // Returns the luck of the player
        public int GetLuck()
        {
            return luck;
        }

        // References a given inventory canvas
        public void SetInventoryCanvas(GameObject _inventoryCanvas)
        {
            inventoryCanvas = _inventoryCanvas;
        }

        // References a given dialog canvas
        public void SetDialogCanvas(GameObject _dialogCanvas)
        {
            dialogCanvas = _dialogCanvas;
        }

        // References a given description canvas
        public void SetDescriptionCanvas(GameObject _descriptionCanvas)
        {
            descriptionCanvas = _descriptionCanvas;
        }
    }
}
