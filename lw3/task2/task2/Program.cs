using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using task1;

class Program
{
    static void Main(string[] args)
    {
        NativeWindowSettings nativeWindowSettings = new()
        {
            ClientSize = new Vector2i(1200, 1200),
            Title = "My window",
            Flags = OpenTK.Windowing.Common.ContextFlags.Default,
            Profile = OpenTK.Windowing.Common.ContextProfile.Compatability,
            API = OpenTK.Windowing.Common.ContextAPI.OpenGL
        };

        Window window = new(nativeWindowSettings);
        window.Run();
    }
}