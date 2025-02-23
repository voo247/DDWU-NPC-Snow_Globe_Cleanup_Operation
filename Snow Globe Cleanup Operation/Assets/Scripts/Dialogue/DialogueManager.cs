using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// TODO
// 1. 텍스트 애니메이션 -> 엔터/스페이스바 동작 관련 코드 추가 필요 (애니메이션 진행 상태별로 애니메이션 종료/다음대사의 기능)
// 2. 배경 및 소품 추가 및 연결
// 3. 배경 전환 관련하여 csv 파일 추가 e.g. Background 1 2 -> 캔버스로 묶어서 전환하면 될 듯?
// 4. 

public class DialogueManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] Image fadeOverlay;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameUI;
    [SerializeField] private TMP_Text contextUI;
    [SerializeField] private Button nextButton;
    [SerializeField] private DialogueParser parser;
    [SerializeField] private SpriteManager spriteManager;

    private Dialogue[] dialogueList;
    private int currDialogueIdx;
    private int currContextIdx;

    private bool isTyping = false;
    private bool cancelTyping = false;

    void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(() => StartCoroutine(DisplayNextSentence()));
        
        dialoguePanel.SetActive(false);

        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            EndDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                cancelTyping = true;
            }
            else
            {
                StartCoroutine(DisplayNextSentence());
            }
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

        Dialogue currDialogue = dialogueList[currDialogueIdx];

        // 접두사로 "S_"(start)가 붙은 화면 이벤트 처리
        if (!string.IsNullOrEmpty(currDialogue.eventName[currContextIdx]) &&
            currDialogue.eventName[currContextIdx].StartsWith("S_"))
        {
            StartCoroutine(PlayDialogueEffects(currDialogue.eventName[currContextIdx].Substring(2)));
        }

        if (!currDialogue.name.StartsWith("#"))
        {
            nameUI.text = currDialogue.name;
        }
        else
        {
            nameUI.text = "";
        }
        StartCoroutine(TypeSentence(currDialogue.context[currContextIdx]));

        // 효과음 재생
        if (!string.IsNullOrEmpty(currDialogue.soundName[currContextIdx]))
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Dialogue/" + currDialogue.soundName[currContextIdx]);
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            }
            else
            {
                Debug.LogWarning("DialogueManager: 사운드 클립을 찾을 수 없음 (" + currDialogue.soundName[currContextIdx] + ")");
            }
        }

        // 스탠딩 이미지 변경
        StartCoroutine(spriteManager.SpriteChangeCoroutine(
            currDialogue.name, 
            currDialogue.spriteName[currContextIdx]
        ));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        cancelTyping = false;
        contextUI.text = "";
        
        foreach (char letter in sentence)
        {
            if (cancelTyping)
            {
                contextUI.text = sentence;
                break;
            }
            contextUI.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
        cancelTyping = false;
    }

    public IEnumerator DisplayNextSentence()
    {
        if (currDialogueIdx >= dialogueList.Length)
        {
            EndDialogue();
            yield break;
        }

        Dialogue currDialogue = dialogueList[currDialogueIdx];

        // 접두사로 "E_"(end)가 붙은 화면 이벤트 처리
        if (!string.IsNullOrEmpty(currDialogue.eventName[currContextIdx]) &&
            currDialogue.eventName[currContextIdx].StartsWith("E_"))
        {
            StartCoroutine(PlayDialogueEffects(currDialogue.eventName[currContextIdx].Substring(2)));
            yield return new WaitForSeconds(0.5f);
        }

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
        nameUI.text = "";
        contextUI.text = "";
        StartCoroutine(spriteManager.SpriteChangeCoroutine("", ""));
        dialoguePanel.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public IEnumerator PlayDialogueEffects(string eventName)
    {
        if (eventName.Equals("fade", System.StringComparison.OrdinalIgnoreCase))
        {
            yield return StartCoroutine(FadeOut(fadeOverlay));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FadeIn(fadeOverlay));

            // // 페이드 아웃-페이드 인 화면 전환 시 캐릭터 스프라이트가 달라질 때 페이드 화면 전환 일어나지 않도록 설정 (과한 애니메이션 방지)
            // if (currContextIdx < dialogueList[currDialogueIdx].context.Length - 1)
            // {
            //     spriteManager.currSpriteCharater = dialogueList[currDialogueIdx].spriteName[currContextIdx + 1];
            // }
            // else
            // {
            //     spriteManager.currSpriteCharater = dialogueList[currDialogueIdx + 1].spriteName[0];
            // }
        }
        else if (eventName.Equals("shake", System.StringComparison.OrdinalIgnoreCase))
        {
            yield return StartCoroutine(ScreenShake());
        }
    }

    private IEnumerator FadeOut(Image fadeOverlay)
    {
        Color color = fadeOverlay.color;
        while (color.a < 1)
        {
            color.a += Time.deltaTime * fadeSpeed;
            fadeOverlay.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeIn(Image fadeOverlay)
    {
        Color color = fadeOverlay.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            fadeOverlay.color = color;
            yield return null;
        }
    }

    private IEnumerator ScreenShake()
    {
        Vector3 originalPos = Camera.main.transform.position;
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float offsetX = Random.Range(-0.3f, 0.3f);
            float offsetY = Random.Range(-0.3f, 0.3f);
            Camera.main.transform.position = originalPos + new Vector3(offsetX, offsetY, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = originalPos;
    }
}
