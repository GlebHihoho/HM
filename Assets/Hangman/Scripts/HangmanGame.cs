using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;
using UnityEngine.Events;

namespace Hangman.Scripts
{
    public class HangmanGame : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset buttonTemplate;
        [SerializeField] private int hp = 7;

        private char[] alphabet =
        {
            'a',
            'b',
            'c',
            'd',
            'e',
            'f',
            'g',
            'h',
            'i',
            'g',
            'k',
            'l',
            'm',
            'n',
            'o',
            'p',
            'q',
            'r',
            's',
            't',
            'u',
            'v',
            'w',
            'x',
            'y',
            'z',
        };
        private int currentHp = 0;

        private List<char> guessedLetters = new List<char>();
        private List<char> wrongTriedLetter = new List<char>();

        private string[] words =
        {
            "cat",
            "dog",
            "rain",
            "unity",
            "time"
        };

        private string wordToGuess = "";

        private void Start()
        {
            currentHp = hp;
            UpdateHpView(currentHp, hp);

            var randomIndex = Random.Range(0, words.Length);

            wordToGuess = words[randomIndex];

            UpdateGuessFieldView(new String('_', wordToGuess.Length));

            var keyBoardContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("keyBoard");

            foreach (char charItem in alphabet)
            {
                VisualElement newElement = buttonTemplate.CloneTree();
                Button button = newElement.Q<Button>("menuButton");

                button.text = charItem.ToString().ToUpper();
                button.clicked += delegate { OnKeyClick(charItem, button); };

                keyBoardContainer.Add(newElement);
            }
        }

        private void OnKeyClick(char item, Button button)
        {
            button.SetEnabled(false);

            bool wordContainsPressedKey = wordToGuess.Contains(item);
            bool letterWasGuessed = guessedLetters.Contains(item);

            if (!wordContainsPressedKey && !wrongTriedLetter.Contains(item))
            {
                wrongTriedLetter.Add(item);

                UpdateHpView(currentHp -= 1, hp);
                CheckHpStatus();
            }

            if (wordContainsPressedKey && !letterWasGuessed)
            {
                guessedLetters.Add(item);
            }

            string stringToPrint = "";

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                char letterInWord = wordToGuess[i];

                if (guessedLetters.Contains(letterInWord))
                {
                    stringToPrint += letterInWord;
                }
                else
                {
                    stringToPrint += "_";
                }
            }

            if (wordToGuess == stringToPrint)
            {
                ShowResetButton();
                ShowResult("WIN");
            }

            UpdateGuessFieldView(stringToPrint);
        }

        private void UpdateHpView(int current, int maxHp)
        {
            TextElement hpValue = GetComponent<UIDocument>().rootVisualElement.Q<TextElement>("hpValue");

            hpValue.text = current.ToString() + "/" + maxHp.ToString();
        }

        private void UpdateGuessFieldView(string value)
        {
            TextElement guessField = GetComponent<UIDocument>().rootVisualElement.Q<TextElement>("guessField");

            guessField.text = value.ToUpper();
        }

        private void CheckHpStatus()
        {
            if (currentHp <= 0)
            {
                List<VisualElement> buttons = GetComponent<UIDocument>().rootVisualElement.Query("menuButton").ToList();

                foreach (Button button in buttons) button.SetEnabled(false);

                ShowResetButton();
                ShowResult("LOSS");
            }
        }

        private void ShowResetButton()
        {
            VisualElement hpBlock = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("hpBlock");
            VisualElement newElement = buttonTemplate.CloneTree();

            Button button = newElement.Q<Button>("menuButton");
            button.text = "Reset";
            button.style.width = 100;
            button.clicked += delegate
            {
                ResetLevel();
                hpBlock.Remove(newElement);
            };

            hpBlock.Add(newElement);
        }

        private void ResetLevel()
        {
            currentHp = hp;
            guessedLetters = new List<char>();
            wrongTriedLetter = new List<char>();

            UpdateHpView(currentHp, hp);

            var randomIndex = Random.Range(0, words.Length);
            wordToGuess = words[randomIndex];

            UpdateGuessFieldView(new String('_', wordToGuess.Length));

            List<VisualElement> buttons = GetComponent<UIDocument>().rootVisualElement.Query("menuButton").ToList();

            foreach (Button button in buttons) button.SetEnabled(true);

            ShowResult("");
        }

        private void ShowResult(string resultText)
        {
            TextElement resultField = GetComponent<UIDocument>().rootVisualElement.Q<TextElement>("result");

            resultField.text = resultText;
        }
    }
}
