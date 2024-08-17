using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public abstract class BaseButtonEditor : Editor
{
    protected void DrawButtons(Object target)
    {
        var methods = target.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);

        foreach (var method in methods)
        {
            var buttonAttribute = (ButtonAttribute)method.GetCustomAttribute(typeof(ButtonAttribute), true);
            var buttonName = string.IsNullOrEmpty(buttonAttribute.name) ? method.Name : buttonAttribute.name;

            if (GUILayout.Button(buttonName))
            {
                method.Invoke(target, null);
            }
        }
    }
}
