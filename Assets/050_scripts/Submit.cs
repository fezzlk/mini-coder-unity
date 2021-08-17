using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Submit : MonoBehaviour
{
    //pythonがある場所
    private string pyExePath = @"";
    //実行したいスクリプトがある場所
    private string pyCodePath = @"[プロジェクトルート]/Assets/050_scripts/pythonScript/run.py";


    //オブジェクトと結びつける
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        // Componentを扱えるようにする
        inputField = inputField.GetComponent<InputField>();
    }

    public void submitAnswer()
    {
        // テキストにinputFieldの内容を反映
        // Debug.Log(inputField.text);
        runPythonScript();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void runPythonScript()
    {
        //外部プロセスの設定
        ProcessStartInfo processStartInfo = new ProcessStartInfo() {
            FileName = pyExePath, //実行するファイル(python)
            UseShellExecute = false,//シェルを使うかどうか
            CreateNoWindow = true, //ウィンドウを開くかどうか
            RedirectStandardOutput = true, //テキスト出力をStandardOutputストリームに書き込むかどうか
            Arguments = pyCodePath + " '" + inputField.text + "'", //実行するスクリプト
        };

        //外部プロセスの開始
        Process process = Process.Start(processStartInfo);

        //ストリームから出力を得る
        StreamReader streamReader = process.StandardOutput;
        string str = streamReader.ReadLine();

        //外部プロセスの終了
        process.WaitForExit();
        process.Close();

        //実行
        print(str);
    }
}
