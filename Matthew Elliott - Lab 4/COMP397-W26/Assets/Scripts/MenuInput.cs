using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour
{
    private InputAction openMenu;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private bool isMenuOpen = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openMenu = InputSystem.actions.FindAction("UI/Menu");
        openMenu.started += ToggleMenu;
        menuPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        openMenu.started -= ToggleMenu;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Open Menu Called Pressing P");
        isMenuOpen = !isMenuOpen;
        if (isMenuOpen)
        {
            menuPanel.SetActive(true);
            GetComponent<PlayerInput>().enabled = false; // Disable the Component
            InputSystem.actions.FindActionMap("Player").Disable(); // Disable the Action Map for Player
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            menuPanel.SetActive(false);
            GetComponent<PlayerInput>().enabled = true; // Enable the Component
            InputSystem.actions.FindActionMap("Player").Enable(); // Enables the Action Map for Player
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }
}
