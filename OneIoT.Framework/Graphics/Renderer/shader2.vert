#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;
layout(location = 2) in vec2 aTexCoord;


out vec2 texCoord;
out vec4 ourColor;

void main(void)
{
    // Then, we further the input texture coordinate to the output one.
    // texCoord can now be used in the fragment shader.

    texCoord = aTexCoord;
    ourColor = aColor;
    
    gl_Position = vec4(aPosition, 1.0);
}