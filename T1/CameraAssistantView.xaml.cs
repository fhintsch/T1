namespace T1;

public partial class CameraAssistantView : ContentView, ITabView
{
    public CameraAssistantView()
    {
        InitializeComponent();
        cameraView.CamerasLoaded += CameraView_CamerasLoaded;
    }

    private void CameraView_CamerasLoaded(object sender, EventArgs e)
    {
        Console.WriteLine($"CamerasLoaded");
        var cameras = cameraView.Cameras;
        if (cameras.Count > 0)
        {
            cameraView.Camera = cameras.First();
            EnableCamera();
        }
    }

    private async void EnableCamera()
    {
        //MainThread.BeginInvokeOnMainThread(async () =>
        //{
        cameraView.Camera = cameraView.Cameras.First();
        cameraView.MirroredImage = true;
        var result = await cameraView.StartCameraAsync();
        Console.WriteLine($"Camera {cameraView.Camera} start {result}");
        //});
    }


    private async void DisableCamera()
    {
        cameraView.Camera = cameraView.Cameras.First();
        Camera.MAUI.CameraResult result;
        for (int i = 0; i < 5; i++)
        {
            result = await cameraView.StopCameraAsync();
            Console.WriteLine($"Camera {cameraView.Camera} stop {result}");
            if (result == Camera.MAUI.CameraResult.Success)
            {
                return;
            }
        }
    }


    public void ActivateAsync(bool foreground)
    {
        if (foreground == true)
        {
            EnableCamera();
        }
        else
        {
            DisableCamera();
        }
    }

}
