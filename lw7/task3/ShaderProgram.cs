using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace task7_1
{
    public class ShaderProgram
    {
        public int shaderProgram = GL.CreateProgram();

        private List<Shader> _shaders = [];

        public void AttachShader(Shader shader)
        {
            GL.AttachShader(shaderProgram, shader.shader);
            _shaders.Add(shader);
        }

        public void DetachShaders()
        {
            foreach (var shader in _shaders)
            {
                GL.DetachShader(shaderProgram, shader.shader);
            }
        }

        public void Compile()
        {
            GL.LinkProgram(shaderProgram);
            GL.UseProgram(shaderProgram);
            CheckStatus();
        }

        public void DeleteProgram()
        {
            GL.DeleteProgram(shaderProgram);
        }

        public void CheckStatus()
        {
            GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out int status);
            if (status != 1)
            {
                throw new Exception("Program " + shaderProgram + " compile exception: " +
                    GL.GetProgramInfoLog(shaderProgram));
            }
            LogToConsole();
        }

        public void LogToConsole()
        {
            Console.WriteLine("<--- Program " + shaderProgram + " log --->");
            Console.WriteLine(GL.GetProgramInfoLog(shaderProgram));
            Console.WriteLine("<=== Program log end ===>");
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(shaderProgram, name);
        }

        public int GetAttributeLocation(string name)
        {
            return GL.GetAttribLocation(shaderProgram, name);
        }
    }
}
