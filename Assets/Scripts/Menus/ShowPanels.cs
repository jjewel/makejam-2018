using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel, creditsPanel;							 
							 
	public GameObject startPanel;							 
	public GameObject pausePanel;                            

    private GameObject activePanel;                         
    private MenuObject activePanelMenuObject;
    private EventSystem eventSystem;

    private void SetSelection(GameObject panelToSetSelected)
    {

        activePanel = panelToSetSelected;
        activePanelMenuObject = activePanel.GetComponent<MenuObject>();
        if (activePanelMenuObject != null)
        {
            activePanelMenuObject.SetFirstSelected();
        }
    }

    public void Start()
    {
        SetSelection(startPanel);
    }

    //Call this function to activate and display the Options panel during the main menu
    public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
        startPanel.SetActive(false);
        SetSelection(optionsPanel);

    }

    //Call this function to activate and display the Credits panel during the main menu
    public void ShowCreditsPanel()
    {
        creditsPanel.SetActive(true);
        startPanel.SetActive(false);
        SetSelection(creditsPanel);

    }

    //Call this function to deactivate and hide the Options panel during the main menu
    public void HideOptionsPanel()
	{
        startPanel.SetActive(true);
        optionsPanel.SetActive(false);
	}

	//Call this function to deactivate and hide the Credits panel during the main menu
	public void HideCreditsPanel()
	{
		startPanel.SetActive(true);
		creditsPanel.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		startPanel.SetActive (true);
        SetSelection(startPanel);
    }

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		startPanel.SetActive (false);

	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
        SetSelection(pausePanel);
    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
	}
}
