using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTheaterClick()
    {
        print("You entered the theater");
        return;
    }

    // Update is called once per frame
    public void OnCameraClick()
    {
        SceneManager.LoadScene("CameraScene");
    }

    public void OnOptionClick()
    {
        print("Now you can change options");
        return;
    }
}
