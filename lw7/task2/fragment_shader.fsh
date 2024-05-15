#version 130

void main()
{
	vec2 pos = gl_TexCoord[0].xy;

	if (pos.y > 0.67)
	{
		gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
	}
	else if (pos.y > 0.33)
	{
		gl_FragColor = vec4(0.0, 0.0, 1.0, 1.0);
	}
	else
	{
		gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0);
	}
}