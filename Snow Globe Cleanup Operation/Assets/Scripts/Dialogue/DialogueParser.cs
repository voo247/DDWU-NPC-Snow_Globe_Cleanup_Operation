using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    [SerializeField] public TextAsset csvFile;
    
    public Dialogue[] Parse()
    {
        if (csvFile == null)
        {
            Debug.LogError("DialogueParser: CSV 파일이 설정되지 않음.");
            return new Dialogue[0];
        }

        List<Dialogue> dialogueList = new List<Dialogue>();

        string[] data = csvFile.text.Split(new char[]{'\n'});

        // data[0] = CSV 데이터의 첫 줄
        for (int i = 1; i < data.Length - 1;)
        {
            string[] row = ParseCSVLine(data[i]);
            
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[1].Trim();

            List<string> contextList = new List<string>();
            List<string> spriteList = new List<string>();
            
            do
            {
                contextList.Add(row[2].Trim());
                spriteList.Add(row[3].Trim());

                if (++i >= data.Length - 1)
                {
                    break;
                }

                row = ParseCSVLine(data[i]);
            } while (string.IsNullOrEmpty(row[1])); // 캐릭터 네임이 공백이면 계속해서 대사
            
            dialogue.context = contextList.ToArray();
            dialogue.spriteName = spriteList.ToArray();

            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }

    private string[] ParseCSVLine(string line)
    {
        List<string> rslt = new List<string>();
        string pattern = "(?<=^|,)(\"[^\"]*\"|[^,]*)";
        MatchCollection matches = Regex.Matches(line, pattern);

        foreach (Match match in matches)
        {
            string value = match.Value.Trim();

            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.Substring(1, value.Length - 2).Replace("\"\"", "\"");
            }

            rslt.Add(value);
        }

        return rslt.ToArray();
    }
}
