using UnityEngine;

public class Choose_Character : MonoBehaviour
{
    void Start()
    {
        // UnityPlayer 클래스를 통해 현재 안드로이드 액티비티에 접근
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // 안드로이드의 SharedPreferences에 저장된 값 가져오기
        AndroidJavaObject prefs = currentActivity.Call<AndroidJavaObject>("getSharedPreferences", "UnityData", 0);
        int receivedValue = prefs.Call<int>("getInt", "selectedCharacter", -1); // 기본값은 -1

        if (receivedValue != -1)
        {
            Debug.Log("Received Character Value: " + receivedValue);
            // 여기서 받은 값에 따라 캐릭터 선택 로직을 구현할 수 있습니다.
            ChooseCharacter(receivedValue);
        }
        else
        {
            Debug.LogError("Failed to receive character value from Android.");
        }
    }

    void ChooseCharacter(int characterValue)
    {
        // 캐릭터 선택 로직을 이곳에 구현하세요.
        // 예를 들어, characterValue에 따라 다른 캐릭터를 로드하거나 활성화할 수 있습니다.
        Debug.Log("Character " + characterValue + " selected.");
    }
}
