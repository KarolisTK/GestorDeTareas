using GestorDeTareas;

class Program
{
    static void Main()
    {
        var gestor = new ListadoTareasSimples(
            0, "", "", DateTime.Now,
            EstadoTarea.NoIniciada, false, false, TipoTareaSimple.Base
        );

        gestor.EditarTarea(2, "EDITADO", "Nueva descripción", EstadoTarea.finalizada);

        var lista = ListadoTareasSimples.ObtenerTareasDePrueba();

        foreach (var campo in lista)
        {
            Console.WriteLine($"{campo.IdTarea} - {campo.NombreTarea} - {campo.DescripcionTarea} - {campo.EstadoTarea} - {campo.EstaEliminado} - {campo.TipoTareaSimple}");
        }
    }
}