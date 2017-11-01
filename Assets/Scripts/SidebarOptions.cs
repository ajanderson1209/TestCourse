using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidebarOptions : MonoBehaviour {

    // Canvas with attached pages
    public GameObject canvas;

    // Container of pages to load
    private Component[] pages;

    
    // Find page objects
    void Start() {
        pages = canvas.GetComponentsInChildren(typeof(CanvasGroup), true);
        
        // activate corresponding page button
        for (int i = 0; i < pages.Length; i++) {
            if (pages[i].gameObject.active) {
                GetComponentInChildren<CanvasGroup>().GetComponentsInChildren<Button>()[i].Select();
                break;
            }
        }
    }

	// Load page based on selection
    public void LoadPage(int _index) {
        // same page check
        if (pages[_index].gameObject.active) {
            return;
        }

        // disable all pages
        foreach (Component page in pages) {
            page.gameObject.SetActive(false);
        }

        // enable correct page
        pages[_index].gameObject.SetActive(true);
    }

    // Move menu button when toggling sidebar
    public void ToggleMenu() {
        Component comp = GetComponentInChildren<CanvasGroup>(true);
        comp.gameObject.SetActive(!comp.gameObject.active);
    }

}
