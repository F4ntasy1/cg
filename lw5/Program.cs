using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using task5_1;

class Program
{
    static void Main(string[] args)
    {
        NativeWindowSettings nativeWindowSettings = new()
        {
            ClientSize = new Vector2i(2100, 1200),
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

        LightObject lightObject = new();

        House house = new();
        Garage garage = new();
        Plot plot = new();

        Window window = new(nativeWindowSettings, [lightObject, house, garage, plot]);
        window.Run();
    }
}