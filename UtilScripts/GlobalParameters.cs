using System.IO;

namespace GlobalParameters
{
    public static class ProjectGlobalParameters
    {
        public static int CanvasSize = 10;
        public static string ActualFileLocation = "Non opened project";

        //version info
        public const string EditorVersion = "0.2.1";
        public const string CompilerVersion = "Development Phase";
        public const string AddedChanges = "Non implemented";

        //routes for nodes
        public const string CodeEditorNode = "/root/Main/Code_editor_container2/Editor/CodeEdit";
        public const string SaveButtonNode = "/root/Main/HBoxContainer/Menu_Bar/Menu_Bar_Separator/Save_logo_container/Control/SaveCodeButton";
        public const string LoadButtonNode = "/root/Main/HBoxContainer/Menu_Bar/Menu_Bar_Separator/Load_logo_container/Control/LoadButton";

        //files folder generic route
        private static string proyectoPath = Directory.GetCurrentDirectory();
        public static string GenericLocalFolderRoute = Path.Combine(proyectoPath, "Local_Files");

        //github link
        public static string GHLink = "https://github.com/eltut0/Pixell-Wall-E";
    }
}