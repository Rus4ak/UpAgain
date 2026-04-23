// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ttyTd55vPzFAyDHC7Zaq2SLhJStCfhneWNvThHK5MhA2R5UwVVzIrfzXdM2ryixgWY11cpvT0VksqyFBZOB+dkd1KV38Bt4HGbAZUFBvf8CHUKVTXHed8OcId5atKo+j94UqeffrXMzbenMs0eDSPjOxJ0PJ2ToN5hJ/VUKLCcayT88iB1CSGLbakaN7yUppe0ZNQmHNA828RkpKSk5LSMpoVmaNfC95p5DNF2jgMYgl6k4a7df0B/rCI8CSjRz/oSbyZt5weuYCwIVyhHKEXvMuD7F6Jv+Gawx95PK6fn0Sym2Y5oAgd0FyCXmqzIB5AIm7o5vj8g1etkaf6pCg1b2c96TJSkRLe8lKQUnJSkpLiwGvUXmMbJXUQxDLeR2FWElISktK");
        private static int[] order = new int[] { 2,3,12,13,7,5,7,12,8,12,12,11,12,13,14 };
        private static int key = 75;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
