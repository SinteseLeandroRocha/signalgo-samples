﻿using SignalGo.Server.Models;
using SignalGo.Shared.DataTypes;
using SignalGo.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SignalGoServerSample
{
    [ServiceContract("SignalGoTestService", ServiceType.ServerService, InstanceType.SingleInstance)]
    public class SignalGoServerMethods
    {
        public Tuple<bool> Login(string userName, string password)
        {
            Console.Write("client called Login: UserName:" + userName + " , Password:" + password);

            //broadcasting to clients
            //get all clients and call interface methods
            foreach (var call in SignalGo.Server.Models.OperationContext.Current.GetAllClientServicesButMe<ISignalGoClientServiceMethods>())
            {
                //call GetMeSignalGo method
                var result = call.GetMeSignalGo("test");

                Console.WriteLine("GetMeSignalGo result: " + result.Item1);

                //call HelloSignalGo method
                call.HelloSignalGo("hello signalGo");

                Console.WriteLine("hello signalGo call Success");
            }

            if (userName == "admin" && password == "admin")
                return new Tuple<bool>(true);
            return new Tuple<bool>(false);
        }
    }
}