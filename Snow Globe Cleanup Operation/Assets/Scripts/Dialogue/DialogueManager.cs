using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

// TODO
// 1. 텍스트 애니메이션 -> 엔터/스페이스바 동작 관련 코드 추가 필요 (애니메이션 진행 상태별로 애니메이션 종료/다음대사의 기능)
// 2. 배경 및 소품 추가 및 연결
// 3. 배경 전환 관련하여 csv 파일 추가 e.g. Background 1 2 -> 캔버스로 묶어서 전환하면 될 듯?
// 4. 

public class DialogueManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] Image fadeOverlay;
    [SerializeField] Image snowGlobe;
    [SerializeField] public RectTransform[] UI_elements;
    [SerializeField] private TMP_Text nameUI;
    [SerializeField] private TMP_Text contextUI;
    [SerializeField] private Button nextButton;
    [SerializeField] private DialogueParser parser;
    [SerializeField] private ViewManager viewManager;
    [SerializeField] private SoundEffectManager soundEffectManager;

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
                    callSkip();
                }
                else
                {
                    StartCoroutine(DisplayNextSentence());
                }
            });
        }
        
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            skipTyping = true;
            isTyping = false;
            viewManager.skipTransition = true;
            soundEffectManager.StopGeneralSound();
            EndDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                callSkip();
            }
            else
            {
                StartCoroutine(DisplayNextSentence());
            }
        }
    }

    private void callSkip()
    {
        skipTyping = true;
        isTyping = false;
        viewManager.skipTransition = true;
        soundEffectManager.StopGeneralSound();
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
        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        if (!(currDialogueIdx < dialogueList.Length && currContextIdx < dialogueList[currDialogueIdx].context.Length))
        {
            EndDialogue();
            yield break;
        }

        // Debug.Log($"{currDialogueIdx}:{currContextIdx}");

        viewManager.skipTransition = false;

        Dialogue currDialogue = dialogueList[currDialogueIdx];

        if (!currDialogue.name.StartsWith("#"))
        {
            nameUI.text = "_ " + currDialogue.name;
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
                soundEffectManager.PlayGeneralSound(currDialogue.soundName[currContextIdx]);
            }
        }

        // 이미지 변경
        StartCoroutine(viewManager.ChangeSprite(
            currDialogue.name, 
            currDialogue.spriteName[currContextIdx]
        ));
        StartCoroutine(viewManager.ChangeBackground(
            currDialogue.backgroundName[currContextIdx]
        ));

        // 접두사로 "S_"(start)가 붙은 화면 이벤트 처리
        if (!string.IsNullOrEmpty(currDialogue.eventName[currContextIdx]) &&
            currDialogue.eventName[currContextIdx].StartsWith("S_"))
        {
            if (currDialogue.eventName[currContextIdx].Equals("S_shake", System.StringComparison.OrdinalIgnoreCase)) yield return new WaitForSeconds(0.1f);
            StartCoroutine(PlayDialogueEffects(currDialogue.eventName[currContextIdx][2..]));
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        contextUI.text = "";
        isTyping = true;
        skipTyping = false;
        soundEffectManager.PlayTypingSound("whatsapp-typing-and-sending-message-sound-effect-204192");

        // 단어별 애니메이션
        string[] words = sentence.Split(' ');
        foreach (string word in words)
        {
            if (skipTyping)
            {
                contextUI.text = sentence;
                isTyping = false;
                skipTyping = false;
                soundEffectManager.StopTypingSound();
                yield break;
            }
            contextUI.text += word + " ";
            yield return new WaitForSeconds(0.1f);
        }

        // // 문자별 애니메이션 -> TMP tag 사용위해서 단어별 애니메이션으로 대체하였음
        // foreach (char letter in sentence)
        // {
        //     if (skipTyping)
        //     {
        //         contextUI.text = sentence;
        //         isTyping = false;
        //         skipTyping = false;
        //         yield break;
        //     }
        //     contextUI.text += letter;
        //     yield return new WaitForSeconds(0.01f);
        // }

        isTyping = false;
        soundEffectManager.StopTypingSound();
    }

    public IEnumerator DisplayNextSentence()
    {
        soundEffectManager.StopGeneralSound();

        Dialogue currDialogue = dialogueList[currDialogueIdx];

        // 접두사로 "E_"(end)가 붙은 화면 이벤트 처리
        if (!string.IsNullOrEmpty(currDialogue.eventName[currContextIdx]) &&
            currDialogue.eventName[currContextIdx].StartsWith("E_"))
        {
            StartCoroutine(PlayDialogueEffects(currDialogue.eventName[currContextIdx][2..]));
            yield return new WaitForSeconds(1f);
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

        StartCoroutine(DisplayDialogue());
    }

    private void EndDialogue()
    {
        if (parser.csvFile.name.Equals("StartStory"))SceneManager.LoadScene("MAIN");
        if (parser.csvFile.name.Equals("HappyEnding"))SceneManager.LoadScene("HAPPYENDING");
        if (parser.csvFile.name.Equals("BadEnding"))SceneManager.LoadScene("BADENDING");
        // nameUI.text = "";
        // contextUI.text = "";
        // StartCoroutine(viewManager.ChangeSprite("", ""));
    }

    public IEnumerator PlayDialogueEffects(string eventName)
    {
        if (eventName.Equals("fade", System.StringComparison.OrdinalIgnoreCase))
        {
            if (skipTyping)
            {
                if(snowGlobe != null) snowGlobe.enabled = false;
                yield break;
            }
            if(snowGlobe != null) yield return StartCoroutine(ScreenShake(new RectTransform[] { snowGlobe.rectTransform }));
            yield return StartCoroutine(FadeOut(fadeOverlay));
            if(snowGlobe != null) snowGlobe.enabled = false;
            if (!skipTyping) yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FadeIn(fadeOverlay));
        }
        if (eventName.Equals("fadeOut", System.StringComparison.OrdinalIgnoreCase))
        {
            yield return StartCoroutine(FadeOut(fadeOverlay));
        }
        if (eventName.Equals("fadeIn", System.StringComparison.OrdinalIgnoreCase))
        {
            yield return StartCoroutine(FadeIn(fadeOverlay));
        }
        else if (eventName.Equals("findDirtySnowGlobe", System.StringComparison.OrdinalIgnoreCase))
        {
            snowGlobe.sprite = Resources.Load<Sprite>("아이템/스노우볼/더러워진 스노우 볼");
            yield return StartCoroutine(FadeOut(snowGlobe)); // black overlay 이미지의 alpha값 증가시키는 fadeout함수 활용
        }
        else if (eventName.Equals("findBeautifulSnowGlobe", System.StringComparison.OrdinalIgnoreCase))
        {
            snowGlobe.sprite = Resources.Load<Sprite>("아이템/스노우볼/깨끗해진 스노우 볼");
            yield return StartCoroutine(FadeOut(snowGlobe)); // black overlay 이미지의 alpha값 증가시키는 fadeout함수 활용
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

        float duration = 0.7f;
        float elapsed = 0f;
        float magnitude = 5f;

        // UI 오브젝트 흔들기
        while (elapsed < duration)
        {
            if (skipTyping)
            {
                break;
            }
            for (int i = 0; i < UI_elements.Length; i++)
            {
                float weight = magnitude - elapsed * 5f; // 점차 약하게 흔들기
                weight = (weight > 0)? weight : -weight;
                float offsetX = Random.Range(-1f, 1f) * weight;
                float offsetY = Random.Range(-1f, 1f) * weight;
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
