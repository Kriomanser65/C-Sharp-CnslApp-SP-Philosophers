using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Philosophers
{
    class Philosopher
    {
        private readonly object leftFork;
        private readonly object rightFork;
        private readonly string name;

        public Philosopher(string name, object leftFork, object rightFork)
        {
            this.name = name;
            this.leftFork = leftFork;
            this.rightFork = rightFork;
        }

        public void Eat()
        {
            Console.WriteLine($"{name} start eating.");
            lock (leftFork)
            {
                Console.WriteLine($"{name} take left fork.");
                lock (rightFork)
                {
                    Console.WriteLine($"{name} take right fork and start eating.");
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"{name} put right fork.");
            }
            Console.WriteLine($"{name} put left forl and start thinking.");
        }

        public void Dine(int eatCount)
        {
            for (int i = 0; i < eatCount; i++)
            {
                Eat();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfPhilosophers = 5;
            List<object> forks = new List<object>();
            List<Philosopher> philosophers = new List<Philosopher>();
            for (int i = 0; i < numberOfPhilosophers; i++)
            {
                forks.Add(new object());
            }
            for (int i = 0; i < numberOfPhilosophers; i++)
            {
                philosophers.Add(new Philosopher($"Philosopher{i + 1}", forks[i], forks[(i + 1) % numberOfPhilosophers]));
            }
            List<Thread> threads = new List<Thread>();
            foreach (Philosopher philosopher in philosophers)
            {
                Thread thread = new Thread(() => philosopher.Dine(3));
                threads.Add(thread);
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }
    }
}