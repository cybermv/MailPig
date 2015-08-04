namespace MailPig.BL.Mailing
{
    using System;

    public class MailerResult
    {
        public MailerResultStatusEnum Status { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public MailerResult()
            : this(MailerResultStatusEnum.Succeeded, string.Empty, null)
        {
        }

        public MailerResult(MailerResultStatusEnum status, string message, Exception exception)
        {
            Status = status;
            Message = message;
            Exception = exception;
        }

        public bool IsValid { get { return Status == MailerResultStatusEnum.Succeeded; } }
    }
}