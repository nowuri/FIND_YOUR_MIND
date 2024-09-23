using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    //LineRenderer lr;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private Vector3 cube1Pos, cube2Pos;

    private void Start()
    {
        // LineRenderer 설정
        for (int i = 0; i < 300; i++) {
            LineRenderer lr = CreateLineRenderer();
            lineRenderers.Add(lr);
        }

    }

    void Update()
    {
        // 얼굴 라인 0~16
        lineRenderers[0].SetPosition(0, GameObject.Find("Sphere_").GetComponent<Transform>().position);
        lineRenderers[0].SetPosition(1, GameObject.Find("Sphere_ (1)").GetComponent<Transform>().position);
        lineRenderers[1].SetPosition(0, GameObject.Find("Sphere_ (1)").GetComponent<Transform>().position);
        lineRenderers[1].SetPosition(1, GameObject.Find("Sphere_ (2)").GetComponent<Transform>().position);
        lineRenderers[2].SetPosition(0, GameObject.Find("Sphere_ (2)").GetComponent<Transform>().position);
        lineRenderers[2].SetPosition(1, GameObject.Find("Sphere_ (3)").GetComponent<Transform>().position);
        lineRenderers[3].SetPosition(0, GameObject.Find("Sphere_ (3)").GetComponent<Transform>().position);
        lineRenderers[3].SetPosition(1, GameObject.Find("Sphere_ (4)").GetComponent<Transform>().position);
        lineRenderers[4].SetPosition(0, GameObject.Find("Sphere_ (4)").GetComponent<Transform>().position);
        lineRenderers[4].SetPosition(1, GameObject.Find("Sphere_ (5)").GetComponent<Transform>().position);
        lineRenderers[5].SetPosition(0, GameObject.Find("Sphere_ (5)").GetComponent<Transform>().position);
        lineRenderers[5].SetPosition(1, GameObject.Find("Sphere_ (6)").GetComponent<Transform>().position);
        lineRenderers[6].SetPosition(0, GameObject.Find("Sphere_ (6)").GetComponent<Transform>().position);
        lineRenderers[6].SetPosition(1, GameObject.Find("Sphere_ (7)").GetComponent<Transform>().position);
        lineRenderers[7].SetPosition(0, GameObject.Find("Sphere_ (7)").GetComponent<Transform>().position);
        lineRenderers[7].SetPosition(1, GameObject.Find("Sphere_ (8)").GetComponent<Transform>().position);
        lineRenderers[8].SetPosition(0, GameObject.Find("Sphere_ (8)").GetComponent<Transform>().position);
        lineRenderers[8].SetPosition(1, GameObject.Find("Sphere_ (9)").GetComponent<Transform>().position);
        lineRenderers[9].SetPosition(0, GameObject.Find("Sphere_ (9)").GetComponent<Transform>().position);
        lineRenderers[9].SetPosition(1, GameObject.Find("Sphere_ (10)").GetComponent<Transform>().position);
        lineRenderers[10].SetPosition(0, GameObject.Find("Sphere_ (10)").GetComponent<Transform>().position);
        lineRenderers[10].SetPosition(1, GameObject.Find("Sphere_ (11)").GetComponent<Transform>().position);
        lineRenderers[11].SetPosition(0, GameObject.Find("Sphere_ (11)").GetComponent<Transform>().position);
        lineRenderers[11].SetPosition(1, GameObject.Find("Sphere_ (12)").GetComponent<Transform>().position);
        lineRenderers[12].SetPosition(0, GameObject.Find("Sphere_ (12)").GetComponent<Transform>().position);
        lineRenderers[12].SetPosition(1, GameObject.Find("Sphere_ (13)").GetComponent<Transform>().position);
        lineRenderers[13].SetPosition(0, GameObject.Find("Sphere_ (13)").GetComponent<Transform>().position);
        lineRenderers[13].SetPosition(1, GameObject.Find("Sphere_ (14)").GetComponent<Transform>().position);
        lineRenderers[14].SetPosition(0, GameObject.Find("Sphere_ (14)").GetComponent<Transform>().position);
        lineRenderers[14].SetPosition(1, GameObject.Find("Sphere_ (15)").GetComponent<Transform>().position);
        lineRenderers[15].SetPosition(0, GameObject.Find("Sphere_ (15)").GetComponent<Transform>().position);
        lineRenderers[15].SetPosition(1, GameObject.Find("Sphere_ (16)").GetComponent<Transform>().position);

        // 눈 광대
        CreateTriangle(16, "Sphere_", "Sphere_ (36)", "Sphere_ (17)");

        // 나머지 얼굴 메쉬 연결
        // 코 라인
        ConnectPoints(19, 27, "Sphere_ (27)");

        // 왼쪽 눈
        ConnectLoop(28, 36, 41);

        // 오른쪽 눈
        ConnectLoop(34, 42, 47);

        // 왼쪽 눈썹
        ConnectPoints(40, 21, "Sphere_ (22)");

        // 오른쪽 눈썹
        ConnectPoints(44, 26, "Sphere_ (27)");

        // 입술 외곽
        ConnectLoop(48, 48, 59);

        // 입술 내곽
        ConnectLoop(60, 60, 67);

        // 코 내부 연결
        AddNoseConnections();

        // 눈입 연결, 눈코 연결
        AddFaceConnections();

        // 얼굴 주변과 입 연결
        AddFaceAndLipConnections();

    }

    private LineRenderer CreateLineRenderer()
    {
        GameObject lineObj = new GameObject("LineRenderer");
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.startWidth = 0.4f;
        lr.endWidth = 0.4f;
        lr.positionCount = 2;
        lr.material.color = Color.blue;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        return lr;
    }

    void CreateTriangle(int startIndex, string point1, string point2, string point3)
    {
        lineRenderers[startIndex].SetPosition(0, GameObject.Find(point1).GetComponent<Transform>().position);
        lineRenderers[startIndex].SetPosition(1, GameObject.Find(point2).GetComponent<Transform>().position);

        lineRenderers[startIndex + 1].SetPosition(0, GameObject.Find(point2).GetComponent<Transform>().position);
        lineRenderers[startIndex + 1].SetPosition(1, GameObject.Find(point3).GetComponent<Transform>().position);

        lineRenderers[startIndex + 2].SetPosition(0, GameObject.Find(point3).GetComponent<Transform>().position);
        lineRenderers[startIndex + 2].SetPosition(1, GameObject.Find(point1).GetComponent<Transform>().position);
    }

    void ConnectPoints(int startIndex, int endIndex, string startPoint)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            lineRenderers[i].SetPosition(0, GameObject.Find($"Sphere_ ({i})").GetComponent<Transform>().position);
            lineRenderers[i].SetPosition(1, GameObject.Find($"Sphere_ ({i + 1})").GetComponent<Transform>().position);
        }
        lineRenderers[endIndex].SetPosition(0, GameObject.Find($"Sphere_ ({endIndex})").GetComponent<Transform>().position);
        lineRenderers[endIndex].SetPosition(1, GameObject.Find(startPoint).GetComponent<Transform>().position);
    }

    void ConnectLoop(int startIndex, int firstPoint, int lastPoint)
    {
        for (int i = firstPoint; i < lastPoint; i++)
        {
            lineRenderers[startIndex + i - firstPoint].SetPosition(0, GameObject.Find($"Sphere_ ({i})").GetComponent<Transform>().position);
            lineRenderers[startIndex + i - firstPoint].SetPosition(1, GameObject.Find($"Sphere_ ({i + 1})").GetComponent<Transform>().position);
        }
        lineRenderers[startIndex + lastPoint - firstPoint].SetPosition(0, GameObject.Find($"Sphere_ ({lastPoint})").GetComponent<Transform>().position);
        lineRenderers[startIndex + lastPoint - firstPoint].SetPosition(1, GameObject.Find($"Sphere_ ({firstPoint})").GetComponent<Transform>().position);
    }

    void AddNoseConnections()
    {
        int index = 68;
        lineRenderers[index].SetPosition(0, GameObject.Find("Sphere_ (27)").GetComponent<Transform>().position);
        lineRenderers[index].SetPosition(1, GameObject.Find("Sphere_ (28)").GetComponent<Transform>().position);

        lineRenderers[index + 1].SetPosition(0, GameObject.Find("Sphere_ (28)").GetComponent<Transform>().position);
        lineRenderers[index + 1].SetPosition(1, GameObject.Find("Sphere_ (29)").GetComponent<Transform>().position);
    }

    void AddFaceConnections()
    {
        int index = 70;
        // 왼쪽 눈-코
        lineRenderers[index].SetPosition(0, GameObject.Find("Sphere_ (39)").GetComponent<Transform>().position);
        lineRenderers[index].SetPosition(1, GameObject.Find("Sphere_ (27)").GetComponent<Transform>().position);

        // 오른쪽 눈-코
        lineRenderers[index + 1].SetPosition(0, GameObject.Find("Sphere_ (42)").GetComponent<Transform>().position);
        lineRenderers[index + 1].SetPosition(1, GameObject.Find("Sphere_ (27)").GetComponent<Transform>().position);

        // 왼쪽 눈-코
        lineRenderers[index + 2].SetPosition(0, GameObject.Find("Sphere_ (39)").GetComponent<Transform>().position);
        lineRenderers[index + 2].SetPosition(1, GameObject.Find("Sphere_ (28)").GetComponent<Transform>().position);

        lineRenderers[index + 3].SetPosition(0, GameObject.Find("Sphere_ (39)").GetComponent<Transform>().position);
        lineRenderers[index + 3].SetPosition(1, GameObject.Find("Sphere_ (29)").GetComponent<Transform>().position);

        // 오른쪽 눈-코
        lineRenderers[index + 4].SetPosition(0, GameObject.Find("Sphere_ (42)").GetComponent<Transform>().position);
        lineRenderers[index + 4].SetPosition(1, GameObject.Find("Sphere_ (28)").GetComponent<Transform>().position);

        lineRenderers[index + 5].SetPosition(0, GameObject.Find("Sphere_ (42)").GetComponent<Transform>().position);
        lineRenderers[index + 5].SetPosition(1, GameObject.Find("Sphere_ (29)").GetComponent<Transform>().position);

        // 코-입
        lineRenderers[index + 6].SetPosition(0, GameObject.Find("Sphere_ (33)").GetComponent<Transform>().position);
        lineRenderers[index + 6].SetPosition(1, GameObject.Find("Sphere_ (51)").GetComponent<Transform>().position);
    }
    void AddFaceAndLipConnections()
    {
        int index = 78;

        // 왼쪽 얼굴 외곽에서 입으로 연결
        lineRenderers[index].SetPosition(0, GameObject.Find("Sphere_ (3)").GetComponent<Transform>().position);
        lineRenderers[index].SetPosition(1, GameObject.Find("Sphere_ (48)").GetComponent<Transform>().position);

        lineRenderers[index + 1].SetPosition(0, GameObject.Find("Sphere_ (4)").GetComponent<Transform>().position);
        lineRenderers[index + 1].SetPosition(1, GameObject.Find("Sphere_ (48)").GetComponent<Transform>().position);

        lineRenderers[index + 2].SetPosition(0, GameObject.Find("Sphere_ (5)").GetComponent<Transform>().position);
        lineRenderers[index + 2].SetPosition(1, GameObject.Find("Sphere_ (48)").GetComponent<Transform>().position);

        // 오른쪽 얼굴 외곽에서 입으로 연결
        lineRenderers[index + 3].SetPosition(0, GameObject.Find("Sphere_ (13)").GetComponent<Transform>().position);
        lineRenderers[index + 3].SetPosition(1, GameObject.Find("Sphere_ (54)").GetComponent<Transform>().position);

        lineRenderers[index + 4].SetPosition(0, GameObject.Find("Sphere_ (12)").GetComponent<Transform>().position);
        lineRenderers[index + 4].SetPosition(1, GameObject.Find("Sphere_ (54)").GetComponent<Transform>().position);

        lineRenderers[index + 5].SetPosition(0, GameObject.Find("Sphere_ (11)").GetComponent<Transform>().position);
        lineRenderers[index + 5].SetPosition(1, GameObject.Find("Sphere_ (54)").GetComponent<Transform>().position);

        //턱에서 입으로 연결
        lineRenderers[index + 6].SetPosition(0, GameObject.Find("Sphere_ (5)").GetComponent<Transform>().position);
        lineRenderers[index + 6].SetPosition(1, GameObject.Find("Sphere_ (59)").GetComponent<Transform>().position);

        lineRenderers[index + 7].SetPosition(0, GameObject.Find("Sphere_ (6)").GetComponent<Transform>().position);
        lineRenderers[index + 7].SetPosition(1, GameObject.Find("Sphere_ (59)").GetComponent<Transform>().position);

        lineRenderers[index + 8].SetPosition(0, GameObject.Find("Sphere_ (6)").GetComponent<Transform>().position);
        lineRenderers[index + 8].SetPosition(1, GameObject.Find("Sphere_ (58)").GetComponent<Transform>().position);

        lineRenderers[index + 9].SetPosition(0, GameObject.Find("Sphere_ (7)").GetComponent<Transform>().position);
        lineRenderers[index + 9].SetPosition(1, GameObject.Find("Sphere_ (58)").GetComponent<Transform>().position);

        lineRenderers[index + 10].SetPosition(0, GameObject.Find("Sphere_ (7)").GetComponent<Transform>().position);
        lineRenderers[index + 10].SetPosition(1, GameObject.Find("Sphere_ (57)").GetComponent<Transform>().position);

        lineRenderers[index + 11].SetPosition(0, GameObject.Find("Sphere_ (8)").GetComponent<Transform>().position);
        lineRenderers[index + 11].SetPosition(1, GameObject.Find("Sphere_ (57)").GetComponent<Transform>().position);

        lineRenderers[index + 12].SetPosition(0, GameObject.Find("Sphere_ (8)").GetComponent<Transform>().position);
        lineRenderers[index + 12].SetPosition(1, GameObject.Find("Sphere_ (56)").GetComponent<Transform>().position);

        lineRenderers[index + 13].SetPosition(0, GameObject.Find("Sphere_ (9)").GetComponent<Transform>().position);
        lineRenderers[index + 13].SetPosition(1, GameObject.Find("Sphere_ (56)").GetComponent<Transform>().position);

        lineRenderers[index + 14].SetPosition(0, GameObject.Find("Sphere_ (10)").GetComponent<Transform>().position);
        lineRenderers[index + 14].SetPosition(1, GameObject.Find("Sphere_ (56)").GetComponent<Transform>().position);

        lineRenderers[index + 15].SetPosition(0, GameObject.Find("Sphere_ (10)").GetComponent<Transform>().position);
        lineRenderers[index + 15].SetPosition(1, GameObject.Find("Sphere_ (55)").GetComponent<Transform>().position);

        lineRenderers[index + 16].SetPosition(0, GameObject.Find("Sphere_ (11)").GetComponent<Transform>().position);
        lineRenderers[index + 16].SetPosition(1, GameObject.Find("Sphere_ (55)").GetComponent<Transform>().position);
    }
}
