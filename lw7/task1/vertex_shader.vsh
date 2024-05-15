#version 130

attribute float x;

uniform mat4 modelViewProjectionMatrix;

void main()
{
    float R = (1 + sin(x)) * (1 + 0.9 * cos(8 * x)) * (1 + 0.1 * cos(24 * x)) * (0.5 + 0.05 * cos(140 * x));
    float x_prime = R * cos(x);
    float y_prime = R * sin(x);
    float z_prime = 0;

    gl_Position = modelViewProjectionMatrix * vec4(x_prime, y_prime, z_prime, 1.0);
    gl_FrontColor = (gl_Position + vec4(1.0)) * 0.5;
}