using System.ComponentModel;

namespace Infrastructure.CrossCutting.Helpers.Enums
{
    public enum ResponseEnum
    {
        [Description("Insucesso")]
        Insucesso = -1,

        [Description("Ok")]
        Sucesso = 1,

        [Description("Erro de sistema")]
        ErroDeSistema = 999
    }
}
