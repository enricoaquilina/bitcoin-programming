using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            SHA1 mySHA1 = new SHA1CryptoServiceProvider();
            byte[] array = mySHA1.ComputeHash(new byte[2]);


            Key privateKey = new Key();
            PubKey publicKey = privateKey.PubKey;
            Console.WriteLine("The public key is " + publicKey);

            var pubKeyHash = publicKey.Hash;
            Console.WriteLine("\nPub Key Hash ");
            Console.WriteLine("\t" + pubKeyHash);
            var mainNetAddress = pubKeyHash.GetAddress(Network.Main);
            Console.WriteLine("Pub Key Hash Main Net Address");
            Console.WriteLine("\t" + mainNetAddress);
            Console.WriteLine("\nThe Main Net Address ScriptPubKey");
            Console.WriteLine("\t" + mainNetAddress.ScriptPubKey);

            //Generate the destination address from the ScriptPubKey
            var paymentScript = mainNetAddress.ScriptPubKey;
            var destAddress = paymentScript.GetDestinationAddress(Network.Main);
            Console.WriteLine("\nScriptPubKey Destination Address");
            Console.WriteLine("\t" + destAddress);
            Console.WriteLine("Main Net Address equal to ScriptPubKey Destination Address : " + (mainNetAddress == destAddress));

            //Generate a hash from the ScriptPubKey
            var generatedPubKeyHash = (KeyId) paymentScript.GetDestination();
            Console.WriteLine("\nGenerated PubKey Hash from ScriptPubKey");
            Console.WriteLine("\t" + generatedPubKeyHash);
            Console.WriteLine("Original PubKey matches with Generated Public Key : " + (pubKeyHash == generatedPubKeyHash));

            var MainNetAddress2 = new BitcoinPubKeyAddress(generatedPubKeyHash, Network.Main);
            Console.WriteLine("\nGenerated address from hash");
            Console.WriteLine("\t" + MainNetAddress2);
            Console.WriteLine("Main Net Address matches with Generated address : " + (mainNetAddress == MainNetAddress2));

            //Get the Bitcoin Secret from the the private key we generated up above
            BitcoinSecret mainNetPrivateKey = privateKey.GetBitcoinSecret(Network.Main);
            Console.WriteLine("\nMain Net Private Key");
            Console.WriteLine("\t" + mainNetPrivateKey);

            //Wallet Import Format(WIF) is the same as the Bitcoin Secret/Private key
            var wifBitcoinSecret = privateKey.GetWif(Network.Main);
            Console.WriteLine("Wallet Import Format(WIF) Bitcoin Secret");
            Console.WriteLine("\t" + wifBitcoinSecret);
            Console.WriteLine("Private Key equals Main Net Private Key : " + (mainNetPrivateKey == wifBitcoinSecret));
             
            //Console.WriteLine("Pub Key Test Address " + publicKey.GetAddress(Network.TestNet));
            //var testNetAddress = pubKeyHash.GetAddress(Network.TestNet);
            //Console.WriteLine("Test Net Hash Address is " + testNetAddress);
            //Console.WriteLine("The Test Net ScriptPubKey is " + testNetAddress.ScriptPubKey);

            Console.ReadLine();
        }
    }
}
