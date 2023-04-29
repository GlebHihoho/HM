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
        [SerializeField] private List<Texture2D> images;
        private readonly char[] _alphabet =
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
        private readonly int _hp = 6;
        private int _currentHp = 0;
        private string _hintText;
        private string _wordToGuess = "";
        private List<char> _guessedLetters = new List<char>();
        private List<char> _wrongTriedLetter = new List<char>();
        private readonly Dictionary<string, string> _word = new Dictionary<string, string>()
        {
            { "cat", "кот" },
            { "dog", "собака" },
            { "rain", "дождь" },
            { "unity", "ты пишешь в нём" },
            { "time", "время" },
            { "word", "слово" },
            { "game", "игра" },
            { "key", "ключ" },
        };
        private string[] _keys;

        private VisualElement _imageContainer;

        private async void Start()
        {
            _currentHp = _hp;
            UpdateHpView(_currentHp, _hp);

            _keys = _word.Keys.ToArray();
            var randomKey = Random.Range(0, _keys.Length);

            _wordToGuess = _keys[randomKey];
            _hintText = _word[_wordToGuess];

            UpdateTextField(new String('_', _wordToGuess.Length), "guessField");
            UpdateTextField(_hintText, "hintField");

            _imageContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("imgBlock");
            
            ChangeBackgroundImage();

            var keyBoardContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("keyBoard");

            foreach (char charItem in _alphabet)
            {
                VisualElement newElement = buttonTemplate.CloneTree();
                Button button = newElement.Q<Button>("menuButton");

                button.text = charItem.ToString().ToUpper();
                button.clicked += () => { OnKeyClick(charItem, button); };

                keyBoardContainer.Add(newElement);
            }

            Button exitButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("exitButton");
            exitButton.clicked += () => { Exit(); };
        }

        private void OnKeyClick(char item, Button button)
        {
            button.SetEnabled(false);

            bool wordContainsPressedKey = _wordToGuess.Contains(item);
            bool letterWasGuessed = _guessedLetters.Contains(item);

            if (!wordContainsPressedKey && !_wrongTriedLetter.Contains(item))
            {
                _wrongTriedLetter.Add(item);

                UpdateHpView(_currentHp -= 1, _hp);
                CheckHpStatus();
                ChangeBackgroundImage();
            }

            if (wordContainsPressedKey && !letterWasGuessed)
            {
                _guessedLetters.Add(item);
            }

            string stringToPrint = "";

            for (int i = 0; i < _wordToGuess.Length; i++)
            {
                char letterInWord = _wordToGuess[i];

                if (_guessedLetters.Contains(letterInWord))
                {
                    stringToPrint += letterInWord;
                }
                else
                {
                    stringToPrint += "_";
                }
            }

            if (_wordToGuess == stringToPrint)
            {
                ShowResetButton();
                UpdateTextField("WIN", "result");
            }

            UpdateTextField(stringToPrint, "guessField");
        }

        private void UpdateHpView(int current, int maxHp)
        {
            TextElement hpValue = GetComponent<UIDocument>().rootVisualElement.Q<TextElement>("hpValue");

            hpValue.text = current.ToString() + "/" + maxHp.ToString();
        }

        private void UpdateTextField(string value, string key)
        {
            TextElement guessField = GetComponent<UIDocument>().rootVisualElement.Q<TextElement>(key);

            guessField.text = value.ToUpper();
        }


        private void CheckHpStatus()
        {
            if (_currentHp <= 0)
            {
                List<VisualElement> buttons = GetComponent<UIDocument>().rootVisualElement.Query("menuButton").ToList();

                foreach (Button button in buttons) button.SetEnabled(false);

                ShowResetButton();
                UpdateTextField("LOSS", "result");
            }
        }

        private void ShowResetButton()
        {
            VisualElement hpBlock = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("hpBlock");
            VisualElement newElement = buttonTemplate.CloneTree();

            Button button = newElement.Q<Button>("menuButton");
            button.text = "Reset";
            button.style.width = 100;
            button.clicked += () =>
            {
                ResetLevel();
                hpBlock.Remove(newElement);
            };

            hpBlock.Add(newElement);
        }

        private void ChangeBackgroundImage()
        {
            _imageContainer.style.backgroundImage = images[_hp - _currentHp];
        }

        private void ResetLevel()
        {
            _currentHp = _hp;
            _guessedLetters = new List<char>();
            _wrongTriedLetter = new List<char>();

            _imageContainer.style.backgroundImage = images[0];

            UpdateHpView(_currentHp, _hp);

            _keys = _word.Keys.ToArray();
            var randomKey = Random.Range(0, _keys.Length);

            _wordToGuess = _keys[randomKey];
            _hintText = _word[_wordToGuess];

            UpdateTextField(new String('_', _wordToGuess.Length), "guessField");
            UpdateTextField(_hintText, "hintField");

            List<VisualElement> buttons = GetComponent<UIDocument>().rootVisualElement.Query("menuButton").ToList();

            foreach (Button button in buttons) button.SetEnabled(true);

            UpdateTextField("", "result");
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
