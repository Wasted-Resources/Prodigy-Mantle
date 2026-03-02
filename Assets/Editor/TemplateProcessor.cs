using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Description: Intercepts the creation of new C# scripts to populate organizational placeholders.
/// Coordination: Matches placeholders in WastedResourcesUnityTemplate.cs.txt.
/// Deployment: This must stay in the Assets/Editor folder.
/// </summary>
public class TemplateProcessor : AssetModificationProcessor
{
    private const string ProjectPlaceholder = "[Project Name]";
    private const string AuthorPlaceholder  = "[Author]";
    private const string EmailPlaceholder   = "[Email]";
    private const string DatePlaceholder    = "#DATE#";

    [SerializeField] private const string OrgProjectName = "Prodigy-Mantle";
    [SerializeField] private const string OrgAuthorName  = "Christof Kloninger";
    [SerializeField] private const string OrgAuthorEmail = "gme.24.kloninger@gmail.com";

    /// <summary>
    /// Unity calls this method whenever a new asset is created.
    /// </summary>
    public static void OnWillCreateAsset(string assetPath)
    {
        assetPath = assetPath.Replace(".meta", "");
        if (!assetPath.EndsWith(".cs")) return;
        string fullPath = Path.GetFullPath(assetPath);
        if (!File.Exists(fullPath)) return;

        try
        {
            string content = File.ReadAllText(fullPath);
            content = content.Replace(ProjectPlaceholder, OrgProjectName);
            content = content.Replace(AuthorPlaceholder, OrgAuthorName);
            content = content.Replace(EmailPlaceholder, OrgAuthorEmail);
            content = content.Replace(DatePlaceholder, DateTime.Now.ToString("yyyy-MM-dd"));

            File.WriteAllText(fullPath, content);
            
            AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            Debug.LogError($"[Wasted Resources] Automation failed for {assetPath}: {e.Message}");
        }
    }
}