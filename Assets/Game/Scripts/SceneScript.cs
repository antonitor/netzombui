using Mirror;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : NetworkBehaviour
{
    public SceneReference sceneReference;
    public TextMeshProUGUI canvasStatusText;
    public PlayerController playerController;
    public TextMeshProUGUI canvasAmmoText;

    [SyncVar(hook = nameof(OnStatusTextChanged))]
    public string statusText;

    private void OnStatusTextChanged(string _Old, string _New)
    {
        canvasStatusText.text = statusText;
    }

    public void UIAmmo(int _value)
    {
        canvasAmmoText.text = "Ammo:" + _value;
    }

    public void ButtonSendMessage()
    {
        if (playerController != null)
            playerController.CmdSendPlayerMessage();
    }

    public void ButtonChangeScene()
    {
        if (isServer)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "MyScene")
            {
                NetworkManager.singleton.ServerChangeScene("MyOtherScene");
            }
            else
            {
                NetworkManager.singleton.ServerChangeScene("MyScene");
            }
        }
        else
        {
            Debug.Log("You are not Server");
        }
    }
}
