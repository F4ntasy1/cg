using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace task5_1
{
    public class Texture
    {
        public int LoadTexture(
            string filepath,
            TextureMagFilter magFilter,
            TextureMinFilter minFilter,
            TextureWrapMode wrapS = TextureWrapMode.Repeat,
            TextureWrapMode wrapT = TextureWrapMode.Repeat
        )
        {
            Bitmap bmp = new(filepath);

            System.Drawing.Imaging.PixelFormat pixelFormat = 0;
            OpenTK.Graphics.OpenGL.PixelFormat textureFormat = 0;
            PixelInternalFormat internalFormat = 0;

            if (IsAlphaBitmap(bmp))
            {
                pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
                // BGRA - blue, green, red, alpha (optional)
                textureFormat = OpenTK.Graphics.OpenGL.PixelFormat.Bgra;
                // внутреннее представление openGL
                internalFormat = PixelInternalFormat.Rgba;
            } 
            else
            {
                pixelFormat = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
                textureFormat = OpenTK.Graphics.OpenGL.PixelFormat.Bgr;
                internalFormat = PixelInternalFormat.Rgb;
            }

            // генерируем один уникальный идентификатор текстурного объекта
            GL.GenTextures(1, out int textureId);
            // делаем активным текстурный объект с данным идентификатором
            // (с ним еще пока не связано никакое изображение)
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                pixelFormat
            );

            // Задаем текстурное изображение для 0 уровня детализации
            GL.TexImage2D(
                TextureTarget.Texture2D, 
                0,      // уровень детализации
                internalFormat, 
                bmpData.Width, 
                bmpData.Height, 
                0,      // ширина рамки текстуры (0 - нет рамки, 1 - рамка в 1 пиксель)
                textureFormat, 
                PixelType.UnsignedByte,     // каждая компонента цвета занимает один байт 
                bmpData.Scan0               // адрес первой строки изображения
            );

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter); // поведение пикселей при уменьшении текстуры
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter); // поведение пикселей при увеличении текстуры
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapS); // способ заполнения текстуры по ширине
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapT); // способ заполнения текстуры по высоте

            bmp.UnlockBits(bmpData);

            return textureId;
        }

        /// <summary>
        /// Возвращает true, если в bitmap есть альфа канал
        /// </summary>
        private bool IsAlphaBitmap(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    var pixel = bmp.GetPixel(i, j);
                    if (pixel.A != 255)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
