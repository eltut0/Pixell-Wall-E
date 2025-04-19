public static class Utils
{
    public static string FileNameCorrection(string filename)
    {
        if (filename.Length > 3)
        {
            if (filename[filename.Length - 1] == 'w' && filename[filename.Length - 2] == 'p' && filename[filename.Length - 3] == '.')
            {
                filename = filename.Substring(0, filename.Length - 3);
            }
        }

        return filename;
    }
}