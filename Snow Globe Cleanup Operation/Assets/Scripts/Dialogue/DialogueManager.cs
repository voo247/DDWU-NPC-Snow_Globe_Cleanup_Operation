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
    [SerializeField] public RectTransform[] UI_elements;
    [SerializeField] private TMP_Text nameUI;
    [SerializeField] private TMP_Text contextUI;
    [SerializeField] private Button nextButton;
    [SerializeField] private DialogueParser parser;
    [SerializeField] private ViewManager viewManager;

    private Dialogue[] dialogueList;
    private int currDialogueIdx;
    private int currContextIdx;

    private bool isTyping = false;
    private bool skipTyping = false;

    void Start()
    {
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(() =>
            {
                if (isTyping)
                {
                    skipTyping = true;
                    isTyping = false;
                    viewManager.skipTransition = true;
                }
                else
                {
                    StartCoroutine(DisplayNextSentence());
                }
            });
        }
        
        dialoguePanel.SetActive(false);

        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            skipTyping = true;
            isTyping = false;
            viewManager.skipTransition = true;
            EndDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                skipTyping = true;
                isTyping = false;
                viewManager.skipTransition = true;
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

        viewManager.skipTransition = false;

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
        
        if (!skipTyping)
        {
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
        }

        // 이미지 변경
        StartCoroutine(viewManager.SpriteChangeCoroutine(
            currDialogue.name, 
            currDialogue.spriteName[currContextIdx]
        ));
        StartCoroutine(viewManager.BackgroundChangeCoroutine(
            currDialogue.backgroundName[currContextIdx]
        ));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        skipTyping = false;
        contextUI.text = "";
        
        foreach (char letter in sentence)
        {
            if (skipTyping)
            {
                contextUI.text = sentence;
                isTyping = false;
                skipTyping = false;
                yield break;
            }
            contextUI.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        isTyping = false;
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
        StartCoroutine(viewManager.SpriteChangeCoroutine("", ""));
        dialoguePanel.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public IEnumerator PlayDialogueEffects(string eventName)
    {
        if (eventName.Equals("fade", System.StringComparison.OrdinalIgnoreCase))
        {
            if (skipTyping)
            {
                yield break;
            }
            yield return StartCoroutine(FadeOut(fadeOverlay));
            if (!skipTyping) yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FadeIn(fadeOverlay));
        }
        else if (eventName.Equals("shake", System.StringComparison.OrdinalIgnoreCase))
        {
            if (skipTyping)
            {
                yield break;
            }
            yield return StartCoroutine(ScreenShake(UI_elements));
        }
    }

    private IEnumerator FadeOut(Image fadeOverlay)
    {
        Color color = fadeOverlay.color;
        while (color.a < 1)
        {
            if (skipTyping)
            {
                color.a = 1;
                fadeOverlay.color = color;
                yield break;
            }
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
            if (skipTyping)
            {
                color.a = 0;
                fadeOverlay.color = color;
                yield break;
            }
            color.a -= Time.deltaTime * fadeSpeed;
            fadeOverlay.color = color;
            yield return null;
        }
    }

    private IEnumerator ScreenShake(RectTransform[] UI_elements)
    {
        Vector2[] originalPositions = new Vector2[UI_elements.Length];
        for (int i = 0; i < UI_elements.Length; i++)
        {
            originalPositions[i] = UI_elements[i].anchoredPosition;
        }

        float duration = 1f;
        float elapsed = 0f;
        float magnitude = 10f;

        // UI 오브젝트 흔들기
        while (elapsed < duration)
        {
            if (skipTyping)
            {
                break;
            }
            for (int i = 0; i < UI_elements.Length; i++)
            {
                float offsetX = Random.Range(-1f, 1f) * (magnitude - elapsed * 2);
                float offsetY = Random.Range(-1f, 1f) * (magnitude - elapsed * 2);
                UI_elements[i].anchoredPosition = originalPositions[i] + new Vector2(offsetX, offsetY);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 모든 UI 오브젝트를 원래 위치로 복귀
        for (int i = 0; i < UI_elements.Length; i++)
        {
            UI_elements[i].anchoredPosition = originalPositions[i];
        }
    }
}
