using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZXing;


public class MobileCameraManager : MonoBehaviour
{
    // Varibales for camera visablity
    private WebCamTexture backCam;
    private Texture defaultBackground;
    


    public RawImage background;

    public GameObject camera;


    // Varibles for qr-code scanner
    private BarcodeReader reader = new BarcodeReader();   
    private bool questFound = false;


    void Update()
    {
        // Only run when the camera is on and delivers a real image
        if (backCam == null || !backCam.isPlaying || backCam.width < 100) return;

        RotateImage();
        FlipImage();
        ResizeImage();
        ScanQRCode();
    }



    // Turnning the image upright 
    void RotateImage()
    {
        float rotation = backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, -rotation);
    }

    // Flipping the image if the camera delivers it upside down
    void FlipImage()
    {
        if (backCam.videoVerticallyMirrored)
            background.rectTransform.localScale = new Vector3(1, -1, 1);
        else
            background.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    // Making the image fill the screen without stretching it
    void ResizeImage()
    {
        RectTransform parent = (RectTransform)background.rectTransform.parent;
        float screenWidth = parent.rect.width;
        float screenHeight = parent.rect.height;

        // When the image is turned by 90°, width and height swap roles
        bool isSideways = backCam.videoRotationAngle == 90 || backCam.videoRotationAngle == 270;
        if (isSideways)
        {
            float temp = screenWidth;
            screenWidth = screenHeight;
            screenHeight = temp;
        }

        float cameraRatio = (float)backCam.width / backCam.height;   

        // Take the bigger size so no empty space is left on the screen
        float imageWidth = Mathf.Max(screenWidth, screenHeight * cameraRatio);
        float imageHeight = imageWidth / cameraRatio;

        background.rectTransform.sizeDelta = new Vector2(imageWidth, imageHeight);
    }

    private void ScanQRCode()
    {
        // fallback if camera not found
        if (questFound) return;
        // Uses QRCode scan every 15 Frames to save energie
        if (Time.frameCount % 15 != 0) return;   
        Result result = reader.Decode(backCam.GetPixels32(), backCam.width, backCam.height);
        // fallback if no QR Code in the iamge
        if (result == null) return;             

        string text = result.Text.Trim();

        // Strings for what the scanner is looking for
        switch (text)
        {
            case "Euphemia":
                StartQuest("EuphemiaQuest");
                break;

            case "Architekt":
                StartQuest("ArchitektQuest");
                break;

            case "Quest3":
                StartQuest("Quest3");
                break;

            case "Quest4":
                StartQuest("Quest4");
                break;

            default:
                // Fallback if text is not in the list -> ignore it and keep scanning
                return;
        }

    }

    // remembers the quest and goes back to the MainScene
    void StartQuest(string questName)
    {
        questFound = true;
        GameState.activeQuest = questName;
        Debug.Log("Quest started: " + questName);
        OnBackClick();   
    }




    public void OpenCamera()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;
        // camAvailable = false;
        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");

            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {

                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back Camera");
            return;
        }

        
        // Asking for Camera permission
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("camera permission granted");
            backCam.Play();
            background.texture = backCam;
        }
        else
        {
            Debug.Log("permission not granted");
            return;
        }
        
        
    }


    public void OnBackClick()
    {
        // Close Camera if still aktiv
        if (backCam != null)
        {
            backCam.Stop();
        }
        SceneManager.LoadScene("MainScene");

    }
}
