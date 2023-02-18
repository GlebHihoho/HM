using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

[System.Serializable]
public class MenuButton
{
    public string Title;
    public UnityEvent Callback;
}

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private VisualTreeAsset buttonTemplate;
    [SerializeField] private List<MenuButton> menuItems;

    private void Start()
    {
        var container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("menuContainer");

        foreach (MenuButton menuItem in menuItems)
        {
            VisualElement newElement = buttonTemplate.CloneTree();
            Button button = newElement.Q<Button>("menuButton");

            button.text = menuItem.Title;
            button.clicked += delegate { OnClick(menuItem); };

            container.Add(newElement);
        }
    }

    private void OnClick(MenuButton menuItem)
    {
        menuItem.Callback.Invoke();
    }

}
