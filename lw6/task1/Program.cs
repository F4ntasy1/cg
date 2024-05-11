using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using task6_1;

class Program
{
    static void Main(string[] args)
    {
        NativeWindowSettings nativeWindowSettings = new()
        {
            ClientSize = new Vector2i(1200, 1200),
            Location = new Vector2i(30, 30),
            WindowBorder = WindowBorder.Resizable,
            WindowState = WindowState.Normal,
            Title = "My window",
            Flags = ContextFlags.Default,
            APIVersion = new Version(3, 3),
            Profile = ContextProfile.Compatability,
            API = ContextAPI.OpenGL,
            NumberOfSamples = 0
        };

        Window window = new(nativeWindowSettings, []);
        window.Run();
    }
}