using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowOnlyAttribute : PropertyAttribute
{

}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string valueStr = GetTitle(label, property);

        //switch (property.propertyType)
        //{
        //    case SerializedPropertyType.Integer:
        //        valueStr = property.intValue.ToString();
        //        break;
        //    case SerializedPropertyType.Boolean:
        //        valueStr = property.boolValue.ToString();
        //        break;
        //    case SerializedPropertyType.Float:
        //        valueStr = property.floatValue.ToString("0.00000");
        //        break;
        //    case SerializedPropertyType.String:
        //        valueStr = property.stringValue;
        //        break;
        //    default:
        //        valueStr = "(not supported)";
        //        break;
        //}

        EditorGUI.LabelField(position, label.text, valueStr);
    }

    string GetTitle(GUIContent label, SerializedProperty property)
    {
        if (property == null)
        {
            return label.text;
        }
        switch (property.propertyType)
        {
            case SerializedPropertyType.Generic:
                break;
            case SerializedPropertyType.Integer:
                return property.intValue.ToString();
            case SerializedPropertyType.Boolean:
                return property.boolValue.ToString();
            case SerializedPropertyType.Float:
                return property.floatValue.ToString();
            case SerializedPropertyType.String:
                return property.stringValue;
            case SerializedPropertyType.Color:
                return property.colorValue.ToString();
            case SerializedPropertyType.ObjectReference:
                return property.objectReferenceValue == null ? label.text : property.objectReferenceValue.ToString();
            case SerializedPropertyType.LayerMask:
                break;
            case SerializedPropertyType.Enum:
                return property.enumNames[property.enumValueIndex];
            case SerializedPropertyType.Vector2:
                return property.vector2Value.ToString();
            case SerializedPropertyType.Vector3:
                return property.vector3Value.ToString();
            case SerializedPropertyType.Vector4:
                return property.vector4Value.ToString();
            case SerializedPropertyType.Rect:
                break;
            case SerializedPropertyType.ArraySize:
                break;
            case SerializedPropertyType.Character:
                break;
            case SerializedPropertyType.AnimationCurve:
                break;
            case SerializedPropertyType.Bounds:
                break;
            case SerializedPropertyType.Gradient:
                break;
            case SerializedPropertyType.Quaternion:
                return property.quaternionValue.ToString();
            case SerializedPropertyType.ExposedReference:
                break;
            case SerializedPropertyType.FixedBufferSize:
                break;
            case SerializedPropertyType.Vector2Int:
                return property.vector2IntValue.ToString();
            case SerializedPropertyType.Vector3Int:
                return property.vector3IntValue.ToString();
            case SerializedPropertyType.RectInt:
                break;
            case SerializedPropertyType.BoundsInt:
                break;
            default:
                break;
        }
        return label.text;
    }
}

#endif
