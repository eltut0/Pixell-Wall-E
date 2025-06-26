using System;
using System.IO;

namespace GlobalParameters
{
    public static class ProjectGlobalParameters
    {
        public static int CanvasSize { get; set; } = 10;
        public static string ActualFileLocation { get; set; } = "Non opened project";

        //version info
        public const string EditorVersion = "2.1";
        public const string CompilerVersion = "1.1";
        public const string AddedChanges = "Non implemented";

        //routes for nodes
        public const string CodeEditorNode = "/root/Main/Code_editor_container2/Editor/CodeEdit";
        public const string SaveButtonNode = "/root/Main/HBoxContainer/Menu_Bar/Menu_Bar_Separator/Save_logo_container/Control/SaveCodeButton";
        public const string LoadButtonNode = "/root/Main/HBoxContainer/Menu_Bar/Menu_Bar_Separator/Load_logo_container/Control/LoadButton";
        public const string CanvasNode = "/root/Main/Code_editor_container2/VBoxContainer/VBoxContainer2/Canvas";

        //files folder generic route
        private static readonly string proyectoPath = Directory.GetCurrentDirectory();
        public static readonly string GenericLocalFolderRoute = Path.Combine(proyectoPath, "Local_Files");

        //github link
        public static readonly string GHLink = "https://github.com/eltut0/Pixell-Wall-E/blob/main/README.md";
    }
}