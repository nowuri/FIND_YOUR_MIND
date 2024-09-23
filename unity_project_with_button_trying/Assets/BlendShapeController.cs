using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using static python_android;
using static button;

public class BlendController : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private python_android pythonJsonInstance;
    private button choiceInstance;

    private Dictionary<string, float> GetNeutralBlendShapes()
    {
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer is not assigned.");
            return new Dictionary<string, float>();
        }

        var neutralBlendShapes = new Dictionary<string, float>();

        // SkinnedMeshRenderer에서 BlendShape 이름의 개수 가져오기
        int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < blendShapeCount; i++)
        {
            // BlendShape의 이름 가져오기
            string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);

            // BlendShape 값을 0으로 설정
            neutralBlendShapes[blendShapeName] = 0f;
        }

        return neutralBlendShapes;
    }
    private readonly Dictionary<string, float> disgustBlendShapes1 = new Dictionary<string, float>
{
    {"V_Explosive", -50f},
    {"V_Wide", 100f},
    {"Brow_Raise_Outer_L", 20f},
    {"Brow_Raise_Outer_R", 20f},
    {"Brow_Drop_L", 15f},
    {"Brow_Drop_R", 15f},
    {"Eye_Squint_L", 60f},
    {"Eye_Squint_R", 40f},
    {"Nose_Sneer_L", 40f},
    {"Nose_Sneer_R", 40f},
    {"Cheek_Raise_L", 25f},
    {"Cheek_Raise_R", 25f},
    {"Mouth_Smile_Sharp_L", 10f},
    {"Mouth_Smile_Sharp_R", 10f},
    {"Mouth_Frown_L", 50f},
    {"Mouth_Frown_R", 50f},
    {"Mouth_Stretch_L", 10f},
    {"Mouth_Stretch_R", 10f},
    {"Mouth_Shrug_Upper", 30f},
    {"Mouth_Shrug_Lower", 30f},
    {"Mouth_Up_Upper_L", 30f},
    {"Mouth_Up_Upper_R", 20f},
    {"Mouth_Down_Lower_L", 20f},
    {"Mouth_Down_Lower_R", 20f},
    {"Jaw_Open", 5f}
};
    private readonly Dictionary<string, float> disgustBlendShapes2 = new Dictionary<string, float>
{
    {"Brow_Raise_Outer_L", 100f},
    {"Brow_Raise_Outer_R", 100f},
    {"Brow_Drop_L", 50f},
    {"Brow_Drop_R", 50f},
    {"Eye_Squint_L", 100f},
    {"Eye_Squint_R", 100f},
    {"Nose_Sneer_L", 40f},
    {"Nose_Sneer_R", 40f},
    {"Nose_Nostril_Raise_L", 60f},
    {"Nose_Nostril_Raise_R", 20f},
    {"Nose_Crease_L", 100f},
    {"Nose_Crease_R", 50f},
    {"Cheek_Raise_L", 100f},
    {"Cheek_Raise_R", 50f},
    {"Mouth_Stretch_L", 70f},
    {"Mouth_Stretch_R", 70f},
    {"Mouth_Funnel_Up_L", 100f},
    {"Mouth_Shrug_Upper", 40f},
    {"Mouth_Shrug_Lower", 30f},
    {"Mouth_Up_Upper_L", 20f},
    {"Mouth_Up_Upper_R", 20f},
    {"Mouth_Down_Lower_L", 20f},
    {"Mouth_Down_Lower_R", 20f},
    {"Jaw_Open", 30f}
};
    private readonly Dictionary<string, float> scaredBlendShapes1 = new Dictionary<string, float>
{
    {"V_Open", 30f},
    {"V_Wide", 100f},
    {"V_Lip_Open", 20f},
    {"Brow_Raise_Inner_L", 50f},
    {"Brow_Raise_Inner_R", 50f},
    {"Brow_Raise_Outer_L", 70f},
    {"Brow_Raise_Outer_R", 70f},
    {"Brow_Drop_L", -8f},
    {"Brow_Drop_R", -8f},
    {"Brow_Compress_L", 70f},
    {"Brow_Compress_R", 70f},
    {"Eye_Squint_L", 25f},
    {"Eye_Squint_R", 25f},
    {"Eye_Wide_L", 70f},
    {"Eye_Wide_R", 70f},
    {"Eyelash_Upper_Up_L", 50f},
    {"Eyelash_Upper_Up_R", 50f},
    {"Eyelash_Lower_Down_L", 30f},
    {"Eyelash_Lower_Down_R", 30f},
    {"Nose_Sneer_L", 15f},
    {"Nose_Sneer_R", 15f},
    {"Cheek_Raise_L", 100f},
    {"Cheek_Raise_R", 100f},
    {"Mouth_Smile_Sharp_L", 30f},
    {"Mouth_Smile_Sharp_R", 30f},
    {"Mouth_Frown_L", 30f},
    {"Mouth_Frown_R", 30f},
    {"Mouth_Push_Upper_L", 100f},
    {"Mouth_Push_Upper_R", 100f},
    {"Mouth_Up_Upper_L", 20f},
    {"Mouth_Up_Upper_R", 20f},
    {"Mouth_Down_Lower_L", 50f},
    {"Mouth_Down_Lower_R", 50f},
    {"Mouth_Chin_Up", 100f},
    {"Jaw_Open", 40f},
    {"Jaw_Up", 100f}
};
    private readonly Dictionary<string, float> scaredBlendShapes2 = new Dictionary<string, float>
{
    {"V_Explosive", -50f},
    {"V_Wide", 100f},
    {"V_Lip_Open", 20f},
    {"Brow_Raise_Inner_L", 75f},
    {"Brow_Raise_Inner_R", 75f},
    {"Brow_Raise_Outer_L", 75f},
    {"Brow_Raise_Outer_R", 75f},
    {"Brow_Drop_L", -22.5f},
    {"Brow_Drop_R", -22.5f},
    {"Eye_Squint_L", 80f},
    {"Eye_Squint_R", 80f},
    {"Eye_Wide_L", 80f},
    {"Eye_Wide_R", 80f},
    {"Nose_Sneer_L", 50f},
    {"Nose_Sneer_R", 50f},
    {"Cheek_Raise_L", 90f},
    {"Cheek_Raise_R", 90f},
    {"Mouth_Smile_Sharp_L", 10f},
    {"Mouth_Smile_Sharp_R", 10f},
    {"Mouth_Frown_L", 60f},
    {"Mouth_Frown_R", 60f},
    {"Mouth_Up_Upper_L", 30f},
    {"Mouth_Up_Upper_R", 30f},
    {"Mouth_Down_Lower_L", 50f},
    {"Mouth_Down_Lower_R", 50f},
    {"Jaw_Open", 80f},
    {"Jaw_Up", 60f}
};
    private readonly Dictionary<string, float> surprisedBlendShapes1 = new Dictionary<string, float>
{
    {"V_Open", 30f},
    {"V_Wide", 100f},
    {"V_Lip_Open", 20f},
    {"Brow_Raise_Inner_L", 50f},
    {"Brow_Raise_Inner_R", 50f},
    {"Brow_Raise_Outer_L", 70f},
    {"Brow_Raise_Outer_R", 70f},
    {"Brow_Compress_L", 70f},
    {"Brow_Compress_R", 70f},
    {"Eye_Squint_L", 25f},
    {"Eye_Squint_R", 25f},
    {"Eye_Wide_L", 40f},
    {"Eye_Wide_R", 40f},
    {"Eyelash_Upper_Up_L", 50f},
    {"Eyelash_Upper_Up_R", 50f},
    {"Eyelash_Lower_Down_L", 30f},
    {"Eyelash_Lower_Down_R", 30f},
    {"Nose_Snear_L", 15f},
    {"Nose_Snear_R", 15f},
    {"Cheek_Raise_L", 100f},
    {"Cheek_Raise_R", 100f},
    {"Mouth_Smile_Sharp_L", 30f},
    {"Mouth_Smile_Sharp_R", 30f},
    {"Mouth_Frown_L", 30f},
    {"Mouth_Frown_R", 30f},
    {"Mouth_Push_Upper_L", 100f},
    {"Mouth_Push_Upper_R", 100f},
    {"Mouth_Up_Upper_L", 20f},
    {"Mouth_Up_Upper_R", 20f},
    {"Mouth_Down_Lower_L", 50f},
    {"Mouth_Down_Lower_R", 50f},
    {"Mouth_Chin_Up", 100f},
    {"Jaw_Open", 40f},
    {"Jaw_Up", 100f}
};
    private readonly Dictionary<string, float> surprisedBlendShapes2 = new Dictionary<string, float>
{
    {"V_Explosive", -50f},
    {"V_Wide", 100f},
    {"V_Lip_Open", 20f},
    {"Brow_Raise_Inner_L", 75f},
    {"Brow_Raise_Inner_R", 75f},
    {"Brow_Raise_Outer_L", 75f},
    {"Brow_Raise_Outer_R", 75f},
    {"Brow_Drop_L", -40f},
    {"Brow_Drop_R", -40f},
    {"Eye_Squint_L", 5f},
    {"Eye_Squint_R", 5f},
    {"Eye_Wide_L", 60f},
    {"Eye_Wide_R", 60f},
    {"Nose_Nostril_Raise_L", 10f},
    {"Nose_Nostril_Raise_R", 10f},
    {"Cheek_Raise_L", 50f},
    {"Cheek_Raise_R", 50f},
    {"Mouth_Smile_Sharp_L", 25f},
    {"Mouth_Smile_Sharp_R", 25f},
    {"Mouth_Dimple_L", 30f},
    {"Mouth_Dimple_R", 30f},
    {"Mouth_Funnel_Up_L", 20f},
    {"Mouth_Funnel_Up_R", 20f},
    {"Mouth_Funnel_Down_L", 20f},
    {"Mouth_Funnel_Down_R", 20f},
    {"Mouth_Shrug_Upper", 20f},
    {"Mouth_Up_Upper_L", 20f},
    {"Mouth_Up_Upper_R", 20f},
    {"Mouth_Down_Lower_L", 45f},
    {"Mouth_Down_Lower_R", 45f},
    {"Jaw_Open", 55f}
};
    private readonly Dictionary<string, float> happyBlendShapes1 = new Dictionary<string, float>
{
    {"Brow_Raise_Inner_L", 50f},
    {"Brow_Raise_Inner_R", 50f},
    {"Brow_Raise_Outer_L", 50f},
    {"Brow_Raise_Outer_R", 50f},
    {"Eye_Wide_L", 50f},
    {"Eye_Wide_R", 50f},
    {"Mouth_Smile_L", 50f},
    {"Mouth_Smile_R", 50f},
    {"Mouth_Smile_Sharp_L", 30f},
    {"Mouth_Smile_Sharp_R", 30f},
    {"Mouth_Shrug_Upper", 35f},
    {"Mouth_Shrug_Lower", -35f},
    {"Mouth_Down_Lower_L", 45f},
    {"Mouth_Down_Lower_R", 45f},
    {"Mouth_Chin_Up", 100f},
    {"Jaw_Open", 20f},
    {"Jaw_Up", 40f}
};
    private readonly Dictionary<string, float> happyBlendShapes2 = new Dictionary<string, float>
{
    {"V_Explosive", -30f},
    {"V_Wide", 100f},
    {"Brow_Raise_Inner_L", 25f},
    {"Brow_Raise_Inner_R", 25f},
    {"Brow_Raise_Outer_L", 25f},
    {"Brow_Raise_Outer_R", 25f},
    {"Eye_Blink_L", 20f},
    {"Eye_Blink_R", 20f},
    {"Eye_Squint_L", 70f},
    {"Eye_Squint_R", 70f},
    {"Eye_Wide_L", 50f},
    {"Eye_Wide_R", 50f},
    {"Cheek_Raise_L", 70f},
    {"Cheek_Raise_R", 70f},
    {"Mouth_Smile_L", 90f},
    {"Mouth_Smile_R", 90f},
    {"Mouth_Smile_Sharp_L", 9f},
    {"Mouth_Smile_Sharp_R", 9f},
    {"Mouth_Shrug_Upper", 15f},
    {"Mouth_Shrug_Lower", -15f},
    {"Mouth_Down_Lower_L", 25f},
    {"Mouth_Down_Lower_R", 25f},
    {"Jaw_Open", 30f}
};
    private readonly Dictionary<string, float> angryBlendShapes1 = new Dictionary<string, float>
{
    {"V_Explosive", -30f},
    {"V_Wide", 100f},
    {"Brow_Raise_Outer_L", 50f},
    {"Brow_Raise_Outer_R", 50f},
    {"Brow_Drop_L", 40f},
    {"Brow_Drop_R", 40f},
    {"Nose_Sneer_L", 70f},
    {"Nose_Sneer_R", 70f},
    {"Cheek_Raise_L", 40f},
    {"Cheek_Raise_R", 40f},
    {"Mouth_Smile_Sharp_L", 10f},
    {"Mouth_Smile_Sharp_R", 10f},
    {"Mouth_Frown_L", 20f},
    {"Mouth_Frown_R", 20f},
    {"Mouth_Stretch_L", 30f},
    {"Mouth_Stretch_R", 30f},
    {"Mouth_Shrug_Upper", 15f},
    {"Mouth_Shrug_Lower", 20f},
    {"Mouth_Up_Upper_L", 50f},
    {"Mouth_Up_Upper_R", 50f},
    {"Mouth_Down_Lower_L", 40f},
    {"Mouth_Down_Lower_R", 40f},
    {"Jaw_Open", 15f}
};
    private readonly Dictionary<string, float> angryBlendShapes2 = new Dictionary<string, float>
{
    {"V_Open", 20f},
    {"V_Wide", 100f},
    {"V_Lip_Open", 30f},
    {"Brow_Raise_Outer_L", 30f},
    {"Brow_Raise_Outer_R", 30f},
    {"Brow_Drop_L", 100f},
    {"Brow_Drop_R", 100f},
    {"Brow_Compress_L", 80f},
    {"Brow_Compress_R", 80f},
    {"Eye_Wide_L", 80f},
    {"Eye_Wide_R", 80f},
    {"Eyelash_Upper_Up_L", 50f},
    {"Eyelash_Upper_Up_R", 50f},
    {"Eyelash_Lower_Down_L", 30f},
    {"Eyelash_Lower_Down_R", 30f},
    {"Nose_Sneer_L", 50f},
    {"Nose_Sneer_R", 50f},
    {"Nose_Nostril_Raise_L", 40f},
    {"Nose_Nostril_Raise_R", 40f},
    {"Nose_Crease_L", 30f},
    {"Nose_Crease_R", 30f},
    {"Cheek_Raise_L", 100f},
    {"Cheek_Raise_R", 100f},
    {"Cheek_Puff_L", 20f},
    {"Cheek_Puff_R", 20f},
    {"Mouth_Smile_Sharp_L", 30f},
    {"Mouth_Smile_Sharp_R", 30f},
    {"Mouth_Push_Upper_L", 100f},
    {"Mouth_Push_Upper_R", 100f},
    {"Mouth_Shrug_Upper", -8f},
    {"Mouth_Shrug_Lower", -25f},
    {"Mouth_Up_Upper_L", 75f},
    {"Mouth_Up_Upper_R", 75f},
    {"Mouth_Down_Lower_L", 75f},
    {"Mouth_Down_Lower_R", 75f},
    {"Mouth_Chin_Up", 100f},
    {"Jaw_Open", 52f},
    {"Jaw_Up", 100f},
    {"Eyelid_Outer_Down_L", 10f},
    {"Eyelid_Outer_Down_R", 10f}
};
    private readonly Dictionary<string, float> sadBlendShapes1 = new Dictionary<string, float>
{
    {"V_Explosive", -30f},
    {"V_Wide", 100f},
    {"Brow_Raise_Inner_L", 70f},
    {"Brow_Raise_Inner_R", 70f},
    {"Brow_Drop_L", -30f},
    {"Brow_Drop_R", -30f},
    {"Eye_Squint_L", 50f},
    {"Eye_Squint_R", 50f},
    {"Cheek_Raise_L", 50f},
    {"Cheek_Raise_R", 50f},
    {"Mouth_Smile_Sharp_L", 10f},
    {"Mouth_Smile_Sharp_R", 10f},
    {"Mouth_Frown_L", 20f},
    {"Mouth_Frown_R", 20f},
    {"Mouth_Roll_Out_Lower_L", 30f},
    {"Mouth_Roll_Out_Lower_R", 30f},
    {"Mouth_Shrug_Upper", 10f},
    {"Mouth_Shrug_Lower", 20f},
    {"Jaw_Open", 15f}
};
    private readonly Dictionary<string, float> sadBlendShapes2 = new Dictionary<string, float>
{
    {"V_Open", 20f},
    {"V_Wide", 100f},
    {"V_Lip_Open", 30f},
    {"Brow_Raise_Inner_L", 75f},
    {"Brow_Raise_Inner_R", 75f},
    {"Brow_Drop_L", -40f},
    {"Brow_Drop_R", -40f},
    {"Brow_Compress_L", 80f},
    {"Brow_Compress_R", 80f},
    {"Eye_Squint_L", 75f},
    {"Eye_Squint_R", 75f},
    {"Eyelash_Upper_Up_L", 50f},
    {"Eyelash_Upper_Up_R", 50f},
    {"Eyelash_Lower_Down_L", 30f},
    {"Eyelash_Lower_Down_R", 30f},
    {"Nose_Nostril_Raise_L", 40f},
    {"Nose_Nostril_Raise_R", 40f},
    {"Nose_Crease_L", 30f},
    {"Nose_Crease_R", 30f},
    {"Cheek_Raise_L", 75f},
    {"Cheek_Raise_R", 75f},
    {"Mouth_Frown_L", 50f},
    {"Mouth_Frown_R", 50f},
    {"Mouth_Roll_Out_Lower_L", 45f},
    {"Mouth_Roll_Out_Lower_R", 45f},
    {"Mouth_Push_Upper_L", 100f},
    {"Mouth_Push_Upper_R", 100f},
    {"Mouth_Shrug_Upper", 20f},
    {"Mouth_Shrug_Lower", 60f},
    {"Mouth_Chin_Up", 100f},
    {"Jaw_Open", 15f},
    {"Jaw_Up", 10f},
    {"Eyelid_Outer_Down_L", 10f},
    {"Eyelid_Outer_Down_R", 10f}
};


    void Start()
    {
        // python_json 클래스의 인스턴스를 가져옵니다.
        pythonJsonInstance = FindObjectOfType<python_android>();
        choiceInstance = FindObjectOfType<button>();

        if (pythonJsonInstance == null)
        {
            // 오류를 기록하고 반환합니다.
            Debug.LogError("python_json instance not found!");
            return;
        }

        // SkinnedMeshRenderer를 가져옵니다.
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer == null)
        {
            // 오류를 기록하고 반환합니다.
            Debug.LogError("SkinnedMeshRenderer를 찾을 수 없습니다!");
            return;
        }

        /*while (true)
        {
            if (choiceInstance.charLoad)
            {
                //Debug.Log("** Blend1 start **");
                StartCoroutine(AnimateBlendShapes());
                break;
            }
            else { }//Debug.Log("** loading... **, BlendShapeC1");
        }*/
        StartCoroutine(AnimateBlendShapes());
    }

    private string GetEmotion()
    {
        if (pythonJsonInstance != null)
        {
            // python_json 클래스의 LatestEmotionScores 속성을 가져옵니다.
            return pythonJsonInstance.emotion;
        }
        else
        {
            Debug.LogError("python_json instance is null!");
            return null;
        }
    }


    // 블렌드 쉐이프를 애니메이트하는 코루틴
    private IEnumerator AnimateBlendShapes()
    {
        while (true)
        {
            string emotion = GetEmotion();
            bool strong = pythonJsonInstance.strong;
            //{ 0: 'Anger', 1: 'Disgust', 2: 'Fear=scared', 3: 'Happiness', 4: 'Neutral', 5: 'Sadness', 6: 'Surprise'}

            // 가장 높은 확률의 감정에 따라 블렌드 쉐이프를 전환합니다.
            switch (emotion)
            {
                case "Anger":
                    Debug.Log("0,Angry로 전환");
                    if (!strong)
                    {
                        StartCoroutine(BlendShapeTransition(GetNeutralBlendShapes(), 0.5f));
                        yield return StartCoroutine(BlendShapeTransition(angryBlendShapes1, 1.0f));
                    }
                    else
                    { yield return StartCoroutine(BlendShapeTransition(angryBlendShapes2, 1.0f)); }
                    break;

                case "Disgust":
                    Debug.Log("1,Disgust로 전환");
                    if (!strong)
                    {
                        StartCoroutine(BlendShapeTransition(GetNeutralBlendShapes(), 0.5f));
                        yield return StartCoroutine(BlendShapeTransition(disgustBlendShapes1, 1.0f));
                    }
                    else
                    { yield return StartCoroutine(BlendShapeTransition(disgustBlendShapes2, 1.0f)); }
                    break;

                case "Fear":
                    Debug.Log("2,Fear로 전환");
                    if (!strong)
                    {
                        StartCoroutine(BlendShapeTransition(GetNeutralBlendShapes(), 0.5f));
                        yield return StartCoroutine(BlendShapeTransition(scaredBlendShapes1, 1.0f));
                    }
                    else
                    { yield return StartCoroutine(BlendShapeTransition(scaredBlendShapes2, 1.0f)); }
                    break;

                case "Happiness":
                    Debug.Log("3,Happy로 전환");
                    if (!strong)
                    {
                        StartCoroutine(BlendShapeTransition(GetNeutralBlendShapes(), 0.5f));
                        yield return StartCoroutine(BlendShapeTransition(happyBlendShapes1, 1.0f));
                    }
                    else
                    { yield return StartCoroutine(BlendShapeTransition(happyBlendShapes2, 1.0f)); }
                    break;

                case "Sadness":
                    Debug.Log("5,Sad로 전환");
                    if (!strong)
                    {
                        StartCoroutine(BlendShapeTransition(GetNeutralBlendShapes(), 0.5f));
                        yield return StartCoroutine(BlendShapeTransition(sadBlendShapes1, 1.0f));
                    }
                    else
                    { yield return StartCoroutine(BlendShapeTransition(sadBlendShapes2, 1.0f)); }
                    break;

                case "Surprise":
                    Debug.Log("6,Surprise로 전환");
                    if (!strong)
                    {
                        StartCoroutine(BlendShapeTransition(GetNeutralBlendShapes(), 0.5f));
                        yield return StartCoroutine(BlendShapeTransition(surprisedBlendShapes1, 1.0f));
                    }
                    else
                    { yield return StartCoroutine(BlendShapeTransition(surprisedBlendShapes2, 1.0f)); }
                    break;

                default:
                    // 기타 감정이나 감정이 없을 경우 Neutral로 전환합니다.
                    Debug.Log("Neutral로 전환");
                    yield return StartCoroutine(BlendShapeTransition(GetNeutralBlendShapes(), 1.0f));
                    break;
            }

            // 0.05초 대기 후 다시 감정 값을 업데이트합니다.
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 블렌드 쉐이프 전환을 수행하는 코루틴
    private IEnumerator BlendShapeTransition(Dictionary<string, float> targetValues, float duration)
    {
        float elapsedTime = 0f;
        Dictionary<string, float> initialBlendShapes = GetCurrentBlendShapes();
        //Dictionary<string, float> neutralBlendShapes = GetNeutralBlendShapes();

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 각 블렌드 쉐이프를 보간하여 전환합니다.
            foreach (var blendShape in targetValues)
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    float initialWeight = initialBlendShapes.ContainsKey(blendShape.Key) ? initialBlendShapes[blendShape.Key] : 0f;
                    float targetWeight = blendShape.Value;
                    float currentWeight = Mathf.Lerp(initialWeight, targetWeight, t);
                    skinnedMeshRenderer.SetBlendShapeWeight(index, currentWeight);
                }
            }

            yield return null;
        }

        // 최종적으로 목표 블렌드 쉐이프 값을 설정합니다.
        foreach (var blendShape in targetValues)
        {
            int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
            if (index != -1)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(index, blendShape.Value);
            }
        }
    }

    // 현재의 블렌드 쉐이프 값들을 가져오는 메서드
    private Dictionary<string, float> GetCurrentBlendShapes()
    {
        Dictionary<string, float> currentBlendShapes = new Dictionary<string, float>();

        // Disgust 표정의 블렌드 쉐이프 값을 가져옵니다.
        foreach (var blendShape in disgustBlendShapes1)
        {
            int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
            if (index != -1)
            {
                currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
            }
        }
        foreach (var blendShape in disgustBlendShapes2)
        {
            int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
            if (index != -1)
            {
                currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
            }
        }

        // Happiness 표정의 블렌드 쉐이프 값을 가져옵니다.
        foreach (var blendShape in happyBlendShapes1)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }
        foreach (var blendShape in happyBlendShapes2)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }

        // Surprised 표정의 블렌드 쉐이프 값을 가져옵니다.
        foreach (var blendShape in surprisedBlendShapes1)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }
        foreach (var blendShape in surprisedBlendShapes2)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }

        // Angry 표정의 블렌드 쉐이프 값을 가져옵니다.
        foreach (var blendShape in angryBlendShapes1)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }
        foreach (var blendShape in angryBlendShapes2)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }

        // Scared 표정의 블렌드 쉐이프 값을 가져옵니다.
        foreach (var blendShape in scaredBlendShapes1)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }
        foreach (var blendShape in scaredBlendShapes2)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }

        // Sad 표정의 블렌드 쉐이프 값을 가져옵니다.
        foreach (var blendShape in sadBlendShapes1)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }
        foreach (var blendShape in sadBlendShapes2)
        {
            if (!currentBlendShapes.ContainsKey(blendShape.Key))
            {
                int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.Key);
                if (index != -1)
                {
                    currentBlendShapes[blendShape.Key] = skinnedMeshRenderer.GetBlendShapeWeight(index);
                }
            }
        }

        return currentBlendShapes;
    }
}