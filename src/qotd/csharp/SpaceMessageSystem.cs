using System;
using System.Numerics;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Space Message System:
    /// You are designing a messaging system for a future space craft that will travel to the far reaches of our Galaxy.
    /// Messages will be sent back to Earth from the space craft and recipients on Earth can send replies back to the space craft that sent the message.
    ///
    /// After the user sends a message to Earth, they need to be given a good approximation of when they can expect to receive a response to their message.
    ///
    /// Write the program that will be responsible for outputting this approximation to the user on the space craft.
    ///
    /// Inputs:
    /// - The distance from earth (in miles) at the time the message is sent
    /// - The velocity (in miles/hour) at the time the message is sent.
    ///
    /// Output:
    /// - The total time (i.e. years, days, hours, minutes, seconds) that the user can expect a response.
    ///
    /// Assumptions:
    /// - The space craft is traveling in a straight line and at some constant speed for the entire duration of the message cycle.
    /// - The messages travel at the speed of light (let's just set it at 186,000 miles/second)
    /// - When a message is received on Earth, assume the time taken to send the response in negligible
    /// - The recipient on earth always responds to messages from the space craft
    /// - The space craft can travel very close to the speed of light
    /// - No wormholes or warped space
    /// </summary>
    public class SpaceMessageSystem {
        public static void Init (string[] args) {
            GetMessageDeliveryTimespan (5878499810000, 1);
            
            GetMessageDeliveryTimespan (99999999999999, 185000 * (60 * 60));
        }

        private static string GetMessageDeliveryTimespan (BigInteger d, BigInteger v) {
            BigInteger result = 0;

            BigInteger vm = 186000 * (60 * 60);

            if (v < vm) {
                BigInteger t = d / vm;

                BigInteger d2 = t * v;
                BigInteger d3 = ((d + d2) * v) / (vm - v);

                result = (d2 + d3) / v;
            }

            return PrintTotalTime (d, v, result);
        }

        private static string PrintTotalTime (BigInteger distance, BigInteger velocity, BigInteger totalTime) {
            string result = string.Empty;

            totalTime = totalTime * 60 * 60;

            BigInteger remainder = 0;

            BigInteger years = totalTime / (60 * 60 * 24 * 365);
            remainder = totalTime % (60 * 60 * 24 * 365);
            BigInteger days = remainder / (60 * 60 * 24);
            remainder = totalTime % (60 * 60 * 24);
            BigInteger hours = remainder / (60 * 60);
            remainder = totalTime % (60 * 60);
            BigInteger minutes = remainder / 60;
            remainder = remainder % 60;
            BigInteger seconds = remainder;

            result = string.Concat (years, " years ", days, " days ", hours, " hours ", minutes, " minutes ", seconds, " seconds.");

            Console.WriteLine ("Distance: " + distance + " miles, Velocity: " + velocity + " miles/hour -> Message will be delived in '" + result + "'");

            return result;
        }
    }
}