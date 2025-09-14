using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MFGTweaks;

public class TexturePatcher
{

    private Dictionary<int, Texture> _patchedTextures = [];

    public Texture PatchTexture(Texture tex, string path)
    {
        if (_patchedTextures.ContainsKey(tex.GetInstanceID()))
            return _patchedTextures[tex.GetInstanceID()];

        var patchTex = LoadTexture(path);
        var renderTex = new RenderTexture(tex.width, tex.height, 0, RenderTextureFormat.ARGB32);
        
        var mat = new Material(Shader.Find("Sprites/Default"));
        
        Graphics.Blit(tex, renderTex);
        Graphics.Blit(patchTex, renderTex, mat);
        
        RenderTexture.active = renderTex;
        var outTexture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        outTexture.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        outTexture.Apply();
        RenderTexture.active = null;

        _patchedTextures.Add(tex.GetInstanceID(), outTexture);
        return outTexture;
    }

    private Texture2D LoadTexture(string path)
    {
        var fileData = File.ReadAllBytes(path);
        var tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);
        return tex;
    }

}
