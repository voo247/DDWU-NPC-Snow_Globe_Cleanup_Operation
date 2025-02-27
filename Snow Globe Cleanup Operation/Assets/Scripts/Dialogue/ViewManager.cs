using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] Image currSprite;
    [SerializeField] Image currBackground;
    [HideInInspector] public string currSpriteCharater = "";
    [HideInInspector] public bool skipTransition = false;
    
    public IEnumerator ChangeSprite(string name, string newSpriteName)
    {
        if (string.IsNullOrEmpty(newSpriteName))
        {
            if (currSprite.sprite != null)
            {
                yield return StartCoroutine(FadeOut(currSprite));
                currSprite.sprite = null;
                currSpriteCharater = "";
            }
            yield break;
        }

        // 새로운 이미지 가져오기
        Sprite newSprite = Resources.Load<Sprite>("캐릭터/" + name + " 대사 이미지/" + newSpriteName);
        if (currSprite != null && currSprite.sprite == newSprite)
        {
            yield break;
        }

        bool isSameCharacter = currSpriteCharater == name;

        if (!isSameCharacter)
        {
            yield return StartCoroutine(FadeOut(currSprite));
        }

        currSprite.sprite = newSprite;
        currSpriteCharater = name;

        if (!isSameCharacter)
        {
            yield return StartCoroutine(FadeIn(currSprite));
        }
    }
    
    public IEnumerator ChangeBackground(string newBackgroundName)
    {
        if (string.IsNullOrEmpty(newBackgroundName))
        {
            yield break;
        }

        // 새로운 이미지 가져오기
        Sprite newBackground = Resources.Load<Sprite>("배경/" + newBackgroundName);
        if (currBackground != null && currBackground.sprite == newBackground)
        {
            yield break;
        }

        currBackground.sprite = newBackground;
    }

    private IEnumerator FadeOut(Image img)
    {
        Color color = img.color;
        while (color.a > 0)
        {
            if (skipTransition)
            {
                color.a = 0;
                img.color = color;
                yield break;
            }
            color.a -= Time.deltaTime * fadeSpeed;
            img.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeIn(Image img)
    {
        Color color = img.color;
        while (color.a < 1)
        {
            if (skipTransition)
            {
                color.a = 1;
                img.color = color;
                yield break;
            }
            color.a += Time.deltaTime * fadeSpeed;
            img.color = color;
            yield return null;
        }
    }
}
