namespace UserApp.Service.Commons
{
    // Configuracion SMTP. Se llena desde appsettings.json -> seccion "Email",
    // y se puede sobreescribir por variables de entorno con el prefijo "Email__"
    // (ej: Email__Host, Email__Port, Email__User, Email__Password).
    public class EmailSetting
    {
        public string? Host { get; set; }
        public int Port { get; set; } = 587;
        public string? User { get; set; }
        public string? Password { get; set; }
        public string? From { get; set; }
        public string? FromName { get; set; }
        public bool EnableSsl { get; set; } = true;

        // Dominio permitido para los correos (ej: "@leyde.hn"). Si esta vacio no se valida el dominio.
        public string? AllowedDomain { get; set; }
    }
}
