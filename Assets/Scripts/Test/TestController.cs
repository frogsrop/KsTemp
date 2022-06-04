using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TestController : MonoBehaviour
{
    [SerializeField] private TestConfiguration testConfiguration;
    [SerializeField] private Image mainImage;
    private const int SIZE = 6;
    [SerializeField] private GameObject[] buttons = new GameObject[SIZE];

    private int currentTest = 0;

    void OnValidate()
    {
        if (buttons.Length != SIZE)
        {
            Debug.LogWarning("Don't change the 'buttons' field's array size!");
            Array.Resize(ref buttons, SIZE);
        }
    }

    private List<TestConfiguration.SingleTestClass> tests;

    private void Start()
    {
        tests = testConfiguration.GetTests;
        InitCurrentQuestion();
    }

    private void InitCurrentQuestion()
    {
        var firstTest = tests[currentTest];
        InitQuestion(firstTest.mainImage, firstTest.optionsImages);
    }

    private void InitQuestion(Sprite mainSprite, Sprite[] buttonsImages)
    {
        mainImage.sprite = mainSprite;
        if (buttonsImages.Length != SIZE)
        {
            throw new Exception("Bad 'buttonsImages' size");
        }
        for (var i = 0; i < TestController.SIZE; i++)
        {
            var btnImg = buttons[i].GetComponent<Image>();
            btnImg.sprite = buttonsImages[i];
        }
    }

    public void OnClick(int index)
    {
        if (currentTest <= tests.Count - 1)
        {
            SaveInfo(index);
            currentTest++;
            if (currentTest < tests.Count)
            {
                InitCurrentQuestion();
                return;
            }
        }
        FinishTest();
    }
    private List<Tuple<int, DateTime>> answers = new();
    private void SaveInfo(int index)
    {
        answers.Add(new Tuple<int, DateTime>(index, DateTime.Now));
    }
    private void FinishTest()
    {
        foreach (var ans in answers)
        {
            print(ans.Item1 + " " + ans.Item2.ToString("HHmmss"));
        }
    }
}
