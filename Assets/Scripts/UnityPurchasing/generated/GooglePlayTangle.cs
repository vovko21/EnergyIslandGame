// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("EemkqHhiuJGcoifPCO4ejYduq6xEZJUu9+pJdAdo6Ve52ZF09lfrgQ+YLHFYz0m4TehOQ5U32L17FMvAuwdqBje1mPv3LP8jbUJr4uG1ZD7ylV6jvrg5nN1tt4cCjaIom3ZHleRnaWZW5GdsZORnZ2bn/gqo7n+gVuRnRFZrYG9M4C7gkWtnZ2djZmXmwfbiO6wg/TN9UMZwF1dCekgRmVElHkC/a/znYkKv1Pzx7bZ8z36DTZO6qGK88HZSWGBDhke/ohN9NvaIm7akUNOmrxQv+0EfD6/9rKmIygLfu1tcCiIsdQusudXmeY+uPdJWbzRuw75NTdem9OxaWTLhhmLgZku0aALEum0B6moU/p/3U28JAu12hqX+qCc9ahnHt2RlZ2Zn");
        private static int[] order = new int[] { 7,12,6,5,10,12,7,13,9,11,10,12,12,13,14 };
        private static int key = 102;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
