using Mirror;
using System;
using TMPro;
using UnityEngine;

public class SceneScript : NetworkBehaviour
{
    public TextMeshProUGUI canvasStatusText;
    public PlayerController playerController;

    [SyncVar(hook = nameof(OnStatusTextChanged))]
    public string statusText;

    private void OnStatusTextChanged(string _Old, string _New)
    {
        canvasStatusText.text = statusText;
    }

    public void ButtonSendMessage()
    {
        if (playerController != null)
            playerController.CmdSendPlayerMessage();
    }
}
