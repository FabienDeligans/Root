﻿using _Providers.DatabaseProviders.MongoDb;
using Library._LogicLayer.Processes;
using Library._LogicLayer.Processes.Models;

namespace _LogicLayer.Processes.Process1
{
    public class Process1Step1 : AbstractProcessStep
    {
        public Process1Step1(
            Process1Step2 process1Step2,
            ServiceMongoDatabase serviceMongoDatabase) 
            : base(serviceMongoDatabase)
        {
            SetCurrentStep(_Steps.Step1); 
            SetNext(process1Step2);
        }

        public override void Run(Process? processToUpdate)
        {
            //DO
            Console.WriteLine($"Process1 step1");
        }
    }
}
