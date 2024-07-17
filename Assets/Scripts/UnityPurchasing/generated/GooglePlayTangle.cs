// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("mnxKqXU2H6rQxzvyoPpZJh1kGkYmmaMQ0+nZVwmiJw4j5kQFRyGOQ3TGRWZ0SUJNbsIMwrNJRUVFQURHBkTcCreaK0WBQGWiPMfm2fpFNjqnqE8CzPqt3B/4tKnlmhOO/tn9bumbqpx1+X/f9wIzRTjkUdx+NRBRwRhAC2Z6SWjZOTBQg7EUmuL3m6OmfmWi4zMpbom16coXJflfjfg1d+vrGJQEJeMDxeA2vsWfwHPOeY+k61P4WbbUYuGBUwfW2zu3FO1BbbXGRUtEdMZFTkbGRUVEgYduhfD8A1Xpsb1LLRwv+BhVkhcruR5d86SoOnllZsAnHudQqS6n5uyuuQhFdU5/By1XppcDXOMsHzo+fvjSuV5ERRdMDUlhX17SDUZHRURF");
        private static int[] order = new int[] { 11,10,11,9,12,8,9,13,12,9,10,11,13,13,14 };
        private static int key = 68;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
