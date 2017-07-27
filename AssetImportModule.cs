using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Processor
{
    ICondition m_condition;
    ImporterSetting m_setting;

    public Processor(ICondition condition, ImporterSetting setting)
    {
        m_condition = condition;
        m_setting = setting;
    }

    internal bool Execute(AssetImporter importer)
    {
        if (!m_condition.IsSatisfy(importer))
            return false;

        m_setting.Execute(importer);
        return true;
    }
}

public class AssetPreprocessModule
{
    List<Processor> m_list;

    public AssetPreprocessModule(List<Processor> list)
    {
        m_list = list;
    }

    internal void Execute(AssetImporter importer)
    {
        foreach (var p in m_list)
        {
            if (p.Execute(importer))
                return;
        }

        Debug.Log("未满足任何条件");

        AssetDatabase.DeleteAsset(importer.assetPath);
        //var metaFile = AssetDatabase.GetTextMetaFilePathFromAssetPath(importer.assetPath);
        //FileUtil.DeleteFileOrDirectory(importer.assetPath);
        //FileUtil.DeleteFileOrDirectory(metaFile);
    }
}