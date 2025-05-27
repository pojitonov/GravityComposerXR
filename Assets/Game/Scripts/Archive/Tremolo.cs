// using FMOD.Studio;
// using PrimeTween;
// using UnityEngine;
// using UnityEngine.Rendering;
// using Shapes;
//
// public class Tremolo : ImmediateModeShapeDrawer
// {
//     private float radius = 0.1f;
//     private float thickness = 0.001f;
//     
//     private Bounds bounds;
//     private EventInstance instance;
//     
//     private bool isTriggered;
//     private bool isSoundDetected;
//     
//     private float currentTremolo;
//
//
//     private void Update()
//     {
//         DetectSound();
//     }
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (!other.CompareTag("Ring"))
//             return;
//
//         bounds = other.bounds;
//         isTriggered = true;
//     }
//
//     private void OnTriggerExit(Collider other)
//     {
//         if (!other.CompareTag("Ring"))
//             return;
//
//         isTriggered = false;
//     }
//
//     private void DetectSound()
//     {
//         if (!isTriggered) return;
//         
//         float maxDistance = 1f;
//         Vector3 origin = transform.position;
//         Vector3 direction = Vector3.down;
//
//         if (Physics.SphereCast(origin, radius, direction, out RaycastHit hit, maxDistance))
//         {
//             Wave wave = hit.collider.GetComponent<Wave>();
//             if (wave)
//             {
//                 instance = wave.EventInstance;
//                 instance.setParameterByName("Tremolo", 0.5f);
//                 instance.setParameterByName("Tremolo_Freq", 0.5f);
//                 isSoundDetected = true;
//             }
//             else
//             {
//                 instance.setParameterByName("Tremolo", 0);
//                 instance.setParameterByName("Tremolo_Freq", 0);
//                 instance = new EventInstance();
//                 isSoundDetected = false;
//             }
//         }
//     }
//
//     public override void DrawShapes(Camera cam)
//     {
//         if (!cam) return;
//         if (!isTriggered) return;
//
//         using (Draw.Command(cam))
//         {
//             Draw.ZTest = CompareFunction.Always;
//
//             Color color1 = Color.yellow;
//             Vector3 origin = transform.position;
//             Vector3 direction = Vector3.down;
//
//             if (Physics.Raycast(origin, direction, out RaycastHit hit))
//             {
//                 if (hit.collider.CompareTag("Desk"))
//                 {
//                     Draw.Ring(hit.point, transform.up, radius: radius, thickness: thickness, color1);
//                 }
//             }
//         }
//     }
// }