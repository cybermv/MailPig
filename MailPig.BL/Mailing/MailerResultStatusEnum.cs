namespace MailPig.BL.Mailing
{
    using System;

    [Flags]
    public enum MailerResultStatusEnum
    {
        Succeeded = 1,
        InvalidSender = 2,
        InvalidRecipient = 4,
        Exception = 8
    }
}