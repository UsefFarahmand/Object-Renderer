using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string ConditionalSourceField = "";
    public string ConditionalSourceField2 = "";
    public string[] ConditionalSourceFields = new string[] { };
    public bool[] ConditionalSourceFieldInverseBools = new bool[] { };
    public bool HideInInspector = false;
    public bool Inverse = false;
    public bool UseOrLogic = false;

    public bool InverseCondition1 = false;
    public bool InverseCondition2 = false;


    // Use this for initialization
    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionalSourceField">The name of the bool field that will be in control</param>
    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = false;
        this.Inverse = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionalSourceField">The name of the bool field that will be in control</param>
    /// <param name="hideInInspector">TRUE = Hide in inspector / FALSE = Disable in inspector</param>
    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionalSourceField">The name of the bool field that will be in control</param>
    /// <param name="hideInInspector">TRUE = Hide in inspector / FALSE = Disable in inspector</param>
    /// <param name="inverse"></param>
    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hideInInspector">TRUE = Hide in inspector / FALSE = Disable in inspector</param>
    public ConditionalHideAttribute(bool hideInInspector = false)
    {
        this.ConditionalSourceField = "";
        this.HideInInspector = hideInInspector;
        this.Inverse = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionalSourceFields">The name of the bool fields that will be in control</param>
    /// <param name="conditionalSourceFieldInverseBools"></param>
    /// <param name="hideInInspector">TRUE = Hide in inspector / FALSE = Disable in inspector</param>
    /// <param name="inverse"></param>
    public ConditionalHideAttribute(string[] conditionalSourceFields, bool[] conditionalSourceFieldInverseBools, bool hideInInspector, bool inverse)
    {
        this.ConditionalSourceFields = conditionalSourceFields;
        this.ConditionalSourceFieldInverseBools = conditionalSourceFieldInverseBools;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conditionalSourceFields">The name of the bool fields that will be in control</param>
    /// <param name="hideInInspector">TRUE = Hide in inspector / FALSE = Disable in inspector</param>
    /// <param name="inverse"></param>
    public ConditionalHideAttribute(string[] conditionalSourceFields, bool hideInInspector, bool inverse)
    {
        this.ConditionalSourceFields = conditionalSourceFields;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
    }

    //public readonly string ConditionalSourceField;
    ///// <summary>
    ///// TRUE = Hide in inspector / FALSE = Disable in inspector 
    ///// </summary>
    //public bool HideInInspector = false;
    //public bool Inverse = false;

    //// Use this for initialization
    //public ConditionalHideAttribute(string conditionalSourceField)
    //{
    //    ConditionalSourceField = conditionalSourceField;
    //    HideInInspector = true;
    //    Inverse = false;
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="conditionalSourceField">The name of the bool field that will be in control</param>
    ///// <param name="hideInInspector">TRUE = Hide in inspector / FALSE = Disable in inspector </param>
    //public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    //{
    //    ConditionalSourceField = conditionalSourceField;
    //    HideInInspector = hideInInspector;
    //    Inverse = false;
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="conditionalSourceField">The name of the bool field that will be in control</param>
    ///// <param name="hideInInspector">TRUE = Hide in inspector / FALSE = Disable in inspector </param>
    ///// <param name="inverse"></param>
    //public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
    //{
    //    ConditionalSourceField = conditionalSourceField;
    //    HideInInspector = hideInInspector;
    //    Inverse = inverse;
    //}
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    //get the attribute data
    //    ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
    //    //check if the propery we want to draw should be enabled
    //    bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

    //    //Enable/disable the property
    //    bool wasEnabled = GUI.enabled;
    //    GUI.enabled = enabled;

    //    //Check if we should draw the property
    //    if (!condHAtt.HideInInspector || enabled)
    //    {
    //        EditorGUI.PropertyField(position, property, label, true);
    //    }

    //    //Ensure that the next property that is being drawn uses the correct settings
    //    GUI.enabled = wasEnabled;
    //}

    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //{
    //    ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
    //    bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

    //    if (!condHAtt.HideInInspector || enabled)
    //    {
    //        return EditorGUI.GetPropertyHeight(property, label);
    //    }
    //    else
    //    {
    //        //The property is not being drawn
    //        //We want to undo the spacing added before and after the property
    //        return -EditorGUIUtility.standardVerticalSpacing;
    //    }
    //}

    //private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    //{
    //    bool enabled = true;
    //    //Look for the sourcefield within the object that the property belongs to
    //    string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
    //    string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
    //    SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

    //    if (sourcePropertyValue != null)
    //    {
    //        enabled = sourcePropertyValue.boolValue;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
    //    }

    //    return enabled;
    //}

    ////private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    ////{
    ////    var enabled = true;

    ////    var propertyPath =
    ////        property.propertyPath; //returns the property path of the property we want to apply the attribute to
    ////    string conditionPath;

    ////    if (!string.IsNullOrEmpty(condHAtt.ConditionalSourceField))
    ////    {
    ////        //Get the full relative property path of the sourcefield so we can have nested hiding
    ////        conditionPath =
    ////            propertyPath.Replace(property.name,
    ////                condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
    ////        var sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

    ////        if (sourcePropertyValue != null)
    ////        {
    ////            enabled = CheckPropertyType(sourcePropertyValue);
    ////        }
    ////    }

    ////    if (condHAtt.Inverse) enabled = !enabled;

    ////    return enabled;
    ////}

    //private bool CheckPropertyType(SerializedProperty sourcePropertyValue)
    //{
    //    switch (sourcePropertyValue.propertyType)
    //    {
    //        case SerializedPropertyType.Boolean:
    //            return sourcePropertyValue.boolValue;
    //        case SerializedPropertyType.ObjectReference:
    //            return sourcePropertyValue.objectReferenceValue != null;
    //        default:
    //            Debug.LogError("Data type of the property used for conditional hiding [" +
    //                            sourcePropertyValue.propertyType + "] is currently not supported");
    //            return true;
    //    }
    //}

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;
        if (!condHAtt.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
            
        }

        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (!condHAtt.HideInInspector || enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            //The property is not being drawn
            //We want to undo the spacing added before and after the property
            return -EditorGUIUtility.standardVerticalSpacing;
            //return 0.0f;
        }


        /*
        //Get the base height when not expanded
        var height = base.GetPropertyHeight(property, label);

        // if the property is expanded go through all its children and get their height
        if (property.isExpanded)
        {
            var propEnum = property.GetEnumerator();
            while (propEnum.MoveNext())
                height += EditorGUI.GetPropertyHeight((SerializedProperty)propEnum.Current, GUIContent.none, true);
        }
        return height;*/
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        bool enabled = (condHAtt.UseOrLogic) ? false : true;

        //Handle primary property
        SerializedProperty sourcePropertyValue = null;
        //Get the full relative property path of the sourcefield so we can have nested hiding.Use old method when dealing with arrays
        if (!property.isArray)
        {
            string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
            sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            //if the find failed->fall back to the old system
            if (sourcePropertyValue == null)
            {
                //original implementation (doens't work with nested serializedObjects)
                sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField);
            }
        }
        else
        {
            //original implementation (doens't work with nested serializedObjects)
            sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField);
        }


        if (sourcePropertyValue != null)
        {
            enabled = CheckPropertyType(sourcePropertyValue);
            if (condHAtt.InverseCondition1) enabled = !enabled;
        }
        else
        {
            //Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
        }


        //handle secondary property
        SerializedProperty sourcePropertyValue2 = null;
        if (!property.isArray)
        {
            string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField2); //changes the path to the conditionalsource property path
            sourcePropertyValue2 = property.serializedObject.FindProperty(conditionPath);

            //if the find failed->fall back to the old system
            if (sourcePropertyValue2 == null)
            {
                //original implementation (doens't work with nested serializedObjects)
                sourcePropertyValue2 = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField2);
            }
        }
        else
        {
            // original implementation(doens't work with nested serializedObjects) 
            sourcePropertyValue2 = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField2);
        }

        //Combine the results
        if (sourcePropertyValue2 != null)
        {
            bool prop2Enabled = CheckPropertyType(sourcePropertyValue2);
            if (condHAtt.InverseCondition2) prop2Enabled = !prop2Enabled;

            if (condHAtt.UseOrLogic)
                enabled = enabled || prop2Enabled;
            else
                enabled = enabled && prop2Enabled;
        }
        else
        {
            //Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
        }

        //Handle the unlimited property array
        string[] conditionalSourceFieldArray = condHAtt.ConditionalSourceFields;
        bool[] conditionalSourceFieldInverseArray = condHAtt.ConditionalSourceFieldInverseBools;
        for (int index = 0; index < conditionalSourceFieldArray.Length; ++index)
        {
            SerializedProperty sourcePropertyValueFromArray = null;
            if (!property.isArray)
            {
                string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
                string conditionPath = propertyPath.Replace(property.name, conditionalSourceFieldArray[index]); //changes the path to the conditionalsource property path
                sourcePropertyValueFromArray = property.serializedObject.FindProperty(conditionPath);

                //if the find failed->fall back to the old system
                if (sourcePropertyValueFromArray == null)
                {
                    //original implementation (doens't work with nested serializedObjects)
                    sourcePropertyValueFromArray = property.serializedObject.FindProperty(conditionalSourceFieldArray[index]);
                }
            }
            else
            {
                // original implementation(doens't work with nested serializedObjects) 
                sourcePropertyValueFromArray = property.serializedObject.FindProperty(conditionalSourceFieldArray[index]);
            }

            //Combine the results
            if (sourcePropertyValueFromArray != null)
            {
                bool propertyEnabled = CheckPropertyType(sourcePropertyValueFromArray);
                if (conditionalSourceFieldInverseArray.Length >= (index + 1) && conditionalSourceFieldInverseArray[index]) propertyEnabled = !propertyEnabled;

                if (condHAtt.UseOrLogic)
                    enabled = enabled || propertyEnabled;
                else
                    enabled = enabled && propertyEnabled;
            }
            else
            {
                //Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
            }
        }


        //wrap it all up
        if (condHAtt.Inverse) enabled = !enabled;

        return enabled;
    }

    private bool CheckPropertyType(SerializedProperty sourcePropertyValue)
    {
        //Note: add others for custom handling if desired
        switch (sourcePropertyValue.propertyType)
        {
            case SerializedPropertyType.Boolean:
                return sourcePropertyValue.boolValue;
            case SerializedPropertyType.ObjectReference:
                return sourcePropertyValue.objectReferenceValue != null;
            default:
                Debug.LogError("Data type of the property used for conditional hiding [" + sourcePropertyValue.propertyType + "] is currently not supported");
                return true;
        }
    }
}

#endif