// using UnityEngine;
// using UnityEngine.Rendering;
// using FMOD.Studio;
// using FMODUnity;
// using PrimeTween;
// using Shapes;
//
// public class Volume : ImmediateModeShapeDrawer
// {
//     private Color color1 = Color.black;
//     private Color color2 = Color.yellow;
//     private float thickness = 0.001f;
//     
//     private TextElement text;
//     private Tween tween;
//     private EventInstance instance;
//     private Sound sound;
//     private Bounds bounds;
//     
//     private bool isTriggered;
//     private bool isSoundFound;
//     
//     private float currentPitch;
//     private float currentTremolo;
//
//     private void Awake()
//     {
//         text = new TextElement();
//     }
//
//     private void Update()
//     {
//         GetSound();
//         UpdateParam();
//     }
//
//     private void GetSound()
//     {
//         if (!isTriggered) return;
//         
//         sound = FindAnyObjectByType<Sound>();
//         isSoundFound = sound;
//         
//         if (!isSoundFound) return;
//         instance = sound.EventInstance;
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
//             Vector3 origin = transform.position;
//             Vector3 direction = Vector3.down;
//
//             if (Physics.Raycast(origin, direction, out RaycastHit hit))
//             {
//                 if (hit.collider.CompareTag("Desk"))
//                 {
//                     Draw.Line(origin, hit.point, thickness, color2);
//                     Draw.Disc(hit.point, cam.transform.forward, thickness, Color.black);
//                 }
//             }
//
//             Draw.Disc(origin, cam.transform.forward, thickness, color1);
//             var content = "Volume: " + currentPitch.ToString("F1");
//             Draw.Text(text, transform.position + new Vector3(0, 0.035f, 0), fontSize: 0.1f, content: content,
//                 color: color2);
//         }
//     }
//
//     private void UpdateParam()
//     {
//         if (!isSoundFound) return;
//         
//         var triggerMin = bounds.min.y;
//         var triggerMax = bounds.max.y;
//         var objectY = transform.position.y;
//         var normalized = Mathf.InverseLerp(triggerMin, triggerMax, objectY);
//         var remapped = Mathf.Lerp(-1f, 1f, normalized);
//
//         if (Mathf.Abs(remapped - currentPitch) > 0.1f)
//         {
//             Tween.Custom(currentPitch, remapped, 0.5f,
//                 onValueChange: val =>
//                 {
//                     currentPitch = val;
//                     instance.setParameterByName("Volume", val);
//                 });
//         }
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
// }