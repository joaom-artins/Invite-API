namespace Invite.Commons.Notifications;

public class NotificationMessage
{
    public static class Common
    {
        public static readonly string UnexpectedError = "Erro inesperado!";
        public static readonly string InvalidCPF = "CPF inválido!";
        public static readonly string ValidationError = "Ocorreram um ou mais erros de validação!";
        public static readonly string RequestListRequired = "Lista da requisição não pode estar vazia!";
    }

    public static class Responsible
    {
        public static readonly string NotFound = "Responsável não encontrado";
        public static readonly string PersonsInRequestInvalid = "O número de pessoas informadas no campo pessoas na família é diferente do número de pessoas informado!";
        public static readonly string ExistsCPF = "CPF já cadastrado!";
    }

    public static class Person
    {
        public static readonly string NotFound = "Pessoa não encontrado";
        public static readonly string PersonsEmpty = "Não existe nenhuma pessoa relacionada a esse responsável";
        public static readonly string ExistsCPF = "CPF já cadastrado!";
    }

    public static class User
    {
        public static readonly string EmailExists = "Esse email já está cadastrado!";
        public static readonly string ExistsCPF = "CPF já cadastrado!";
        public static readonly string DifferentPasswords = "As senhas informadas são diferentes!";
        public static readonly string ErrorInCreate = "Ocorreu um erro inesperado ao criar o usuário,tente novamente por favor!";
        public static readonly string NotFound = "Usuário não encontrado!";
        public static readonly string ErrorInAddRole = "Erro ao adicionar Role em usuário!";
        public static readonly string InvaliData = "Dados incorretos!";
    }
}
