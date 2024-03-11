using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class DocumentTestes
{
    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInInvalid()
    {   
        var doc = new Document("123", EDocumentType.CNPJ);
        Assert.IsFalse(doc.IsValid);
    }
    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInValid()
    {

        var doc = new Document("45904608000189", EDocumentType.CNPJ);
        Assert.IsTrue(doc.IsValid);
    }
    [TestMethod]
    public void ShouldReturnErrorWhenCPFIsInInvalid()
    {
        var doc = new Document("1234", EDocumentType.CPF);
        Assert.IsFalse(doc.IsValid);
    }
    [TestMethod]
    [DataTestMethod]
    [DataRow("61611788005")]
    [DataRow("62159598046")]
    [DataRow("43254928016")]
    [DataRow("37766564092")]
    public void ShouldReturnErrorWhenCPFIsInValid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsTrue(doc.IsValid);
    }
}