namespace Nikki.Reflection.Interface
{
    interface IOperative
    {
        bool TryAddCollection(string CName, string root);
        bool TryAddCollection(string CName, string root, out string error);
        bool TryRemoveCollection(string CName, string root);
        bool TryRemoveCollection(string CName, string root, out string error);
        bool TryCloneCollection(string newname, string copyfrom, string root);
        bool TryCloneCollection(string newname, string copyfrom, string root, out string error);
        bool TryImportCollection(string root, string filepath);
        bool TryImportCollection(string root, string filepath, out string error);
        bool TryExportCollection(string CName, string root, string filepath);
        bool TryExportCollection(string CName, string root, string filepath, out string error);
    }
}