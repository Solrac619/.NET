using MediatR;
using ApplicationCore.Wrappers;
using System;

public class ColaboradorCreateCommand : IRequest<Response<int>>
{
    public int? Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsProfesor { get; set; }

    
}
