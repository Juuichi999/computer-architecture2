namespace computer_architecture;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        //meow
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: program <directory> <pattern> [<newDateTime>]");
            return;
        }

        var directory = args[0];
        var pattern = args[1];
        var newDateTime = args.Length > 2 ? DateTime.Parse(args[2]) : DateTime.Now;

        if (!Directory.Exists(directory))
        {
            Console.WriteLine($"Directory {directory} does not exist.");
            return;
        }

        var files = Directory.GetFiles(directory, pattern, SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            Console.WriteLine($"No files matching pattern {pattern} found in directory {directory}.");
            return;
        }

        foreach (var file in files)
        {
            try
            {
                if ((File.GetAttributes(file) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    Console.WriteLine($"File {file} is read-only and cannot be modified.");
                    continue;
                }

                File.SetCreationTime(file, newDateTime);
                Console.WriteLine($"Changed creation time of file {file} to {newDateTime}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to change creation time of file {file}. Error: {ex.Message}");
            }
        }
    }
}