using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject accessCodePanel;
    public GameObject chatRoomPanel;

    [Header("Access Code UI")]
    public TMP_InputField codeInputField;
    public string correctCode = "1234"; // Set your desired code here
    public TextMeshProUGUI errorText;

    [Header("Chat Room UI")]
    public TMP_InputField messageInputField;
    public Transform chatContentParent;
    public ScrollRect scrollRect;
    public TextMeshProUGUI statusText;

    [Header("Prefabs")]
    public GameObject messagePrefab; // Create a simple TMP Text or Image bubble prefab

    void Start()
    {
        // Initial State
        accessCodePanel.SetActive(true);
        chatRoomPanel.SetActive(false);
        if (errorText != null) errorText.text = "";
    }

    // Called by the Submit Button on the AccessCodePanel
    public void SubmitAccessCode()
    {
        if (codeInputField.text == correctCode)
        {
            accessCodePanel.SetActive(false);
            chatRoomPanel.SetActive(true);
            statusText.text = "Logged in as: User";
        }
        else
        {
            if (errorText != null) errorText.text = "Invalid Code!";
            codeInputField.text = "";
        }
    }

    // Called by the Send Button on the ChatRoomPanel
    public void SendChatMessage()
    {
        string msg = messageInputField.text;
        if (string.IsNullOrEmpty(msg)) return;

        // Instantiate message in the list
        GameObject newBubble = Instantiate(messagePrefab, chatContentParent);

        // Find the TMP component in the prefab and set the text
        TMP_Text bubbleText = newBubble.GetComponentInChildren<TMP_Text>();
        if (bubbleText != null) bubbleText.text = msg;

        // Clear input
        messageInputField.text = "";

        // Force scroll to bottom
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void BackToLogin()
    {
        chatRoomPanel.SetActive(false);
        accessCodePanel.SetActive(true);
    }
}