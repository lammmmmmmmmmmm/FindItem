// using UnityEngine;
//
// namespace Bot.Entities.Monster {
//     public class TargetDetectorWithTrigger : MonoBehaviour {
//         public IDie TargetDie { get; private set; }
//
//         public Transform TargetTransform { get; private set; }
//
//         private void OnTriggerEnter2D(Collider2D other) {
//             if (TargetDie != null)
//                 return;
//
//             if (other.TryGetComponent<IDie>(out var iDie)) {
//                 TargetDie = iDie;
//                 TargetTransform = other.transform;
//             }
//         }
//
//         private void OnTriggerExit2D(Collider2D other) {
//             if (TargetDie == null)
//                 return;
//
//             if (other.GetComponent<IDie>() == TargetDie) {
//                 RemoveTarget();
//             }
//         }
//
//         public float TargetDistance() {
//             return Vector2.Distance(transform.position, TargetTransform.position);
//         }
//
//         private void RemoveTarget() {
//             TargetDie = null;
//             TargetTransform = null;
//         }
//     }
// }