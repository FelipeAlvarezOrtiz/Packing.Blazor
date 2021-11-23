using MediatR;
using Microsoft.EntityFrameworkCore;
using Packing.Core.Productos;
using Packing.Persistencia;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Packing.Negocio.Grupos
{
    public class CrearGrupo
    {
        public record Command : IRequest
        {
            public string NombreGrupo { get; set; }
            public string FileName { get; set; }
            public byte[] ArchivoStream { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            private readonly IWebHostEnvironment _env;
            public Handler(ApplicationDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Grupos.Where(grupo => grupo.NombreGrupo.Equals(request.NombreGrupo))
                    .FirstOrDefaultAsync(cancellationToken) is not null)
                    throw new Exception("El grupo a ingresar ya existe.");
                if (request.ArchivoStream.Length <= 0) throw new Exception("El archivo no puede estar vacio");

                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/grupos/",
                //    request.ArchivoStream.Name);
                //if (File.Exists(filePath))
                //    throw new Exception("Ya existe un archivo guardado con ese nombre asociado a otro grupo.");
                var pathCliente = _env.ContentRootPath;
                
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/grupos/");

                var fs = File.Create(Path.Combine(path, request.FileName));

                await fs.WriteAsync(request.ArchivoStream.AsMemory(0, request.ArchivoStream.Length), cancellationToken);

                fs.Close();

                //await using (var stream = System.IO.File.Create(filePath))
                //{
                //    await request.ArchivoStream.OpenReadStream().CopyToAsync(stream, cancellationToken);
                //}

                await _context.Grupos.AddAsync(new GrupoProducto()
                {
                    NombreGrupo = request.NombreGrupo,
                    Imagen = request.NombreGrupo
                }, cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken) > 0
                    ? Unit.Value
                    : throw new Exception("Ha ocurrido un problema al guardar el grupo.");
            }
        }
    }
}
