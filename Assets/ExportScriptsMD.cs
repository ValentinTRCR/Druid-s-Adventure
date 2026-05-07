#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ExportScriptsMD : MonoBehaviour
{
    [Header("Glisse tes dossiers ici")]
    public List<DefaultAsset> folders; // drag & drop de plusieurs dossiers

    [Header("Nom du fichier Markdown")]
    public string fileName = "AllScripts_FromMultipleFolders.md";

    [Header("Descriptions (facultatif, par script)")]
    [TextArea]
    public List<string> descriptions;

    [ContextMenu("Exporter tous les scripts des dossiers en Markdown")]

    void Start()
    {
        Export();       
    }
    public void Export()
    {
        List<string> allFiles = new List<string>();

        // 1️⃣ Parcourir tous les dossiers glissés
        foreach (var folder in folders)
        {
            if (folder == null) continue;

            string folderPath = AssetDatabase.GetAssetPath(folder);
            string fullFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

            if (Directory.Exists(fullFolderPath))
            {
                string[] files = Directory.GetFiles(fullFolderPath, "*.cs", SearchOption.AllDirectories);
                allFiles.AddRange(files);
            }
        }

        if (allFiles.Count == 0)
        {
            Debug.LogWarning("⚠️ Aucun script trouvé dans les dossiers glissés");
            return;
        }

        string md = "# Table des matières\n\n";

        // 2️⃣ Table des matières
        for (int i = 0; i < allFiles.Count; i++)
        {
            string fileNameOnly = Path.GetFileNameWithoutExtension(allFiles[i]);
            string description = (i < descriptions.Count) ? descriptions[i] : "";
            string anchor = fileNameOnly.ToLower().Replace(" ", "_");

            md += $"- [{fileNameOnly}](#{anchor})";
            if (!string.IsNullOrEmpty(description))
                md += $" - {description}";
            md += "\n";
        }

        md += "\n---\n\n";

        // 3️⃣ Contenu des scripts
        for (int i = 0; i < allFiles.Count; i++)
        {
            string path = allFiles[i];
            string fileNameOnly = Path.GetFileNameWithoutExtension(path);
            string description = (i < descriptions.Count) ? descriptions[i] : "";
            string anchor = fileNameOnly.ToLower().Replace(" ", "_");

            md += $"# {fileNameOnly} {{#{anchor}}}\n";
            md += $"**Chemin :** {path}\n\n";
            if (!string.IsNullOrEmpty(description))
                md += $"**Description :** {description}\n\n";

            md += "```csharp\n";
            md += File.ReadAllText(path);
            md += "\n```\n\n";
            md += "---\n\n";
        }

        // 4️⃣ Écriture du fichier Markdown
        string outputPath = Path.Combine(Application.dataPath, fileName);
        File.WriteAllText(outputPath, md);

        Debug.Log("✅ Markdown exporté pour plusieurs dossiers : " + outputPath);
    }
}
#endif