using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Curp { get; set; }

    public byte? IdRol { get; set; }

    public int? IdUsuarioModificado { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public byte[]? Imagen { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Estatus { get; set; }

    public int? Edad { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual Usuario? IdUsuarioModificadoNavigation { get; set; }

    public virtual ICollection<Usuario> InverseIdUsuarioModificadoNavigation { get; set; } = new List<Usuario>();
}
