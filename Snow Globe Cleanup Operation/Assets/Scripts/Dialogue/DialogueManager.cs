using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameUI;
    [SerializeField] private TMP_Text contextUI;
    [SerializeField] private Button nextButton;
    [SerializeField] private DialogueParser parser;
    [SerializeField] private SpriteManager spriteManager;

    private Dialogue[] dialogueList;
    private int currDialogueIdx;
    private int currContextIdx;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(DisplayNextSentence);
        
        dialoguePanel.SetActive(false);

        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            EndDialogue();
        }
    }

    public void StartDialogue()
    {
        dialogueList = parser.Parse();
        if (dialogueList == null || dialogueList.Length == 0)
        {
            Debug.LogWarning("DialogueManager: 출력가능한 대사가 없음.");
            return;
        }

        currDialogueIdx = 0;
        currContextIdx = 0;
        dialoguePanel.SetActive(true);
        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        if (currDialogueIdx >= dialogueList.Length)
        {
            EndDialogue();
            return;
        }

        Dialogue currentDialogue = dialogueList[currDialogueIdx];

        nameUI.text = currentDialogue.name; 
        contextUI.text = currentDialogue.context[currContextIdx];

        // 스탠딩 이미지 변경
        StartCoroutine(spriteManager.SpriteChangeCoroutine(
            currentDialogue.name, 
            currentDialogue.spriteName[currContextIdx]
        ));
    }

    public void DisplayNextSentence()
    {
        if (currDialogueIdx >= dialogueList.Length)
        {
            EndDialogue();
            return;
        }

        Dialogue currDialogue = dialogueList[currDialogueIdx];

        if (currContextIdx < currDialogue.context.Length - 1)
        {
            currContextIdx++;
        }
        else
        {
            currDialogueIdx++;
            currContextIdx = 0;
        }

        if (currDialogueIdx < dialogueList.Length)
        {
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        nameUI = null;
        contextUI = null;
        StartCoroutine(spriteManager.SpriteChangeCoroutine("", ""));
        dialoguePanel.SetActive(false);
        SceneManager.LoadScene("Game");
    }
}
