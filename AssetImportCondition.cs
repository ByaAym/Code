using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public interface ICondition
{
    bool IsSatisfy(AssetImporter importer);
}

public class FilterNameCondition : ICondition
{
    string m_regx;

    public FilterNameCondition(string regx)
    {
        m_regx = regx;
    }

    public bool IsSatisfy(AssetImporter importer)
    {
        return Regex.IsMatch(Path.GetFileNameWithoutExtension(importer.assetPath), m_regx);
    }
}

public class AndCondition : ICondition
{
    List<ICondition> m_conditions;

    public AndCondition(List<ICondition> conditions)
    {
        m_conditions = conditions;
    }

    public bool IsSatisfy(AssetImporter importer)
    {
        return m_conditions.FindIndex(item => !item.IsSatisfy(importer)) < 0;
    }
}

public class OrCondition : ICondition
{
    ICondition[] m_conditions;

    public OrCondition(params ICondition[] conditions)
    {
        m_conditions = conditions;
    }

    public bool IsSatisfy(AssetImporter importer)
    {
        foreach(var condition in m_conditions)
        {
            if (condition.IsSatisfy(importer))
                return true;
        }
        return false;
    }
}
