using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace Catalogo
{
    internal class GestionDB
    {
        // Definicion de los atributos
        SQLiteConnection conn; //Conectar BD
        SQLiteCommand cmd; //Comandos
        SQLiteDataReader reader; //Leer de resultados de comandos
        string nombreDB = "AutosDB.sqlite"; //Nombre BD
        string mensaje = string.Empty; // Mensaje definido como vacio

        public string Mensaje { get => mensaje; }

        //Metoodo para conectar la bd 
        void Conectar()
        {
            try
            {
                conn = new SQLiteConnection(string.Format("Data source={0}; version=3", nombreDB));
                conn.Open();
            }
            catch (Exception ex)
            {
                mensaje = "No fue posible establecer conexión por: " + ex.Message;
            }
        }

        // Metodo AgregarVehiculo
        public int AgregarVehiculo(Vehiculo vehiculo)
        {
            int respuesta = 0; //si la respuesta es 0 es porque no funciono 
            try
            {
                Conectar(); // se conecta a la base y se pasa el comando 
                string query = "INSERT INTO Vehiculo (placa, idFabricante, Referencia, precio, modelo, foto, color) " +
                    "VALUES (@placa, @idFabricante, @Referencia, @precio, @modelo, @foto, @color)";
                cmd = new SQLiteCommand(query, conn); // Inicializar  comando 
                // se pasa cada valor a la variable temporal
                cmd.Parameters.AddWithValue("@placa", vehiculo.Placa);
                cmd.Parameters.AddWithValue("@idFabricante", vehiculo.Fabricante);
                cmd.Parameters.AddWithValue("@Referencia", vehiculo.Referencia);
                cmd.Parameters.AddWithValue("@precio", vehiculo.Precio);
                cmd.Parameters.AddWithValue("@color", vehiculo.Color);
                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@foto", vehiculo.Foto); // Guardar la ruta de la foto
                respuesta = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mensaje = "Error al agregar puntaje: " + ex.Message; // mensaje si hay error
            }
            finally
            {
                conn.Close(); // finaliza la conexion
            }
            return respuesta;
        }

        // Metodo para MostrarMarcas
        public List<string> MostrarMarcas()
        {
            List<string> marcas = new List<string>(); //lista que luego sera devuelta con las marcas dadas en la bd

            try
            {
                Conectar(); // conexion a bd
                //se da el comando y se ejecuta
                string query = "SELECT descripcion FROM Fabricantes";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                //se agrega cada descripcion que es el nombre de la marca a la lista
                while (reader.Read())
                {
                    marcas.Add(reader["descripcion"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                mensaje = "Error al buscar fabricantes: " + ex.Message; // mensaje si hay error
            }
            finally
            {
                conn.Close(); // finaliza la conexion
            }
            return marcas;
        }

        // Metodo para MostrarPlacas
        public List<string> MostrarPlacas()
        {
            List<string> placas = new List<string>();

            try
            {
                Conectar(); // conexion a bd
                //se da el comando y se ejecuta
                string query = "SELECT placa FROM Vehiculo";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //se agrega cada placa a la lista
                    placas.Add(reader["placa"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                mensaje = "Error al buscar placas: " + ex.Message; // mensaje si hay error
            }
            finally
            {
                conn.Close(); // finaliza la conexion
            }
            return placas;
        }

        // metodo para buscar vehiculo por placa
        public Vehiculo BuscarVehiculoPorPlaca(string placa)
        {
            Vehiculo vehiculo = null; //se inicia vehiculo como nulo
            Conectar();
            //se conecta y se ejecuta el comando
            string query = "SELECT * FROM Vehiculo where placa=@placa";
            cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@placa", placa);
            //parametro placa
            reader = cmd.ExecuteReader();
            if (reader.Read())
            //se asigna cada valor de la base de datos a su corresponiente en la instancia vehiculo
            {
                vehiculo = new Vehiculo();
                vehiculo.Placa = reader.GetString(0);
                vehiculo.Fabricante = reader.GetInt32(1);
                vehiculo.Referencia = reader.GetString(2);
                vehiculo.Precio = reader.GetDouble(3);
                vehiculo.Modelo = reader.GetInt32(4);
                vehiculo.Foto = reader.GetString(5);
                vehiculo.Color = reader.GetString(6);
            }
            // se finaliza la coneccion
            reader.Close();
            conn.Close();
            return vehiculo;
        }

        // metodo para editar vehiculo
        public int EditarVehiculo(Vehiculo vehiculo)
        {
            int respuesta = 0; //si la respuesta es 0 es porque no funciono 
            try
            {
                //se conecta la bd
                Conectar();
                //se ejecuta el codigo
                string query = "UPDATE Vehiculo SET idFabricante = @idFabricante, Referencia = @Referencia, precio = @precio, modelo = @modelo, color = @color WHERE placa = @placa";
                cmd = new SQLiteCommand(query, conn); // Inicializar comando
                // se pasa cada valor a la variable temporal
                cmd.Parameters.AddWithValue("@idFabricante", vehiculo.Fabricante);
                cmd.Parameters.AddWithValue("@Referencia", vehiculo.Referencia);
                cmd.Parameters.AddWithValue("@precio", vehiculo.Precio);
                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@color", vehiculo.Color);
                cmd.Parameters.AddWithValue("@placa", vehiculo.Placa);
                respuesta = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mensaje = "Error al editar vehículo: " + ex.Message; // mensaje si hay error
            }
            finally
            {
                conn.Close(); // finaliza la conexion
            }
            return respuesta;
        }

        // metodo para borrar vehiculo
        public int BorrarVehiculo(string placa)
        {
            int respuesta = 0; //si la respuesta es 0 es porque no funciono 
            try
            {
                // se hace conexion a la base de datos, se define el comando
                // se pasa el parametro a la variable temporal, se ejecuta el comando
                Conectar();
                string query = "DELETE FROM Vehiculo WHERE placa = @placa";
                cmd = new SQLiteCommand(query, conn); // Inicializar comando
                cmd.Parameters.AddWithValue("@placa", placa);
                respuesta = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mensaje = "Error al borrar vehiculo: " + ex.Message; // mensaje si hay error
            }
            finally
            {
                conn.Close(); // finaliza la conexion
            }
            return respuesta;
        }
    }
}
