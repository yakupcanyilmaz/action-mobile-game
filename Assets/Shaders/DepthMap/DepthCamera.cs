using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DepthCamera : MonoBehaviour
{
  public int horizontalResolution = 512;
  public int verticalResolution = 512;

  public Shader customShader;
  public bool isCustomShader;

  static int camMatrix = Shader.PropertyToID("_CamMatrix"),
    camRenderTexture = Shader.PropertyToID("_CamRenderTexture");

  private Camera cam;
  private RenderTexture renderTexture;

  void Start()
  {
    if (cam == null)
    {
      cam = GetComponent<Camera>();
    }

    cam.enabled = false;

    renderTexture = new RenderTexture(horizontalResolution, verticalResolution, 24, RenderTextureFormat.Depth);
    renderTexture.Create();
    cam.targetTexture = renderTexture;
  }


  private void Reset()
  {
    cam = GetComponent<Camera>();
  }

  void Update()
  {
    if (cam == null)
    {
      return;
    }

    var dimensions = cam.worldToCameraMatrix;
    var perspective = cam.projectionMatrix;
    var depthCamMatrix = perspective * dimensions;

    var projection = GL.GetGPUProjectionMatrix(depthCamMatrix, true);
    Shader.SetGlobalMatrix(camMatrix, projection);
    Shader.SetGlobalTexture(camRenderTexture, renderTexture);

    if (isCustomShader)
    {
      cam.RenderWithShader(customShader, "RenderType");
    }
    else
    {
      cam.Render();
    }
  }
}
