using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using static python_android;
using static button;

public class BlendController2 : MonoBehaviour
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
    {"Brow_Raise_Outer_Left", 100f},
    {"Brow_Raise_Outer_Right", 100f},
    {"Brow_Raise_Left", 20f},
    {"Brow_Raise_Right", 20f},
    {"Eye_Squint_L", 100f},
    {"Eye_Squint_R", 100f},
    {"Nose_Flanks_Raise", 60f},
    {"Mouth_Frown", 60f},
    {"Mouth_Snarl_Upper_L", 40f},
    {"Mouth_Open", 40f},
    {"A02_Brow_Down_Left", 30f},
    {"A03_Brow_Down_Right", 30f},
    {"A04_Brow_Outer_Up_Left", 35f},
    {"A05_Brow_Outer_Up_Right", 35f},
    {"A21_Cheek_Squint_Left", 50f},
    {"A22_Cheek_Squint_Right", 50f},
    {"A23_Nose_Sneer_Left", 50f},
    {"A24_Nose_Sneer_Right", 50f},
    {"A25_Jaw_Open", 50f},
    {"A36_Mouth_Shrug_Lower", 60f},
    {"A40_Mouth_Frown_Left", 20f},
    {"A41_Mouth_Frown_Right", 20f},
    {"A44_Mouth_Upper_Up_Left", 35f},
    {"A45_Mouth_Upper_Up_Right", 35f},
    {"A46_Mouth_Lower_Down_Left", 25f},
    {"A47_Mouth_Lower_Down_Right", 25f},
    {"A50_Mouth_Stretch_Left", 40f},
    {"A51_Mouth_Stretch_Right", 40f}
};
    private readonly Dictionary<string, float> scaredBlendShapes1 = new Dictionary<string, float>
{
    {"Brow_Raise_Outer_Left", 100f},
    {"Brow_Raise_Outer_Right", 100f},
    {"Brow_Raise_Left", 20f},
    {"Brow_Raise_Right", 20f},
    {"Eye_Squint_L", 50f},
    {"Eye_Squint_R", 50f},
    {"Mouth_Down", 55f},
    {"Mouth_Open", 50f},
    {"A01_Brow_Inner_Up", 75f},
    {"A02_Brow_Down_Left", -22.5f},
    {"A03_Brow_Down_Right", -22.5f},
    {"A04_Brow_Outer_Up_Left", 75f},
    {"A05_Brow_Outer_Up_Right", 75f},
    {"A16_Eye_Squint_Left", 75f},
    {"A17_Eye_Squint_Right", 75f},
    {"A18_Eye_Wide_Left", 75f},
    {"A19_Eye_Wide_Right", 75f},
    {"A21_Cheek_Squint_Left", 75f},
    {"A22_Cheek_Squint_Right", 75f},
    {"A23_Nose_Sneer_Left", 25f},
    {"A24_Nose_Sneer_Right", 25f},
    {"A40_Mouth_Frown_Left", 30f},
    {"A41_Mouth_Frown_Right", 30f},
    {"A44_Mouth_Upper_Up_Left", 30f},
    {"A45_Mouth_Upper_Up_Right", 30f},
    {"A46_Mouth_Lower_Down_Left", 55f},
    {"A47_Mouth_Lower_Down_Right", 55f}
};
    private readonly Dictionary<string, float> surprisedBlendShapes1 = new Dictionary<string, float>
{
    {"Brow_Raise_Outer_Left", 100f},
    {"Brow_Raise_Outer_Right", 100f},
    {"Brow_Raise_Left", 20f},
    {"Brow_Raise_Right", 20f},
    {"Mouth_Open", 70f},
    {"A01_Brow_Inner_Up", 50f},
    {"A02_Brow_Down_Left", -25f},
    {"A03_Brow_Down_Right", -25f},
    {"A04_Brow_Outer_Up_Left", 50f},
    {"A05_Brow_Outer_Up_Right", 50f},
    {"A18_Eye_Wide_Left", 50f},
    {"A19_Eye_Wide_Right", 50f},
    {"A25_Jaw_Open", 25f},
    {"A29_Mouth_Funnel", 15f},
    {"A35_Mouth_Shrug_Upper", 15f},
    {"A42_Mouth_Dimple_Left", 15f},
    {"A43_Mouth_Dimple_Right", 15f},
    {"A44_Mouth_Upper_Up_Left", 15f},
    {"A45_Mouth_Upper_Up_Right", 15f},
    {"A46_Mouth_Lower_Down_Left", 30f},
    {"A47_Mouth_Lower_Down_Right", 30f}
};
    private readonly Dictionary<string, float> happyBlendShapes1 = new Dictionary<string, float>
{
    {"V_Lip_Open", 50f},
    {"Eye_Squint_L", 150f},
    {"Eye_Squint_R", 150f},
    {"Mouth_Smile", 150f},
    {"Mouth_Smile_L", 40f},
    {"Mouth_Smile_R", 40f},
    {"Mouth_Open", 30f}
};
    private readonly Dictionary<string, float> angryBlendShapes1 = new Dictionary<string, float>
{
    {"Brow_Raise_Outer_Left", 100f},
    {"Brow_Raise_Outer_Right", 100f},
    {"Brow_Raise_Left", 20f},
    {"Brow_Raise_Right", 20f},
    {"Nose_Scrunch", 40f},
    {"Mouth_Open", 50f},
    {"A02_Brow_Down_Left", 30f},
    {"A03_Brow_Down_Right", 30f},
    {"A04_Brow_Outer_Up_Left", 35f},
    {"A05_Brow_Outer_Up_Right", 35f},
    {"A21_Cheek_Squint_Left", 50f},
    {"A22_Cheek_Squint_Right", 50f},
    {"A23_Nose_Sneer_Left", 50f},
    {"A24_Nose_Sneer_Right", 50f},
    {"A35_Mouth_Shrug_Upper", 10f},
    {"A36_Mouth_Shrug_Lower", 15f},
    {"A40_Mouth_Frown_Left", 15f},
    {"A41_Mouth_Frown_Right", 15f},
    {"A44_Mouth_Upper_Up_Left", 35f},
    {"A45_Mouth_Upper_Up_Right", 35f},
    {"A46_Mouth_Lower_Down_Left", 25f},
    {"A47_Mouth_Lower_Down_Right", 25f},
    {"A50_Mouth_Stretch_Left", 20f},
    {"A51_Mouth_Stretch_Right", 20f}
};
    private readonly Dictionary<string, float> sadBlendShapes1 = new Dictionary<string, float>
{
    {"Mouth_Open", 70f},
    {"A01_Brow_Inner_Up", 100f},
    {"A16_Eye_Squint_Left", 80f},
    {"A17_Eye_Squint_Right", 80f},
    {"A21_Cheek_Squint_Left", 100f},
    {"A22_Cheek_Squint_Right", 100f},
    {"A36_Mouth_Shrug_Lower", 100f},
    {"A40_Mouth_Frown_Left", 60f},
    {"A41_Mouth_Frown_Right", 60f}
};
    private readonly Dictionary<string, float> disgustBlendShapes2 = new Dictionary<string, float>
{
    {"Mouth_Snarl_Upper_R", 60f},
    {"Mouth_Snarl_Lower_L", 40f},
    {"Mouth_Snarl_Lower_R", 40f},
    {"A04_Brow_Outer_Up_Left", 100f},
    {"A05_Brow_Outer_Up_Right", 100f},
    {"A16_Eye_Squint_Left", 100f},
    {"A17_Eye_Squint_Right", 100f},
    {"A20_Cheek_Puff", 10f},
    {"A21_Cheek_Squint_Left", 100f},
    {"A22_Cheek_Squint_Right", 100f},
    {"A23_Nose_Sneer_Left", 100f},
    {"A24_Nose_Sneer_Right", 100f},
    {"A35_Mouth_Shrug_Upper", 10f},
    {"A36_Mouth_Shrug_Lower", 100f},
    {"A40_Mouth_Frown_Left", 40f},
    {"A41_Mouth_Frown_Right", 40f},
    {"A42_Mouth_Dimple_Left", 20f},
    {"A43_Mouth_Dimple_Right", 20f},
    {"A44_Mouth_Upper_Up_Left", 20f},
    {"A45_Mouth_Upper_Up_Right", 20f},
    {"A48_Mouth_Press_Left", 20f},
    {"A49_Mouth_Press_Right", 20f},
    {"A50_Mouth_Stretch_Left", 2f}
};
    private readonly Dictionary<string, float> scaredBlendShapes2 = new Dictionary<string, float>
{
    {"Cheek_Raise_L", 100f},
    {"Cheek_Raise_R", 100f},
    {"Mouth_Widen", 50f},
    {"Mouth_Open", 70f},
    {"A01_Brow_Inner_Up", 70f},
    {"A06_Eye_Look_Up_Left", 70f},
    {"A07_Eye_Look_Up_Right", 70f},
    {"A16_Eye_Squint_Left", 80f},
    {"A17_Eye_Squint_Right", 80f},
    {"A18_Eye_Wide_Left", 40f},
    {"A19_Eye_Wide_Right", 40f},
    {"A21_Cheek_Squint_Left", 30f},
    {"A22_Cheek_Squint_Right", 30f},
    {"A23_Nose_Sneer_Left", 10f},
    {"A24_Nose_Sneer_Right", 10f},
    {"A25_Jaw_Open", 10f},
    {"A40_Mouth_Frown_Left", 10f},
    {"A41_Mouth_Frown_Right", 10f},
    {"A44_Mouth_Upper_Up_Left", 10f},
    {"A45_Mouth_Upper_Up_Right", 10f},
    {"A46_Mouth_Lower_Down_Left", 20f},
    {"A47_Mouth_Lower_Down_Right", 20f}
};
    private readonly Dictionary<string, float> surprisedBlendShapes2 = new Dictionary<string, float>
{
    {"Brow_Drop_Left", 20f},
    {"Brow_Drop_Right", 20f},
    {"Brow_Raise_Left", 40f},
    {"Brow_Raise_Right", 40f},
    {"Nose_Flanks_Raise", 10f},
    {"Cheeks_Suck", 20f},
    {"Mouth_Widen", 10f},
    {"A01_Brow_Inner_Up", 50f},
    {"A04_Brow_Outer_Up_Left", 50f},
    {"A05_Brow_Outer_Up_Right", 50f},
    {"A18_Eye_Wide_Left", 80f},
    {"A19_Eye_Wide_Right", 80f},
    {"A21_Cheek_Squint_Left", 20f},
    {"A22_Cheek_Squint_Right", 20f},
    {"A35_Mouth_Shrug_Upper", 30f},
    {"A42_Mouth_Dimple_Left", 30f},
    {"A43_Mouth_Dimple_Right", 30f},
    {"A44_Mouth_Upper_Up_Left", 20f},
    {"A45_Mouth_Upper_Up_Right", 20f},
    {"A46_Mouth_Lower_Down_Left", 20f},
    {"A47_Mouth_Lower_Down_Right", 20f}
};
    private readonly Dictionary<string, float> happyBlendShapes2 = new Dictionary<string, float>
{
    {"V_Lip_Open", 50f},
    {"Eye_Squint_L", 180f},
    {"Eye_Squint_R", 180f},
    {"Mouth_Smile", 120f},
    {"Mouth_Widen", 100f},
    {"Mouth_Open", 20f}
};
    private readonly Dictionary<string, float> angryBlendShapes2 = new Dictionary<string, float>
{
    {"Mouth_Pucker", 15f},
    {"A02_Brow_Down_Left", 75f},
    {"A03_Brow_Down_Right", 75f},
    {"A04_Brow_Outer_Up_Left", 30f},
    {"A05_Brow_Outer_Up_Right", 30f},
    {"A18_Eye_Wide_Left", 75f},
    {"A19_Eye_Wide_Right", 75f},
    {"A21_Cheek_Squint_Left", 75f},
    {"A22_Cheek_Squint_Right", 75f},
    {"A23_Nose_Sneer_Left", 30f},
    {"A24_Nose_Sneer_Right", 30f},
    {"A25_Jaw_Open", 50f},
    {"A44_Mouth_Upper_Up_Left", 75f},
    {"A45_Mouth_Upper_Up_Right", 75f},
    {"A46_Mouth_Lower_Down_Left", 75f},
    {"A47_Mouth_Lower_Down_Right", 75f}
};
    private readonly Dictionary<string, float> sadBlendShapes2 = new Dictionary<string, float>
{
    {"Brow_Raise_Inner_Left", 100f},
    {"Brow_Raise_Inner_Right", 100f},
    {"Brow_Drop_Left", 50f},
    {"Brow_Drop_Right", 50f},
    {"Eye_Squint_L", 10f},
    {"Eye_Squint_R", 10f},
    {"A01_Brow_Inner_Up", 90f},
    {"A14_Eye_Blink_Left", 100f},
    {"A15_Eye_Blink_Right", 100f},
    {"A16_Eye_Squint_Left", 75f},
    {"A17_Eye_Squint_Right", 75f},
    {"A21_Cheek_Squint_Left", 50f},
    {"A22_Cheek_Squint_Right", 50f},
    {"A25_Jaw_Open", 20f},
    {"A34_Mouth_Roll_Lower", 20f},
    {"A36_Mouth_Shrug_Lower", 100f},
    {"A40_Mouth_Frown_Left", 50f},
    {"A41_Mouth_Frown_Right", 50f}
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
                //Debug.Log("** Blend2 start **");
                StartCoroutine(AnimateBlendShapes());
                break;
            }
            else { }
            //Debug.Log("** loading... **, BlendShapeC2");
        }*/
        StartCoroutine(AnimateBlendShapes());
    }

    private string GetEmotion()
    {
        if (pythonJsonInstance != null)
        {
            // python_json 클래스의 LatestEmotionScores 속성을 가져옵니다.
            return pythonJsonInstance.emotion;
            //smoothValues[orgValues.Count - 2];
        }
        else
        {
            // 오류를 기록하고 null을 반환합니다.
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