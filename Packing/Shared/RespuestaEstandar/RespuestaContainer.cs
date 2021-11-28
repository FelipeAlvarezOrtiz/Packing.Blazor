using System;

namespace Packing.Shared.RespuestaEstandar
{
    public enum EstadosRespuesta : ushort
    {
        Correcto = 0,
        Incorrecto = 1,
        CorrectoConErrores = 2
    }

    public class RespuestaContainer<T>
    {
        public Guid IdRespuesta { get; set; }
        public EstadosRespuesta Estado { get; set; }
        public string MensajeHumano { get; set; }
        public string MensajeLog { get; set; } 
        public string ModuloOrigen { get; set; }
        public T Resultado { get; set; }
    }

    public static class GeneradorRespuestas<T>
    {
        public static RespuestaContainer<T> GenerarRespuestaCorrecta(string moduloOrigen,T result)
        {
            return new RespuestaContainer<T>
            {
                Estado = EstadosRespuesta.Correcto,
                IdRespuesta = Guid.NewGuid(),
                MensajeHumano = "Realizado correctamente",
                MensajeLog = "Todo ok",
                ModuloOrigen = moduloOrigen,
                Resultado = result
            };
        }

        public static RespuestaContainer<T> GenerarRespuestaIncorrecta(string mensajeHumano, string mensajelog, string moduloOrigen, T result)
        {
            return new RespuestaContainer<T>
            {
                Estado = EstadosRespuesta.Incorrecto,
                IdRespuesta = Guid.NewGuid(),
                MensajeHumano = mensajeHumano,
                MensajeLog = mensajelog,
                ModuloOrigen = moduloOrigen,
                Resultado = result
            };
        }
    }

}
