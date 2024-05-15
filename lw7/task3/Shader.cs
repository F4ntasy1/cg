using OpenTK.Graphics.OpenGL;

namespace task7_3
{
    public class Shader
    {
        public int shader;

        public Shader(ShaderType shaderType, string filePath)
        {
            shader = GL.CreateShader(shaderType);
            SetSourceByFile(filePath);
        }

        public void SetSourceByFile(string filePath)
        {
            StreamReader reader = new(filePath);
            string text = reader.ReadToEnd();
            GL.ShaderSource(shader, text);
            CompileShader();
        }

        public void CompileShader()
        {
            GL.CompileShader(shader);
        }

        public void CheckStatus()
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
            if (status != 1)
            {
                throw new Exception("Shader " + shader + " compile exception: " + 
                    GL.GetShaderInfoLog(shader));
            }
            LogToConsole();
        }

        public void LogToConsole()
        {
            Console.WriteLine("<--- Shader " + shader + " log --->");
            Console.WriteLine(GL.GetShaderInfoLog(shader));
            Console.WriteLine("<=== Shader log end ===>");
        }

        public void Delete()
        {
            GL.DeleteShader(shader);
        }
    }
}
