namespace WeatherWiseApi.Code.Model;

/// <summary>
/// Usuário do Sistema
/// </summary>
public class User
{
    /// <summary>
    /// Identificação do Usuário
    /// </summary>
    public int id_user { get; set; }

    /// <summary>
    /// Nome de Usuário
    /// </summary>
    public string name_user { get; set; }

    /// <summary>
    /// Email do Usuário
    /// </summary>
    public string email_user { get; set; }

    /// <summary>
    /// Senha do Usuário
    /// </summary>
    public string password_user { get; set; }

    /// <summary>
    /// Tipo do Usuário
    /// </summary>
    public TypesUser type_user { get; set; }

    /// <summary>
    /// PROFILE IMAGE
    /// </summary>
    public string profile_image { get; set; }
}
