﻿using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Address _address;
    private readonly Email _email;
    private readonly Student _student;
    private readonly Subscription _subscription;
    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("56293721063", EDocumentType.CPF);
        _address = new Address("Rua 1", "71", "Bairro", "Gotham", "FL", "USA", "58076150");
        _email = new Email("batman@dc.com");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null); // vamos passa a ExpireDate como null.
    }
    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email);
        _subscription.AddPayment(payment);

        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);
        Assert.IsFalse(_student.IsValid);
    }
    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);
        Assert.IsFalse(_student.IsValid);
    }
    [TestMethod]
    public void ShouldReturnSucessWhenAddSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.IsValid);
    }
}