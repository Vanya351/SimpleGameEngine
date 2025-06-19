#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec4 aColor;

layout (location = 2) in vec4 transCol1;
layout (location = 3) in vec4 transCol2;
layout (location = 4) in vec4 transCol3;
layout (location = 5) in vec4 transCol4;

out vec4 ourColor;

void main()
{
    gl_Position = vec4(aPos, 0.0, 1.0) * mat4(transCol1, transCol2, transCol3, transCol4);
    ourColor = aColor;
} 