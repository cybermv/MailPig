namespace MailPig.BL.Mailing
{
    public class MailPigEmail
    {
        public bool SentSuccessfully { get; private set; }

        public int RecipientId { get; set; }

        public int EmailId { get; set; }

        public string Sender { get; private set; }

        public string Recipient { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public MailPigEmail(int emailId, int recipientId, string sender, string recipient, string subject, string body)
        {
            SentSuccessfully = false;
            EmailId = emailId;
            RecipientId = recipientId;
            Sender = sender;
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }

        public void SendSuceeded()
        {
            this.SentSuccessfully = true;
        }
    }
}