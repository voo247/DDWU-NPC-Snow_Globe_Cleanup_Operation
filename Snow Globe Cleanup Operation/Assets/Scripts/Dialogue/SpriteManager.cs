using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] Image currSprite;
    [HideInInspector] public string currSpriteCharater = "";
    
    public IEnumerator SpriteChangeCoroutine(string name, string newSpriteName)
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

        if (currSpriteCharater != name)
        {
            yield return StartCoroutine(FadeOut(currSprite));
        }

        currSprite.sprite = newSprite;

        if (currSpriteCharater != name)
        {
            yield return StartCoroutine(FadeIn(currSprite));
        }
        currSpriteCharater = name;
    }

    private IEnumerator FadeOut(Image img)
    {
        Color color = img.color;
        while (color.a > 0)
        {
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
            color.a += Time.deltaTime * fadeSpeed;
            img.color = color;
            yield return null;
        }
    }
}
