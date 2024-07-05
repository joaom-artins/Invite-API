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
        public static readonly string PersonsInRequestInvalid = "O número de pessoas informadas no campo pessoas na família é diferente do número de pessoas informado!";
    }

    public static class Person
    {
        public static readonly string NotFound = "Pessoa não encontrado";
        public static readonly string PersonsEmpty = "Não existe nenhuma pessoa relacionada a esse responsável";
    }
}
