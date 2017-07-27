using System;
using UnityEditor;
using UnityEngine;

public abstract class ImporterSetting
{
    string m_settingPath;

    public ImporterSetting(string settingPath)
    {
        m_settingPath = settingPath;
    }

    public void Execute(AssetImporter importer)
    {
        OnExecute(m_settingPath, importer);
    }

    protected abstract void OnExecute(string settingPath, AssetImporter importer);
}

public class TextureImporterSetting : ImporterSetting
{
    public TextureImporterSetting(string path)
        : base(path)
    { }

    protected override void OnExecute(string settingPath, AssetImporter importer)
    {
        var templateImporter = AssetImporter.GetAtPath(settingPath) as TextureImporter;
        if (templateImporter == null)
        {
            Debug.LogError("Import Fail!! Can't find template file: " + settingPath);

            var metaFile = AssetDatabase.GetTextMetaFilePathFromAssetPath(importer.assetPath);
            FileUtil.DeleteFileOrDirectory(importer.assetPath);
            FileUtil.DeleteFileOrDirectory(metaFile);
            return;
        }

        var textureImporter = importer as TextureImporter;

        TextureImporterSettings ts = new TextureImporterSettings();
        templateImporter.ReadTextureSettings(ts);

        textureImporter.SetTextureSettings(ts);
        textureImporter.SetPlatformTextureSettings(templateImporter.GetDefaultPlatformTextureSettings());
    }
}
