using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class CustomEditField : Attribute
{
    public bool Hide;
    public string Sections;
    public string Parent;
    public string Label;
    public string Range;
    public bool ListTable;
    public bool AllowSceneObject = true;
    public EditType T;
}
