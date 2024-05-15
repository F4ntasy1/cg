#version 130

attribute float x;

void main()
{
    gl_Position = gl_ModelViewProjectionMatrix * vec4(0.0, 0.0, 0.0, 1.0);
    gl_FrontColor = (gl_Position + vec4(1.0)) * 0.5;
}