// File: MenuCanvas.cs
// Author: Richard Kneale
// Date created: 15th October 2017
// Description: Contains functions used by the MenuCanvas

using UnityEngine;
using UnityEngine.UI;

namespace RolePlayingGame
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject startButton;

        // Use this for initialization
        void Start()
        {
            // Add functionality to the start button
            Button button = startButton.GetComponent<Button>();
            button.onClick.AddListener(StartSelect);
        }

        // Update is called once per frame
        void Update()
        {
        }

        // Opens the SelectCanvas and destroys the canvas that this script is on
        void StartSelect()
        {
            Instantiate(Resources.Load("Canvases/SelectCanvas"));
            Destroy(gameObject);
        }
    }
}
