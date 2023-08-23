# Cannot start/stop camera in a reliable way
## Environment
- Nuget package: Camera.MAUI 1.4.4
- Target: MacCatalyst

## Problem description
I want to use the camera in assistant-like dialog where, i.e. a number of views with a common design each connected by a forward and 
a backward button.
The common design is implemented by a TcameraPage and it's varying content is implemented by several ContentView that are injected into TcameraPage. 

In this test the assistant consists of 4 steps. In step 2 a cameraview is needed. This is implemented by CameraAssistantView. 
When moving into step 2 the CameraAssistantView is injected and the camera should start, when moving out the camera should stop.

Unfortunately this works only sometimes. If not the camera start first results in an **AccessError**. 
Then on the second try the result is **Success** and one can see the playback picture on the screen. 
But upon leaving the step 2 the camera stop action results in **AccessError**. 


```
2023-08-22 16:51:29.166 T1[60533:1070729] ENTER T1.CameraAssistantView
2023-08-22 16:51:29.166 T1[60533:1070729] Set Content
2023-08-22 16:51:34.412 T1[60533:1070729] CamerasLoaded
2023-08-22 16:51:36.093 T1[60533:1070729] Camera FaceTime HD-Kamera start AccessError
2023-08-22 16:51:36.383 T1[60533:1070729] Camera FaceTime HD-Kamera start Success
2023-08-22 16:51:52.377 T1[60533:1070729] LEAVE T1.CameraAssistantView
2023-08-22 16:51:52.388 T1[60533:1070729] ENTER Microsoft.Maui.Controls.VerticalStackLayout
2023-08-22 16:51:52.388 T1[60533:1070729] Set Content
2023-08-22 16:51:52.420 T1[60533:1070729] Camera FaceTime HD-Kamera stop AccessError
2023-08-22 16:51:52.443 T1[60533:1070729] Camera FaceTime HD-Kamera stop AccessError
2023-08-22 16:51:52.443 T1[60533:1070729] Camera FaceTime HD-Kamera stop AccessError
2023-08-22 16:51:52.445 T1[60533:1070729] Camera FaceTime HD-Kamera stop AccessError
2023-08-22 16:51:52.445 T1[60533:1070729] Camera FaceTime HD-Kamera stop AccessError
```
