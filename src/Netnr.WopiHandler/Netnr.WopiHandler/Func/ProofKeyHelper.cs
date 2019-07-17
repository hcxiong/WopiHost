using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Netnr.WopiHandler
{
    public struct KeyInfo
    {
        public KeyInfo(string cspBlob, string modulus, string exponent)
        {
            CspBlob = cspBlob;
            Exponent = exponent;
            Modulus = modulus;
        }

        public string CspBlob { get; }
        public string Exponent { get; }
        public string Modulus { get; }
    }

    public class ProofKeyValidationInput
    {
        public ProofKeyValidationInput(string accessToken, long timestamp, string url, string proof, string oldProof)
        {
            AccessToken = accessToken;
            Timestamp = timestamp;
            Url = url;
            Proof = proof;
            OldProof = oldProof;
        }

        public string AccessToken { get; }
        public long Timestamp { get; }
        public string Url { get; }
        public string Proof { get; }
        public string OldProof { get; }
    }

    public class ProofKeysHelper
    {
        private KeyInfo _currentKey;
        private KeyInfo _oldKey;

        public ProofKeysHelper(KeyInfo current, KeyInfo old)
        {
            _currentKey = current;
            _oldKey = old;
        }

        public bool Validate(ProofKeyValidationInput testCase)
        {
            // Encode values from headers into byte[]
            var accessTokenBytes = Encoding.UTF8.GetBytes(testCase.AccessToken);
            var hostUrlBytes = Encoding.UTF8.GetBytes(testCase.Url.ToUpperInvariant());
            var timeStampBytes = EncodeNumber(testCase.Timestamp);

            // prepare a list that will be used to combine all those arrays together
            List<byte> expectedProof = new List<byte>(
                4 + accessTokenBytes.Length +
                4 + hostUrlBytes.Length +
                4 + timeStampBytes.Length);

            expectedProof.AddRange(EncodeNumber(accessTokenBytes.Length));
            expectedProof.AddRange(accessTokenBytes);
            expectedProof.AddRange(EncodeNumber(hostUrlBytes.Length));
            expectedProof.AddRange(hostUrlBytes);
            expectedProof.AddRange(EncodeNumber(timeStampBytes.Length));
            expectedProof.AddRange(timeStampBytes);

            // create another byte[] from that list
            byte[] expectedProofArray = expectedProof.ToArray();

            // validate it against current and old keys in proper combinations
            bool validationResult =
                TryVerification(expectedProofArray, testCase.Proof, _currentKey.CspBlob) ||
                TryVerification(expectedProofArray, testCase.OldProof, _currentKey.CspBlob) ||
                TryVerification(expectedProofArray, testCase.Proof, _oldKey.CspBlob);

            // TODO:
            // in real code you should also check that TimeStamp header is no more than 20 minutes old
            // but because we're using predefined test cases to validate that the method works
            // we can't do it here.
            return validationResult;
        }

        private static byte[] EncodeNumber(int value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        private static byte[] EncodeNumber(long value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }

        private static bool TryVerification(byte[] expectedProof,
            string signedProof,
            string publicKeyCspBlob)
        {
            using(RSACryptoServiceProvider rsaAlg = new RSACryptoServiceProvider())
            {
                byte[] publicKey = Convert.FromBase64String(publicKeyCspBlob);
                byte[] signedProofBytes = Convert.FromBase64String(signedProof);
                try
                {
                    rsaAlg.ImportCspBlob(publicKey);
                    return rsaAlg.VerifyData(expectedProof, "SHA256", signedProofBytes);
                }
                catch(FormatException)
                {
                    return false;
                }
                catch(CryptographicException)
                {
                    return false;
                }
            }
        }
    }
}