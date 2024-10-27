namespace Catalogo
{
    public class Vehiculo
    {
        //atributos
        string placa;
        int fabricante;
        string referencia;
        string color;
        double precio;
        int modelo;
        string foto;
        public Vehiculo() { }
        public Vehiculo(string placa, int fabricante, string referencia,
            string color, double precio, int modelo, string foto)
        {
            this.placa = placa;
            this.fabricante = fabricante;
            this.referencia = referencia;
            this.color = color;
            this.precio = precio;
            this.modelo = modelo;
            this.foto = foto;
        }

        public string Placa { get => placa; set => placa = value; }
        public int Fabricante { get => fabricante; set => fabricante = value; }
        public string Referencia { get => referencia; set => referencia = value; }
        public string Color { get => color; set => color = value; }
        public double Precio { get => precio; set => precio = value; }
        public int Modelo { get => modelo; set => modelo = value; }
        public string Foto { get => foto; set => foto = value; }
    }
}
