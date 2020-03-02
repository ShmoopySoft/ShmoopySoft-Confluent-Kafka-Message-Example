# ShmoopySoft Confluent Kafka Message Example

A Visual Studio 2019 solution written in C# to demonstrate sending messages to Confluent Kafka using the open source Confluent.Kafka client library for .NET.

For this solution to work, it is necessary to add a Confluent.Kafka reference to project.

## Getting Started

In order to send messages to Kafka, you must have a Confluent Kafka Server setup and configured with a Kafka Topic to write to. You will need its IP address and Port number to send messages.

### Running

1. Download the solution from our GitHub repository
2. Open the solution in Visual Studio 2019
3. Install the Confluent.Kafka NuGet Package: Install-Package Confluent.Kafka -Version 1.3.0
4. Edit the 'BootstrapServers' string constant with your Confluent Kafka Server IP address and Port Number (usually 9092)
5. Edit the 'KafkaTopic' string constant with your Kafka topic name
6. Click the Start button, or press F5

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* https://github.com/confluentinc/confluent-kafka-dotnet
