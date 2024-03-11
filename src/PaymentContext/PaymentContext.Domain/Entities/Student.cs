using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscription; // cria List private de assinaturas
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscription = new List<Subscription>();// add no ctor

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }           // coloca no get _subscription.ToArray();
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscription.ToArray(); } }
        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;
            foreach (var sub in _subscription)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Esta assinatura não possui pagamento")
                );

            //Alternativa
            //if (hasSubscriptionActive)
            //    AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa");

            // Verifica se o estudante já possui uma assinatura ativa

        }
    }
}