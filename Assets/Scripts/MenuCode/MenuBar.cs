using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{

    public Button[] menuBtns;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void Setting()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackMenuBtn()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void OnHoverEnter(int mapIndex)
    {       
            menuBtns[mapIndex].transform.localScale = Vector3.one * 1.1f; // Phóng to nhẹ     
    }

    public void OnHoverExit(int mapIndex)
    {
            menuBtns[mapIndex].transform.localScale = Vector3.one; // Trở lại bình thường
    }


}
