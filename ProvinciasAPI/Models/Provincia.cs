namespace ProvinciasAPI.Models
{
    public class Centroide
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class Provincia
    {
        public string Nombre { get; set; }
        public Centroide Centroide { get; set; }
    }

    public class ProvinciaResponse
    {
        public int Cantidad { get; set; }
        public int Inicio { get; set; }
        public object Parametros { get; set; }
        public List<Provincia> Provincias { get; set; }
        public int Total { get; set; }
    }
}
