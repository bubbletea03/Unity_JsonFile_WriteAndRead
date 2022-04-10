using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // FileStream 클래스
using System.Text; // Encoding 클래스

public class JsonExample : MonoBehaviour
{

    void Start(){
        JTestClass jTestClass = new JTestClass(true); // 참조
        string jsonData = ObjectToJson(jTestClass); // JTestClass를 JSON으로 변환
        Debug.Log(jsonData); // 디버그 로그 출력
        CreateJsonFile(Application.dataPath, "JTestClass", jsonData);
            // p1. <path to project folder>/Assets
            // p2. 파일이름:JTestClass
            // p3. JSON화 한 데이터

        JTestClass jTestClassObject = JsonToObject<JTestClass>(jsonData);
            // var -> 변수 자료형 자동
            // 위에서 JSON으로 변환한걸 다시 Object(Class)로 변환 하는 듯.
            // 위에서 ObjectToJson 변환으로 얻은 JSON로 -> 데이터 넣은 JTestClass 객체 만듬

        jTestClassObject.Print(); // JTestClass.Print();

        JTestClass jTestClassLoaded = LoadJsonFile<JTestClass>(Application.dataPath, "JTestClass");
            // 읽을 경로: Asset 폴더
            // 파일명: "JTestClass"

        jTestClassLoaded.Print(); // 읽은 걸 출력

    }
    
    string ObjectToJson(object obj){ // 객체->Json화 함수

        return JsonUtility.ToJson(obj); // 객체(CLASS) 넣으면 그 객체 안의 변수 정보들 JSON 형식으로 만들어줌.
            // (string 형태로 리턴)
    }

    T JsonToObject<T>(string jsonData){ // Json->객체화 함수

        return JsonUtility.FromJson<T>(jsonData);
        // FromJson(string json)
        // 반환 값:
        //     An instance of the object.

        // 클래스(객체) 넣고, JSON string 넣으면 정보 기입된 그 클래스를 반환.
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData){

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create); // 파일 생성. 
            //  "createPath/" 여기 부분이 위치 결정 해줌. ("파일명.json")

        byte[] data = Encoding.UTF8.GetBytes(jsonData); // JSON 데이터 받아서 바이트화 시켜서 변수에 저장
        fileStream.Write(data, 0, data.Length); // 쓰기
            // Write()
            // byte를 걍 char라 생각하면 됨 --> 즉 data는 문자열이라고 생각하면 됨
            // 문자열 입력을 하는데, 글자 갯수만큼 입력한다고 생각하면 됨

        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open); // 오픈 모드로.
        byte[] data = new byte[fileStream.Length]; // 파일 안 문자들 길이만큼 배열 크기 선언
        fileStream.Read(data, 0, data.Length); 
            // 데이터 읽어서 -> data 변수에다가 넣어줌
            // 데이터 길이만큼(위에서 미리 선언한 배열 크기 만큼.)
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data); // byte -> string화
        return JsonUtility.FromJson<T>(jsonData); // JSON -> "객체로 반환."
    }



}
