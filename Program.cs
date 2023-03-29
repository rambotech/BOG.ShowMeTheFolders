// See https://aka.ms/new-console-template for more information

using BOG.SwissArmyKnife;
using Figgle;
using System.Runtime.InteropServices;

try
{
	Environment.ExitCode = 0;
	var av = new BOG.SwissArmyKnife.AssemblyVersion();
	Environment.ExitCode = 0;
	Console.WriteLine(FiggleFonts.Ivrit.Render("ShowMeTheFolders"));
	Console.WriteLine($"{Path.GetFileName(av.Filename)}, {av.Version}, {av.BuildDate:G}");

	var osPlatform =
		RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD) ? "FreeBSD" : (
		RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "Linux" : (
		RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Windows" : (
		RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "OSX" : "Unnknown")));

	Console.WriteLine("");
	Console.WriteLine("HostName: ............  {0}", Environment.MachineName);
	Console.WriteLine("Platform: ............  {0}", osPlatform);
	Console.WriteLine("Processor architecture: {0}", RuntimeInformation.ProcessArchitecture);
	Console.WriteLine("Framework: ...........  {0}", RuntimeInformation.FrameworkDescription);
	Console.WriteLine("O/S Info: ............  {0}", RuntimeInformation.OSDescription);
	Console.WriteLine("");

	var pass = 0;
	while (pass < 2)
	{
		Console.WriteLine();
		Console.WriteLine("===============================================================");
		switch (pass)
		{
			case 0:
				Console.WriteLine("Have Definitions");
				break;
			case 1:
				Console.WriteLine("No Definitions");
				break;
		}
		Console.WriteLine("===============================================================");
		var names = Enum.GetNames(typeof(Environment.SpecialFolder));
		var values = Enum.GetValues(typeof(Environment.SpecialFolder));
		for (int index = 0; index < names.Length; index++)
		{
			var dirEnum = (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), names[index]);
			var thisLocation = Environment.GetFolderPath(dirEnum);
			switch (pass)
			{
				case 0:
					if (!string.IsNullOrWhiteSpace(thisLocation))
					{
						Console.WriteLine($"{names[index]}");
						Console.WriteLine($"    .....: {thisLocation}");
					}
					break;

				case 1:
					if (string.IsNullOrWhiteSpace(thisLocation))
					{
						Console.WriteLine($"{names[index]}");
					}
					break;
			}
		}
		pass++;
	}
}
catch (Exception ex)
{
	Console.WriteLine(DetailedException.WithMachineContent(ref ex));
	Environment.ExitCode = 1;
}
Console.WriteLine($"Exit Code: {Environment.ExitCode}");
#if DEBUG
Console.ReadLine();
#endif
