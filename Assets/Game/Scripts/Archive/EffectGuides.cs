// using UnityEngine;
// using Shapes;
// using UnityEngine.Rendering;
//
// [ExecuteAlways]
// public class EffectGuides : ImmediateModeShapeDrawer
// {
//     [SerializeField] EffectItem effect;
//
//     private bool trigger;
//
//     public override void DrawShapes(Camera cam)
//     {
//         if (cam == null) return;
//         if (!trigger) return;
//
//         using (Draw.Command(cam))
//         {
//             Draw.ZTest = CompareFunction.Always;
//             
//             float thickness = 0.001f;
//             float radius = 0.05f;
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
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (!other.CompareTag("Ring"))
//             return;
//
//         trigger = true;
//     }
//
//     private void OnTriggerExit(Collider other)
//     {
//         if (!other.CompareTag("Ring"))
//             return;
//
//         trigger = false;
//     }
// }