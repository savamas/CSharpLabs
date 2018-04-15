using System.ServiceModel;

namespace PolynomialWcfLib
{
	[ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
	public interface IPolynomial
	{
		[OperationContract]
		string GetPolynomialSolution(string equation);
	}
}
