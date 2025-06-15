using Godot;
public static class InfoWindow
{
    public static void DisplayInfoWindow(string title, string text, int length, int heigth)
    {
        var inputwindow = new Window
        {
            Title = title,
            Size = new Vector2I(length, heigth),
            Unresizable = true
        };

        var vbox = new VBoxContainer
        {
            Size = inputwindow.Size
        };

        //label 
        var editorv = new Label
        {
            Text = text,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var button = new Button
        {
            Text = "OK",
            SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter
        };

        vbox.AddChild(editorv);
        vbox.AddChild(button);
        inputwindow.AddChild(vbox);

        button.Pressed += () =>
        {
            inputwindow.QueueFree();
        };

        SceneTree root = Engine.GetMainLoop() as SceneTree;
        root.Root.AddChild(inputwindow);

        inputwindow.CloseRequested += () => inputwindow.QueueFree();

        inputwindow.PopupCentered();
    }
}