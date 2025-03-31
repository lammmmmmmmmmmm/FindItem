using UnityEngine;

namespace Item {
    public static class LayerMaskExtension {
        public static bool Contains(this LayerMask mask, int layer) {
            return mask == (mask | (1 << layer));
        }

        public static LayerMask AddLayerMasks(this LayerMask mask, LayerMask maskB) {
            return mask | maskB; // Bitwise OR combines both LayerMasks
        }

        public static LayerMask RemoveLayerMasks(this LayerMask mask, LayerMask maskB) {
            return mask ^ maskB;
        }
    }
}