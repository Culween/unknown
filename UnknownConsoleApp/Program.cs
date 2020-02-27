using Microsoft.Extensions.DependencyInjection;
using System;

namespace UnknownConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar>(new Bar())
                .AddSingleton<IBaz>(_ => new Baz())
                .AddSingleton<IQux, Qux>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("serviceProvider.GetService<IFoo>(): {0}", serviceProvider.GetService<IFoo>());
            Console.WriteLine("serviceProvider.GetService<IBar>(): {0}", serviceProvider.GetService<IBar>());
            Console.WriteLine("serviceProvider.GetService<IBaz>(): {0}", serviceProvider.GetService<IBaz>());
            Console.WriteLine("serviceProvider.GetService<IGux>(): {0}", serviceProvider.GetService<IQux>());

            IServiceCollection services1 = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient(typeof(IFooBar<,>), typeof(FooBar<,>));

            IServiceProvider serviceProvider1 = services1.BuildServiceProvider();
            Console.WriteLine("serviceProvider.GetService<IFoobar<IFoo, IBar>>().Foo: {0}", serviceProvider1.GetService<IFooBar<IFoo, IBar>>().Foo);
            Console.WriteLine("serviceProvider.GetService<IFoobar<IFoo, IBar>>().Bar: {0}", serviceProvider1.GetService<IFooBar<IFoo, IBar>>().Bar);
        }
    }
}
