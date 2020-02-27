using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;
using TaskGrainInterfaces;

namespace Client
{
    class Program
    {
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            var taskGrainKey = Guid.Empty;
            var task = client.GetGrain<ITaskGrain>(taskGrainKey);
            var taskId = "1-2-3-4";
            var response = await task.GetTaskName(taskId);
            Console.WriteLine($"TaskId={taskId}, TaskName={response}");

            var response1 = await task.GetTaskName();
            Console.WriteLine($"TaskName={response1}");

            var group = client.GetGrain<ITaskGroupGrain>(Guid.NewGuid());
            await group.AddTask(taskGrainKey);

            // Observe
            var friend = client.GetGrain<INotifyGrain>(Guid.Empty);
            Chat c = new Chat();
            var obj = await client.CreateObjectReference<IChat>(c);
            await friend.Subscribe(obj);

            var isOkGrain = client.GetGrain<IEverythingIsOkGrain>(Guid.NewGuid());
            await isOkGrain.Start();
        }
    }
}
