using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
   public class TextureScaler
{
    public static Texture2D Scaled(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0,0,width,height);
        GPUScale(src,width,height,mode);

        Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
        result.Resize(width, height);
        result.ReadPixels(texR,0,0,true);
        return result;          
    }
    static void GPUScale(Texture2D src, int width, int height, FilterMode fmode)
    {
        src.filterMode = fmode;
        src.Apply(true);    
        
        RenderTexture rtt = new RenderTexture(width, height, 32);
        
        Graphics.SetRenderTarget(rtt);
        
        GL.LoadPixelMatrix(0,1,1,0);
        
        GL.Clear(true,true,new Color(0,0,0,0));
        Graphics.DrawTexture(new Rect(0,0,1,1),src);
    }
}
}
