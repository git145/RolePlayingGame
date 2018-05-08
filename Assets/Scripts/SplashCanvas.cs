// File: SplashCanvas.cs
// Author: Richard Kneale
// Date created: 15th October 2017
// Description: Contains functions used by the SplashCanvas

using UnityEngine;

namespace RolePlayingGame
{
    public class SplashCanvas : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        // Opens the MenuCanvas and destroys the canvas that this script is on
        public void ShowMenu()
        {
            Instantiate(Resources.Load("Canvases/MenuCanvas"));
            Destroy(gameObject);
        }
    }
}
