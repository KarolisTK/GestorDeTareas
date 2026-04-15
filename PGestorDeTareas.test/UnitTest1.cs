using GestorDeTareas;
using GestorDeTareas.DTOs;
using NUnit.Framework.Internal;

namespace PGestorDeTareas.test
{
    [TestFixture]
    public class GestorDeTareasCRUD
    {
        private TareaRepository repository;
        private TareaService _service;
        private Tarea _tarea;
        private CrearTareaDTO _tareaDTO;

        [SetUp]
        public void Setup()
        {
            repository = new TareaRepository("ruta_test.json");
            _service = new TareaService();
            _tarea = new Tarea
            {
                NombreTarea = "tarea",
                DescripcionTarea = "descripcion de tarea Urgente",
                FechaCreacionTarea = System.DateTime.Now,
                EstadoTarea = EstadoTarea.NoIniciada,
                EstaEliminado = false,
                TipoTarea = TipoTarea.Urgente
            };
            _tareaDTO = new CrearTareaDTO
            {
                NombreTarea = "tarea",
                DescripcionTarea = "descripcion de tarea Urgente",
                FechaCreacionTarea = System.DateTime.Now,
                EstadoTarea = EstadoTarea.NoIniciada,
                EstaEliminado = false,
                TipoTarea = TipoTarea.Urgente
            };
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists("ruta_test.json"))
                File.Delete("ruta_test.json");
        }
        [Test]
        public void CrearTresTareas_ComprobarQueSeHanCreadoLasTresTareas()
        {
            var lista = new List<Tarea>();

            var tarea1 = _service.MapearTarea(_tareaDTO);
            var tarea2 = _service.MapearTarea(_tareaDTO);
            var tarea3 = _service.MapearTarea(_tareaDTO);
            lista.Add(tarea1);
            lista.Add(tarea2);
            lista.Add(tarea3);
            Assert.That(lista.Count, Is.EqualTo(3));
        }

        [Test]

        public void EditarUnaTarea_()
        {
            var tareaEditada = new EditarTareaDTO
            {
                NombreTarea = "tarea Editada",
                DescripcionTarea = "descripcion de tarea Urgente Editada",
                EstadoTarea = EstadoTarea.NoIniciada,
                EstaEliminado = false,
                TipoTarea = TipoTarea.Urgente
            };
            var resultado = _service.MapearEdiccionTarea(_tarea, tareaEditada);
            Assert.That(resultado.NombreTarea, Is.EqualTo("tarea Editada"));

        }
    }
}
