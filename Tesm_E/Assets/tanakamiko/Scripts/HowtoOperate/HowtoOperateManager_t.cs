using UnityEngine;
using UnityEngine.SceneManagement;

public class HowtoOperateManager_t : MonoBehaviour
{
    public GameObject operationPanel;
    public GameObject firePanel;
    public GameObject treePanel;
    public GameObject earthPanel;
    public GameObject waterPanel;

    void Start()
    {
        ShowOperation();
    }

    void HideAll()
    {
        operationPanel.SetActive(false);
        firePanel.SetActive(false);
        treePanel.SetActive(false);
        earthPanel.SetActive(false);
        waterPanel.SetActive(false);
    }

    public void ShowOperation()
    {
        HideAll();
        operationPanel.SetActive(true);
    }

    public void ShowFire()
    {
        HideAll();
        firePanel.SetActive(true);
    }

    public void ShowTree()
    {
        HideAll();
        treePanel.SetActive(true);
    }

    public void ShowEarth()
    {
        HideAll();
        earthPanel.SetActive(true);
    }

    public void ShowWater()
    {
        HideAll();
        waterPanel.SetActive(true);
    }

    // ç≈å„ÇæÇØégÇ§
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}

