namespace UserApp.Service.Commons.CurrentUser
{
    /// <summary>
    /// Expone el usuario autenticado (el que viene en el JWT del frontend).
    /// Se usa para llenar los campos de auditoria (CreatedBy / UpdatedBy).
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Username del usuario autenticado. Devuelve "system" si no hay
        /// un usuario en el contexto (por ejemplo, procesos internos).
        /// </summary>
        string UserName { get; }
    }
}
