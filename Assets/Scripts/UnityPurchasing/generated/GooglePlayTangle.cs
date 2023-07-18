// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("A0F8cAhc6IdciN4aJVCG/NfCUGFURsKlcgB0unmm0XnJcHBx4zoeFNzturyazUCn3CawtUW531WnRUaHmtyzhkfCUfUifmp48Cr+G6uOGM+9dMFUIMzWyR2QGwYBmS8Bej7QfxDfrNY9jlrqoD9cnGChGhHe+arsglzOOan92mwl8ttHkGcQ4nlOUrsh268JmT1EnhoGX6CxbFsDuVrKpY89vp2Psrm2lTn3OUiyvr6+ur+8oSc7kWpL8Hj4Z8FxA/1UobN0so09Zu/+yLY1qYga+H3FRcOA3nVTtq157kTVjaa8XQu8KtpwVMADf8B2Pb6wv489vrW9Pb6+vz1nK1otrMOvsQTVQbsJ65tQkbi8fi//NHzHInl2xhOW7VurGL28vr++");
        private static int[] order = new int[] { 5,11,12,3,8,6,9,7,9,13,12,12,12,13,14 };
        private static int key = 191;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
