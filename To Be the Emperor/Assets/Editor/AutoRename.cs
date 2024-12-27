using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

public class AutoRenameLevelObjects : EditorWindow
{
    private GameObject parentObject;

    [MenuItem("Tools/Auto Rename Level Objects")]
    public static void ShowWindow()
    {
        GetWindow<AutoRenameLevelObjects>("Auto Rename Level Objects");
    }

    void OnGUI()
    {
        GUILayout.Label("레벨 오브젝트 자동 네이밍", EditorStyles.boldLabel);

        parentObject = (GameObject)EditorGUILayout.ObjectField("부모 오브젝트", parentObject, typeof(GameObject), true);

        if (GUILayout.Button("네이밍 실행"))
        {
            RenameLevelObjects();
        }
    }

    void RenameLevelObjects()
    {
        if (parentObject == null)
        {
            Debug.LogWarning("부모 오브젝트를 설정하세요.");
            return;
        }

        // 부모 이름에서 숫자 파싱
        int parentIndex = ExtractIndexFromName(parentObject.name);
        if (parentIndex == -1)
        {
            Debug.LogWarning("부모 오브젝트 이름에서 인덱스를 찾을 수 없습니다.");
            return;
        }

        foreach (Transform child in parentObject.transform)
        {
            string newName = ReplaceIndexInName(child.name, parentIndex);
            child.name = newName;
        }

        EditorUtility.SetDirty(parentObject);
        Debug.Log($"레벨 {parentIndex} 오브젝트 네이밍 완료!");
    }

    // 부모 이름에서 숫자 추출
    int ExtractIndexFromName(string name)
    {
        Match match = Regex.Match(name, @"\d+");  // 숫자 추출
        if (match.Success)
        {
            return int.Parse(match.Value);
        }
        return -1;  // 숫자가 없으면 -1 반환
    }

    // 기존 이름에서 인덱스만 교체
    string ReplaceIndexInName(string originalName, int index)
    {
        // 뒤에 붙은 _숫자 부분만 교체 (예: _3 → _1)
        return Regex.Replace(originalName, @"_\d+$", $"_{index}");
    }
}
