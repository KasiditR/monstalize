using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class ButtonAttribute : PropertyAttribute
{
    public string name;
    public object[] parameters;
    public ButtonAttribute()
    {
        parameters = null;
    }
    public ButtonAttribute(string name)
    {
        this.name = name;
        parameters = null;
    }
    public ButtonAttribute(object[] parameters)
    {
        this.parameters = parameters;
    }
    public ButtonAttribute(string name, object[] parameters)
    {
        this.name = name;
        this.parameters = parameters;
    }
}
