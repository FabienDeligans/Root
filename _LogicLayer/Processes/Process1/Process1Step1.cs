﻿using _Providers.DatabaseProviders.MongoDb;
using Back._LogicLayer.Processes;
using Common.Models.Processes;

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
