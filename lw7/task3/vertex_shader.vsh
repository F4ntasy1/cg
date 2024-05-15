#version 330 core
layout (location = 0) in vec3 position;

uniform mat4 modelViewProjectionMatrix; 
uniform float progress;

void main()
{
    vec2 pos = position.xy;

    vec3 startPos = vec3(pos.x, pos.y, (pos.x * pos.x + pos.y * pos.y) / 10);
    vec3 endPos = vec3(pos.x, pos.y, (pos.x * pos.x - pos.y * pos.y) / 10);
    vec3 morphedPos = mix(startPos, endPos, progress);

    gl_Position = modelViewProjectionMatrix * vec4(morphedPos, 1.0);
    gl_FrontColor = (gl_Position + vec4(1.0)) * 0.5;
}