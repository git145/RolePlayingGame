// File: DescriptionCanvas.cs
// Author: Richard Kneale
// Date created: 1st November 2017
// Description: Manages a DescriptionCanvas

using UnityEngine;
using UnityEngine.UI;

public class DescriptionCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject proceedButton;

    // Use this for initialization
    private void Awake()
    {
        Button button = proceedButton.GetComponent<Button>();
        button.onClick.AddListener(HideDescription);
    }

    // Use this for initialization
    private void Start ()
    {
	}

    // Update is called once per frame
    private void Update ()
    {
	}

    // Hides the description canvas
    private void HideDescription()
    {
        gameObject.SetActive(false);
    }
}
