namespace Invite.Commons.Notifications;

public class NotificationMessage
{
    public static class Common
    {
        public static readonly string UnexpectedError = "Erro inesperado!";
        public static readonly string InvalidCPF = "CPF inválido!";
        public static readonly string ExistsCPF = "CPF já cadastrado!";
    }

    public static class Responsible
    {
        public static readonly string NotFound = "Responsável não encontrado";
    }
}
