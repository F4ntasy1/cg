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

        //CircleDiagram circleDiagram = new();
        Torus torus = new Torus();

        Window window = new(nativeWindowSettings, [torus]);
        window.Run();
    }
}