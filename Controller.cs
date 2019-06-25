using System;
using BasisMeasurementPattern;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace Quantum.BasisMeasurementPattern
{
    class Driver
    {
        static void Main(string[] args)
        {
            using (var qsim = new QuantumSimulator())
            {

                Client Alice = new Client();
                Client Bob = new Client();
                Console.WriteLine("Creating Secret Basis Measurement Pattern Between Alice and Bob...");

                IQArray<bool> AlicePattern = new QArray<bool>();
                IQArray<bool> BobPattern = new QArray<bool>();
                //create secret pattern
                (AlicePattern, BobPattern) = createBasisMeasurementPatternFromSender.Run(qsim).Result;
                Alice.basisMeassurementPattern = AlicePattern.ToArray();
                Bob.basisMeassurementPattern = BobPattern.ToArray();
                Console.WriteLine("Representing Alice Pattern");
                Alice.representPattern();

                Console.WriteLine("Representing Bob Pattern");
                Bob.representPattern();
                Console.WriteLine("Both Patterns Should Match");

                Console.WriteLine("Now And Bob Share Secret Pattern!, Alice want to send Bob a secret message using BMP");
                Console.WriteLine("Suggest any message!");

                Alice.message = Console.ReadLine();
                Alice.convertMessageToBitString();
                //running the basis code
                Alice.encode();
                Bob.decode(Alice.encodedMessage);
                Random rnd = new Random();
                int r=0;
                for (int k=0; k < (Alice.messageInBits .Length/ 8); ++k) {
                   r = rnd.Next(1, 13);
                    if (k % 2 == 1) {

                        Console.WriteLine("after sending the "+k+ "th character the controller pair of qubits entangled with  Bob measuered in 1 the pattern become");
                        for (int l = 0; l < 8; l++)
                        {


                            if (Alice.basisMeassurementPattern[l] == true)
                            {
                                Alice.basisMeassurementPattern[l] = false;
                            }
                            else {
                                Alice.basisMeassurementPattern[l] = true;
                            }
                            Bob.basisMeassurementPattern[l] = Alice.basisMeassurementPattern[l];




                        }
                        Console.WriteLine("new pattern for Alice and Bob!");
                        Alice.representPattern();
                        Bob.representPattern();
                    }
                }
                 
                if (r % 2 == 1) {
                    //flipp
                    Console.Out.Write("The pattern will be flipped");

                }

                Console.WriteLine("Decoded Message From Bob Is");
                Console.WriteLine(Bob.decodedMessage);
                HelloQ.Run(qsim).Wait();
                var estimator = new ResourcesEstimator();
                //System.Console.WriteLine(estimator.ToTSV());
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }

            
        }
    }
}