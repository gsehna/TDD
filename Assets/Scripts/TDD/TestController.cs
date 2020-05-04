using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private List<Test> tests = new List<Test>();

    [HideInInspector]
    public Pathfinder pathfinder;

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();

        string[] fileNames = System.IO.Directory.GetFiles(Application.dataPath + "/Scripts/TDD/Tests");
        List<string> testNames = new List<string>();

        for (int i = 0; i < fileNames.Length; i += 2)
        {
            string testName = fileNames[i].Replace(".cs", "");
            testName = testName.Replace(Application.dataPath + "/Scripts/TDD/Tests\\", "");
            testNames.Add(testName);
        }

        foreach (string s in testNames)
        {
            CreateTest(s);
        }

        foreach (Test t in tests)
        {
            t.testController = this;
            bool testResult = t.DoTests();

            Debug.Log(t.GetType().ToString() + " RESULT: " + testResult);
        }
    }

    private void CreateTest(string name)
    {
        System.Type testType = System.Type.GetType(name);
        object newTest = System.Activator.CreateInstance(testType);
        tests.Add((Test)newTest);
    }
}
