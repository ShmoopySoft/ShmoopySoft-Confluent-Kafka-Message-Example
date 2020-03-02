/*
 * MIT License
 * 
 * Copyright(c) 2020 ShmoopySoft (Pty) Ltd
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO 
 * EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE 
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

//// REQUIRED: Confluent.Kafka 
//// NuGet Package Manager Command:
//// Install-Package Confluent.Kafka -Version 1.3.0

using Confluent.Kafka;
using System;

namespace ShmoopySoft_Confluent_Kafka_Message_Example
{
    /// <summary>
    /// The Program class's responsibility is to provide an entry point for the application.
    /// </summary>
    class Program
    {
        private const int MessageTimeout = 30000;                       // 30 Seconds
        private const string BootstrapServers = "127.0.0.1:9092";       // <<<!!! INSERT YOUR KAFKA BOOTSTRAP SERVER IP ADDRESSES AND PORTS HERE !!!>>>
        private const string KafkaTopic = "My Topic";                   // <<<!!! INSERT YOUR KAFKA TOPIC NAME HERE !!!>>>

        /// <summary>
        /// C# applications have an entry point called the Main Method. 
        /// It is the first method that gets invoked when an application starts.
        /// </summary>
        /// <param name="args">Command line arguments as string type parameters</param>
        static void Main(string[] args)
        {
            try
            {
                //// Set the message to send.
                string messageToSend = "Test message at " + DateTime.Now;

                //// Display progress.
                Console.WriteLine("Sending Kafka message: " + messageToSend);

                //// Create a new ProducerConfig.
                ProducerConfig producerConfig = new ProducerConfig
                {
                    BootstrapServers = BootstrapServers,
                    MessageTimeoutMs = MessageTimeout,
                };

                //// Create a new ProducerBuilder with the ProducerConfig defined.
                //// The using block ensures the ProducerBuilder is automatically closed.
                using (var producer = new ProducerBuilder<Null, string>(producerConfig)
                    .SetKeySerializer(Serializers.Null)
                    .SetValueSerializer(Serializers.Utf8)
                    .Build())
                {
                    //// Send the message and await the response.
                    //// Note the use of GetAwaiter().GetResult(); this avoids the AggregateException wrapping 
                    //// that happens if you use Wait() or Result.
                    var result = 
                        producer.ProduceAsync(KafkaTopic, new Message<Null, string> { Value = messageToSend }).GetAwaiter().GetResult();

                    //// Display a confirmation.
                    Console.WriteLine("The message was successfully sent to Kafka :-)");
                }
            }
            catch (ProduceException<string, string> pe)
            {
                //// Display an error.
                Console.WriteLine("Failed to send the message to Kafka :-(");
                Console.Write(Environment.NewLine);
                Console.WriteLine(pe.ToString());
            }
            catch (Exception ex)
            {
                //// Display an error.
                Console.WriteLine("Failed to send the message to Kafka :-(");
                Console.Write(Environment.NewLine);
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey(true);
        }
    }
}