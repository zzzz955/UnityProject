using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
{
    // LevelTransition_Next에 닿았을 경우
    if (other.gameObject.name.Contains("LevelTransition_Next"))
    {
        Debug.Log($"{other.gameObject.name}에 닿았습니다.");
        TeleportToSpawn("PlayerSpawn_Entry", 1, other.gameObject.name);
    }
    // LevelTransition_Previous에 닿았을 경우
    else if (other.gameObject.name.Contains("LevelTransition_Previous"))
    {
        Debug.Log($"{other.gameObject.name}에 닿았습니다.");
        TeleportToSpawn("PlayerSpawn_Exit", -1, other.gameObject.name);
    }
}


    // 플레이어를 해당 인덱스의 스폰 포인트로 이동
    void TeleportToSpawn(string spawnType, int offset, string transitionName)
    {
        // 현재 트리거의 인덱스를 추출
        int currentIndex = ExtractIndex(transitionName);

        // 이동할 타겟 인덱스 설정
        int targetIndex = currentIndex + offset;

        // 이동할 스폰 포인트 이름 구성
        string targetSpawnName = $"{spawnType}_{targetIndex}";

        // 타겟 스폰 포인트 찾기
        GameObject targetSpawn = GameObject.Find(targetSpawnName);

        // 타겟 위치로 플레이어 이동
        if (targetSpawn != null)
        {
            transform.position = targetSpawn.transform.position;
            Debug.Log($"{targetSpawnName}로 이동!");
        }
        else
        {
            Debug.LogWarning($"{targetSpawnName}를 찾을 수 없습니다.");
        }
    }

    // 오브젝트 이름에서 숫자 인덱스를 추출하는 메서드
    int ExtractIndex(string name)
    {
        // 이름에서 마지막 "_" 이후의 숫자 부분 추출
        int index = 1;  // 기본값
        string[] splitName = name.Split('_');

        if (splitName.Length > 1 && int.TryParse(splitName[splitName.Length - 1], out int result))
        {
            index = result;
        }
        return index;
    }
}
