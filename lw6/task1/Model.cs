using Assimp;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace task6_1
{
    public class Model
    {
        /**
         * Scene - Объект сцены, куда загружается вся модель. Все данные модели, включая данные о материалах
         * и полигональной сетки. Также ссылается на корневой узел сцены.
         * 
         * Root node - Корневой узел может иметь потомков (как и остальные узлы) и индексы, которые указывают на 
         * данные полигональной сетки, хранящиеся в массиве объекта сцены meshes.
         * 
         * Mesh - Объект полигональной сетки, который содержит все соответствующие данные, необходимые для 
         * отрисовки: вершинные координаты, вектора нормалей, текстурные координаты, грани и информацию о 
         * материале объекта, содержит несколько граней.
         * 
         * Face - Грани представляют собой примитивы объекта (треугольники, квадраты, точки). 
         * Они содержат индексы вершин, образующие примитивы.
         * 
         * Так же, полигональная сетка содержит индекс на материальный объект (Material), который имеет 
         * несколько функций для получения свойств материала.
         */
        private Scene scene = new();
        private Texture texture = new();
        private List<int> displays = [];
        private List<int> textures = [];

        public Model(string filePath)
        {
            AssimpContext importer = new();
            scene = importer.ImportFile(filePath, PostProcessSteps.FlipUVs);

            LoadTextures();
            CreateDisplayLists();
        }

        public void RenderModel()
        {
            for (int i = 0; i < scene.MeshCount; i++)
            {
                GL.CallList(displays[i]);
            }
        }

        private void CreateDisplayLists()
        {
            displays = new(new int[scene.MeshCount]);

            for (int i = 0; i < scene.MeshCount; i++)
            {
                Mesh mesh = scene.Meshes[i];

                displays[i] = GL.GenLists(1);
                GL.NewList(displays[i], ListMode.Compile);

                ApplyMaterial(scene.Materials[mesh.MaterialIndex], i);

                GL.Begin(GetPrimitiveTypeByMesh(mesh));

                for (int j = 0; j < mesh.Vertices.Count; j++)
                {
                    var normal = mesh.Normals[j];
                    var vertex = mesh.Vertices[j];
                    var texture = mesh.TextureCoordinateChannels[0][j];

                    GL.Normal3(normal.X, normal.Y, normal.Z);
                    if (scene.Materials[mesh.MaterialIndex].HasTextureDiffuse)
                    {
                        GL.TexCoord2(texture.X, texture.Y);
                    }
                    GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
                }

                GL.End();

                GL.EndList();
            }
        }

        private OpenTK.Graphics.OpenGL.PrimitiveType GetPrimitiveTypeByMesh(Mesh mesh)
        {
            int faceIndexCount = mesh.Faces[0].IndexCount;
            if (4 == faceIndexCount)
            {
                return OpenTK.Graphics.OpenGL.PrimitiveType.Quads;
            }
            else if (3 == faceIndexCount)
            {
                return OpenTK.Graphics.OpenGL.PrimitiveType.Triangles;
            }
            throw new Exception("Unsupported quantity in faces");
        }

        private void ApplyMaterial(Material material, int index)
        {
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient,
                ConvertAssimpColorToColor4(material.ColorAmbient));
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse,
                ConvertAssimpColorToColor4(material.ColorDiffuse));
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular,
                ConvertAssimpColorToColor4(material.ColorSpecular));
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess,
                material.Shininess);

            if (material.GetMaterialTexture(TextureType.Diffuse, index, out var _))
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, textures[index]);
            }
        }

        private Color4 ConvertAssimpColorToColor4(Color4D color4d)
        {
            return new Color4(color4d.R, color4d.G, color4d.B, color4d.A);
        }

        private void LoadTextures()
        {
            foreach (Mesh mesh in scene.Meshes)
            {
                if (mesh.MaterialIndex < 0) continue;

                Material material = scene.Materials[mesh.MaterialIndex];

                for (int i = 0; i < material.GetMaterialTextureCount(TextureType.Diffuse); i++)
                {
                    if (material.GetMaterialTexture(TextureType.Diffuse, i, out var textureSlot) &&
                        File.Exists(textureSlot.FilePath))
                    {
                        textures.Add(texture.LoadTexture(textureSlot.FilePath));
                    }
                }
            }
        }
    }
}
