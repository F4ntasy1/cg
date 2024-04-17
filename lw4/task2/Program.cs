using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using task2;

class Program
{
    static void Main(string[] args)
    {
        NativeWindowSettings nativeWindowSettings = new()
        {
            ClientSize = new Vector2i(900, 900),
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

        Torus torus1 = new();
        Torus torus2 = new();
        Torus torus3 = new();

        Window window = new(nativeWindowSettings, [torus1, torus2, torus3]);
        window.Run();
    }
}