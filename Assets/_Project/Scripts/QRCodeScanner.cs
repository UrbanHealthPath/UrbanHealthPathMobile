using System;
using UnityEngine;
using ZXing;
using UnityEngine.UI;
using ZXing.Aztec.Internal;
using ZXing.Common;
using System.Runtime.InteropServices;
public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage rawImageBackground;

    [SerializeField] private AspectRatioFitter aspectRatioFitter;

    [SerializeField] private Text outputText;

    [SerializeField] private RectTransform scanZone;

    private bool camAvailable;

    private WebCamTexture cameraTexture;

    // Start is called before the first frame update
    private void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateCameraRender();
        //if (Detect())
        //{
            //Scan();
        //}
    }

    private void UpdateCameraRender()
    {
        if (camAvailable)
        {
            aspectRatioFitter.aspectRatio = (float) cameraTexture.width / (float) cameraTexture.height;
            rawImageBackground.rectTransform.localEulerAngles =
                new Vector3(0, 0, -cameraTexture.videoRotationAngle);
        }
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            camAvailable = false;
            return;
        }

        foreach (var cam in devices)
        {
            if (cam.isFrontFacing == false)
            {
                cameraTexture =
                    new WebCamTexture(cam.name, (int) scanZone.rect.width, (int) scanZone.rect.height);
            }
        }
        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        camAvailable = true;
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width,
                cameraTexture.height);
            if (result != null)
            {
                outputText.text = result.Text;
            }
            else
            {
                outputText.text = "FAILED TO READ QR CODE";
            }
        }
        catch
        {
            outputText.text = "QR SCAN FAILED";
        }
    }

    private bool Detect()
    {
        try
        {
            LuminanceSource source =
            new RGBLuminanceSource(Color32ArrayToByte(cameraTexture.GetPixels32()), cameraTexture.width, 
                cameraTexture.height); 
            var binarizer = new HybridBinarizer(source);
            var binBitmap = new BinaryBitmap(binarizer);
            BitMatrix bm = binBitmap.BlackMatrix;
            Detector detector = new Detector(bm);
            DetectorResult result = detector.detect();
            return true;
        }
        catch
        {
            return false;
        }
    }

    private byte[] Color32ArrayToByte(Color32[] colors)
    {
        if (colors == null || colors.Length == 0)
            return null;

        int lengthOfColor32 = Marshal.SizeOf(typeof(Color32));
        int length = lengthOfColor32 * colors.Length;
        byte[] bytes = new byte[length];

        GCHandle handle = default(GCHandle);
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            Marshal.Copy(ptr, bytes, 0, length);
        }
        finally
        {
            if (handle != default(GCHandle))
                handle.Free();
        }

        return bytes;
    }

    public void OnClickScan()
    {
        Scan();
    }
}

