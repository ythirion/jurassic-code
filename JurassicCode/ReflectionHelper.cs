namespace JurassicCode;

using System;
using System.Reflection;

public static class ReflectionHelper
{
    public static object GetPrivateField(object obj, string fieldName)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        if (string.IsNullOrEmpty(fieldName))
        {
            throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
        }

        var type = obj.GetType();
        var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        if (fieldInfo == null)
        {
            throw new ArgumentException($"Field '{fieldName}' not found on type '{type.Name}'.");
        }
        return fieldInfo.GetValue(obj);
    }
}