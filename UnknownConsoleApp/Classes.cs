using System;
using System.Collections.Generic;
using System.Text;

namespace UnknownConsoleApp
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IQux
    {
        IFoo Foo
        {
            get;
        }

        IBar Bar { get; }

        IBaz Baz { get; }
    }
    public interface IFooBar<T1, T2>
    {
        T1 Foo { get; }
        T2 Bar { get; }
    }

    public class Foo : IFoo
    {
        public Foo()
        {
            Console.Write("I'm Foo");
        }
    }
    public class Bar : IBar
    {
        public Bar()
        {
            Console.WriteLine("I'm Bar");
        }
    }
    public class Baz : IBaz
    {
        public Baz()
        {
            Console.WriteLine("I'm Baz");
        }
    }
    public class Qux : IQux
    {
        public IFoo Foo { get; private set; }
        public IBar Bar { get; private set; }
        public IBaz Baz { get; private set; }

        public Qux(IFoo foo, IBar bar, IBaz baz)
        {
            this.Foo = foo;
            this.Bar = bar;
            this.Baz = baz;
            Console.WriteLine("I'm Qux");
        }
    }

    public class FooBar<T1,T2> : IFooBar<T1, T2>
    {
        public T1 Foo { get; private set; }

        public T2 Bar { get; private set; }

        public FooBar(T1 foo,T2 bar)
        {
            this.Foo = foo;
            this.Bar = bar;
        }
    }
}
