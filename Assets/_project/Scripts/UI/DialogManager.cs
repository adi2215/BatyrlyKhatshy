using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backGroundBox;
    public ManagerScene scene;

    public bool isActive = false;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        DisplayMessage();
        backGroundBox.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.5f).SetEase(Ease.InOutExpo);
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            backGroundBox.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutExpo);
            StartCoroutine(LastScene());
        }
    }

    public IEnumerator LastScene()
    {
        yield return new WaitForSeconds(1.5f);
        scene.LoadNextLevel(1);
    } 

    void Start()
    {
        backGroundBox.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextMessage();
        }
    }
}
