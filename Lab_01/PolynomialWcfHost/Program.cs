using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using PolynomialWcfLib;

namespace PolynomialWcfHost
{
	class Program
	{
		static void Main(string[] args)
		{
			Uri baseAddress = new Uri("net.tcp://localhost:8013/Services");

			ServiceHost selfHost = new ServiceHost(typeof(PolynomialWcfService), baseAddress);

			try
			{

				selfHost.AddServiceEndpoint(typeof(IPolynomial), new NetTcpBinding(), "PolynomialService");

				ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = false;

				selfHost.Description.Behaviors.Add(smb);

				var mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
				selfHost.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, "mex");

				selfHost.Open();
				Console.WriteLine("The service is ready!");
				Console.WriteLine("Press <ENTER> to terminate service.");
				Console.WriteLine();
				Console.ReadLine();

				selfHost.Close();
			}
			catch (CommunicationException ce)
			{
				Console.WriteLine($"An exeption occured: {ce.Message}");
				Console.ReadLine();
				selfHost.Abort();
			}
		}
	
	}
}
