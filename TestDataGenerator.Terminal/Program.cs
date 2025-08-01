using TestDataGenerator.Core;

var isRunning = true;

IServiceCollection serviceCollection = new ServiceCollection();

serviceCollection.AddTestDataGeneration();

IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

Console.WriteLine("Type 'Type:Length' to generate test data");
Console.WriteLine("Known Types:");
Console.WriteLine(string.Join(", ", serviceProvider.GetService<ITestDataTypeCollection>().Value));
Console.WriteLine();
Console.WriteLine("Type 'exit' to close the application.");

while (isRunning)
{
    Console.Write("> ");

    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    var parts = input.Split(':', StringSplitOptions.RemoveEmptyEntries);
    var command = parts[0].ToLower();

    switch (command)
    {
        case "exit":
            // The 'exit' command.
            Console.WriteLine("Exiting application. Goodbye!");
            isRunning = false;
            break;

        default:
            serviceProvider.GetService<ITestDataType>().Value = parts[0];

            if (parts.Length < 2 || !int.TryParse(parts[1], out var length))
            {
                Console.WriteLine("Invalid input. Please use the format 'Type:Length'.");
                continue;
            }

            serviceProvider.GetService<ITestDataLength>().Value = length;
            Console.WriteLine(serviceProvider.GetService<ITestData>().Value);
            break;
    }
}