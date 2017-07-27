using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetImportPostProcessTool : AssetPostprocessor
{
    static readonly string AssetTemplatePath = "Assets/Resource/Template/";

    static readonly Dictionary<Type, AssetPreprocessModule> Config = new Dictionary<Type, AssetPreprocessModule>()
    {
#region TextureImporter
        {
            typeof(TextureImporter), new AssetPreprocessModule(
                new List<Processor>()
                {
                    new Processor(
                        new FilterNameCondition(".*_n"),
                        new TextureImporterSetting(AssetTemplatePath + "texture_n.jpg")
                    ),

                    new Processor(
                        new FilterNameCondition(".*_d"),
                        new TextureImporterSetting(AssetTemplatePath + "texture_d.jpg")
                    ),

                    new Processor(
                        new AndCondition(new List<ICondition>() { new FilterNameCondition(".*_n"), new FilterNameCondition(".*_d"), }),
                        new TextureImporterSetting(AssetTemplatePath + "texture_d.jpg")
                    ),
                }
            )
        },
#endregion

#region ModelImporter

        {
            typeof(ModelImporter), new AssetPreprocessModule(
                new List<Processor>()
            )
        }

#endregion
    };

    //模型导入之前调用
    public void OnPreprocessModel()
    {
        Debug.Log("OnPreprocessModel=" + this.assetPath);
    }

    //模型导入之前调用  
    public void OnPostprocessModel(GameObject go)
    {
        Debug.Log("OnPostprocessModel=" + go.name);
    }

    //纹理导入之前调用，针对入到的纹理进行设置  
    public void OnPreprocessTexture()
    {
        Debug.Log("OnPreprocessTexture=" + assetImporter.assetPath);
        //AssetPreprocessModule module;
        //if (Config.TryGetValue(typeof(TextureImporter), out module))
        //    module.Execute(assetImporter);
        //else
        //    Debug.Log("未找到相应的处理器");
    }

    public void OnPostprocessTexture(Texture2D tex)
    {
        //Debug.Log("OnPostProcessTexture=" + this.assetPath);
    }

    public void OnPostprocessAudio(AudioClip clip)
    {

    }

    public void OnPreprocessAudio()
    {
        AudioImporter audio = this.assetImporter as AudioImporter;
        //audio. = UnityEngine.AudioCompressionFormat.AAC;// AudioImporterFormat.Compressed;
    }

    //所有的资源的导入，删除，移动，都会调用此方法，注意，这个方法是static的  
    public static void OnPostprocessAllAssets(string[] importedAsset, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        //Debug.Log("OnPostprocessAllAssets");
        //foreach (string str in importedAsset)
        //{
        //    Debug.Log("importedAsset = " + str);
        //}
        //foreach (string str in deletedAssets)
        //{
        //    Debug.Log("deletedAssets = " + str);
        //}
        //foreach (string str in movedAssets)
        //{
        //    Debug.Log("movedAssets = " + str);
        //}
        //foreach (string str in movedFromAssetPaths)
        //{
        //    Debug.Log("movedFromAssetPaths = " + str);
        //}
    }

}

