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
    }

    private async Task EnableCamera()
    {
        var cameras = cameraView.Cameras;
        if (cameras.Count > 0)
        {
            cameraView.Camera = cameras.First();
            cameraView.IsEnabled = true;
            cameraView.MirroredImage = false;
            Camera.MAUI.CameraResult result;
            for (int i = 0; i < 5; i++)
            {
                result = await cameraView.StartCameraAsync();
                Console.WriteLine($"Camera {cameraView.Camera} start {result}");
                if (result == Camera.MAUI.CameraResult.Success)
                {
                    return;
                }
            }
        }
        else
        {
            Console.WriteLine($"No camera found, cannot start camera");
        }
    }


    private async Task DisableCamera()
    {
        var cameras = cameraView.Cameras;
        if (cameras.Count > 0)
        {
            cameraView.Camera = cameras.First();
            cameraView.IsEnabled = false;
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
        else
        {
            Console.WriteLine($"No camera found, cannot stop camera");
        }
    }


    public async Task ActivateAsync(bool foreground)
    {
        if (foreground == true)
        {
            await EnableCamera();
        }
        else
        {
            await DisableCamera();
        }
    }

    async void BtnCamera_Clicked(System.Object sender, System.EventArgs e)
    {
        if (cameraView.IsEnabled == true)
        {
            await DisableCamera();
        }
        else
        {
            await EnableCamera();
        }

    }
}
