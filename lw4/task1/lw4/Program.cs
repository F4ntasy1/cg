﻿using lw4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using task1;

class Program
{
    static void Main(string[] args)
    {
        NativeWindowSettings nativeWindowSettings = new()
        {
            ClientSize = new Vector2i(900, 900),
            Title = "My window",
            Flags = OpenTK.Windowing.Common.ContextFlags.Default,
            Profile = OpenTK.Windowing.Common.ContextProfile.Compatability,
            API = OpenTK.Windowing.Common.ContextAPI.OpenGL
        };

        IDrawable dodecahedron = new Dodecahedron();

        Window window = new(nativeWindowSettings, [dodecahedron]);
        window.Run();
    }
}